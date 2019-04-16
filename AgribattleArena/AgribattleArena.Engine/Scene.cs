using AgribattleArena.Engine.Helpers;
using AgribattleArena.Engine.NativeManagers;
using AgribattleArena.Engine.Natives;
using AgribattleArena.Engine.Objects;
using AgribattleArena.Engine.Objects.Abstract;
using AgribattleArena.Engine.SceneGenerators;
using AgribattleArena.Engine.Synchronizers;
using AgribattleArena.Engine.VarManagers;
using System;
using System.Collections.Generic;

namespace AgribattleArena.Engine
{ 
    public class Scene: ISceneParentRef, ISceneForSceneGenerator, ForExternalUse.IScene
    {
        public event EventHandler<SyncEventArgs> ReturnAction;

        int id;
        List<Player> players;

        readonly IVarManager varManager;
        INativeManager nativeManager;
        Random gameRandom;

        Tile[][] tiles;
        List<TileObject> actors;
        List<SpecEffect> specEffects;
        TileObject tempTileObject;

        List<TileObject> deletedActors;
        List<SpecEffect> deletedEffects;
        int idsCounter;
        int randomCounter;

        public IVarManager VarManager { get { return varManager; } }
        public INativeManager NativeManager { get { return nativeManager; } }

        public TileObject TempTileObject { get { return tempTileObject; } }
        public int Id { get { return id; } }
        public int RandomCounter { get { return randomCounter; } }
        public IEnumerable<Player> Players { get { return players; } }

        public Scene(int id, IEnumerable<ForExternalUse.Generation.ObjectInterfaces.IPlayer> players, ForExternalUse.Generation.ISceneGenerator generator, 
            ForExternalUse.INativeManager nativeManager, int seed)
        {
            this.id = id;
            this.gameRandom = new Random(seed);
            this.nativeManager = (INativeManager)nativeManager;
            ISceneGenerator tempGenerator = (ISceneGenerator)generator;
            idsCounter = 0;
            this.players = new List<Player>();
            actors = new List<TileObject>();
            specEffects = new List<SpecEffect>();
            deletedActors = new List<TileObject>();
            deletedEffects = new List<SpecEffect>();
            tempGenerator.GenerateNewScene(this, players, unchecked(seed * id));
            EndTurn();
        }

        public float GetNextRandom()
        {
            randomCounter++;
            return (float)gameRandom.NextDouble();
        }

        public int GetNextId()
        {
            int tempId = idsCounter;
            idsCounter++;
            return tempId;
        }

        public Tile GetTile(float x, float y)
        {
            return tiles[(int)(x / VarManager.TileSize)][(int)(y / VarManager.TileSize)];
        }

        #region Creation
        public Tile[][] SetupEmptyTileSet (int width, int height)
        {
            Tile[][] tiles= new Tile[width][];
            for(int i = 0; i<width;i++)
            {
                tiles[i] = new Tile[height];
            }
            this.tiles = tiles;
            return tiles;
        }

        public Player CreatePlayer (int id)
        {
            Player player = new Player(this, id);
            players.Add(player);
            return player;
        }

        public Actor CreateActor(Player owner, string nativeName, string roleNativeName, Tile target, float? z)
        {
            return CreateActor(owner, null, nativeName, nativeManager.GetRoleModelNative(roleNativeName), target, z);
        }

        public Actor CreateActor(Player owner, int? externalId, string nativeName, RoleModelNative roleModel, Tile target, float? z)
        {
            Actor actor = new Actor(this, owner, externalId, target, z, nativeManager.GetActorNative(nativeName), roleModel);
            actors.Add(actor);
            return actor;
        }

        public ActiveDecoration CreateDecoration(Player owner, string nativeName, Tile target, float? z, int? health, TagSynergy[] armor, float? mod)
        {
            ActiveDecoration decoration = new ActiveDecoration(this, owner, target, z, health, armor, nativeManager.GetDecorationNative(nativeName), mod);
            actors.Add(decoration);
            return decoration;
        }

        public SpecEffect CreateEffect(Player owner, string nativeName, float x, float y, float? z, float? duration, float? mod)
        {
            SpecEffect effect = new SpecEffect(this, owner, x, y, z, nativeManager.GetEffectNative(nativeName), duration, mod);
            specEffects.Add(effect);
            return effect;
        }

        public Tile CreateTile(string nativeName, int x, int y, int? height)
        {
            Tile tile = new Tile(this, x, y, nativeManager.GetTileNative(nativeName), height);
            tiles[x][y] = tile;
            return tile;
        }
        #endregion

        #region Sync
     /*   public SynchronizationObject GetSynchronizationData(bool nullify)
        {
            SynchronizationObject sync = new SynchronizationObject(actors, deletedActors, deletedEffects, tiles, randomCounter);
            if (nullify)
            {
                sync.ChangedActors.ForEach(x => x.Affected = false);
                sync.ChangedTiles.ForEach(x => x.Affected = false);
                this.deletedActors.Clear();
                this.deletedEffects.Clear();
            }
            return sync;
        }

        public SynchronizationObject GetSynchronizationData()
        {
            return GetSynchronizationData(false);
        }

        public FullSynchronizationObject GetFullSynchronizationData()
        {
            return new FullSynchronizationObject(actors, specEffects, tiles, randomCounter);
        }*/
        #endregion

        #region Updates
        public void EndTurn()
        {
            float minInitiativePosition = float.MaxValue;
            TileObject newObject = null;
            foreach (TileObject obj in actors)
            {
                if (obj.IsAlive && obj.InitiativePosition < minInitiativePosition)
                {
                    minInitiativePosition = obj.InitiativePosition;
                    newObject = obj;
                }
            }
            if (newObject != null)
            {
                this.tempTileObject = newObject;
                Update(minInitiativePosition);
                //ReturnAction(this, Engine.Helpers.Action.EndTurn, null, null, null, null, GetSynchronizationData(true));
                this.tempTileObject.StartTurn();
            }
        }

        void Update(float time)
        {
            foreach (TileObject obj in actors)
            {
                obj.Update(time);
            }
            foreach (SpecEffect eff in specEffects)
            {
                eff.Update(time);
            }
            AfterActionUpdate();
        }

        void AfterActionUpdate()
        {
            for (int i = 0; i < actors.Count; i++)
            {
                if (!actors[i].IsAlive)
                {
                    deletedActors.Add(actors[i]);
                    actors.RemoveAt(i);
                    i--;
                }
            }
            for (int i = 0; i < specEffects.Count; i++)
            {
                if (!specEffects[i].IsAlive)
                {
                    deletedEffects.Add(specEffects[i]);
                    specEffects.RemoveAt(i);
                    i--;
                }
            }
            //TODO WinCondition
        }
        #endregion

        #region Actions
        public bool DecorationCast(ActiveDecoration actor)
        {
            if (tempTileObject == actor)
            {
                actor.Cast();
                AfterActionUpdate();
                //this.ReturnAction(this, Engine.Helpers.Action.Move, actor.Id, null, null, null, GetSynchronizationData(true));
                return true;
            }
            return false;
        }

        public bool ActorMove(Actor actor, Tile target)
        {
            if (tempTileObject == actor)
            {
                bool result = actor.Move(target);
                if (result)
                {
                    AfterActionUpdate();
                    //this.ReturnAction(this, Engine.Helpers.Action.Move, actor.Id, null, target.X, target.Y, GetSynchronizationData(true));
                }
                return result;
            }
            return false;
        }

        public bool ActorCast(Actor actor, int id, Tile target)
        {
            if (tempTileObject == actor)
            {
                bool result = actor.Cast(id, target);
                if (result)
                {
                    AfterActionUpdate();
                    //this.ReturnAction(this, Engine.Helpers.Action.Cast, actor.Id, id, target.X, target.Y, GetSynchronizationData(true));
                }
                return result;
            }
            return false;
        }

        public bool ActorAttack(Actor actor, Tile target)
        {
            if (tempTileObject == actor)
            {
                bool result = actor.Attack(target);
                if (result)
                {
                    AfterActionUpdate();
                    //this.ReturnAction(this, Engine.Helpers.Action.Attack, actor.Id, null, target.X, target.Y, GetSynchronizationData(true));
                }
                return result;
            }
            return false;
        }

        public bool ActorWait(Actor actor)
        {
            if (tempTileObject == actor)
            {
                bool result = actor.Wait();
                if (result)
                {
                    AfterActionUpdate();
                    //this.ReturnAction(this, Engine.Helpers.Action.Wait, actor.Id, null, null, null, GetSynchronizationData(true));
                }
                return result;
            }
            return false;
        }
        #endregion
    }
}
