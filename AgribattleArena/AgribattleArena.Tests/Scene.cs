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
        int version;
        float remainedTurnTime;
        TileObject tempTileObject;

        readonly IVarManager varManager;
        INativeManager nativeManager;
        Random gameRandom;

        Tile[][] tiles;
        List<TileObject> actors;
        List<SpecEffect> specEffects;

        List<TileObject> deletedActors;
        List<SpecEffect> deletedEffects;
        int idsCounter;
        int randomCounter;

        public IVarManager VarManager { get { return varManager; } }
        public INativeManager NativeManager { get { return nativeManager; } }

        public int Version { get { return version; } }
        public float RemainedTurnTime { get { return remainedTurnTime; } }
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
        public ForExternalUse.Synchronization.ISynchronizer GetSynchronizationData(bool nullify)
        {
            ForExternalUse.Synchronization.ISynchronizer sync = null;/* new SynchronizationObject(actors, deletedActors, deletedEffects, tiles, randomCounter);
            if (nullify)
            {
                sync.ChangedActors.ForEach(x => x.Affected = false);
                sync.ChangedTiles.ForEach(x => x.Affected = false);
                this.deletedActors.Clear();
                this.deletedEffects.Clear();
            }*/
            return sync;
        }

        public ForExternalUse.Synchronization.ISynchronizer GetSynchronizationData()
        {
            return GetSynchronizationData(false);
        }

        public ForExternalUse.Synchronization.ISynchronizer GetFullSynchronizationData()
        {
            return null;// new FullSynchronizationObject(actors, specEffects, tiles, randomCounter);
        }
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
                this.remainedTurnTime = varManager.TurnTimeLimit;
                TileObject previousTempTileObject = this.tempTileObject; 
                this.tempTileObject = newObject;
                Update(minInitiativePosition);
                AfterUpdateSynchronization(ForExternalUse.EngineHelper.Action.EndTurn, previousTempTileObject, null, null, null);
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
            foreach(Player player in players)
            {
                player.
            }
        }

        //TODO Lock
        public void UpdateTime(float time)
        {
            this.remainedTurnTime -= time;
            if(remainedTurnTime<=0)
            {
                IPlayerParentRef player = tempTileObject.Owner;
                TileObject previousTempTileObject = tempTileObject;
                tempTileObject.EndTurn();
                if (player!=null)
                {
                    player.SkipTurn()
                    AfterUpdateSynchronization(ForExternalUse.EngineHelper.Action.SkipTurn, tempTileObject, null, null, null);
                }
            }
        }

        public void AfterUpdateSynchronization (ForExternalUse.EngineHelper.Action action, TileObject actor, int? actionId, int? targetX, int? targetY)
        {
            AfterActionUpdate();
            this.ReturnAction(this, new SyncEventArgs(this, action, GetSynchronizationData(true), actor?.Id, actionId, targetX, targetY));

        }
        #endregion

        #region Actions
        public bool DecorationCast(ActiveDecoration actor)
        {
            if (tempTileObject == actor)
            {
                actor.Cast();
                AfterUpdateSynchronization(ForExternalUse.EngineHelper.Action.Decoration, actor, null, null, null);
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
                    AfterUpdateSynchronization(ForExternalUse.EngineHelper.Action.Move, actor, null, target.X, target.Y);
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
                    AfterUpdateSynchronization(ForExternalUse.EngineHelper.Action.Cast, actor, null, target.X, target.Y);
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
                    AfterUpdateSynchronization(ForExternalUse.EngineHelper.Action.Attack, actor, null, target.X, target.Y);
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
                    AfterUpdateSynchronization(ForExternalUse.EngineHelper.Action.Wait, actor, null, null, null);
                }
                return result;
            }
            return false;
        }
        #endregion
    }
}
