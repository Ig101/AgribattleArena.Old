using AgribattleArenaBackendServer.Engine.ActorModel;
using AgribattleArenaBackendServer.Engine.Generator;
using AgribattleArenaBackendServer.Engine.Generator.GeneratorEntities;
using AgribattleArenaBackendServer.Engine.Helpers;
using AgribattleArenaBackendServer.Engine.NativeManager;
using AgribattleArenaBackendServer.Engine.Synchronization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Engine
{
    public delegate void SyncHandler(Scene scene, Engine.Helpers.Action action, uint? id, int? actionId, int? targetX, int? targetY, ISynchronizationObject syncInfo);

    public class Scene: IScene
    {
        public event SyncHandler ReturnAction;

        int id;
        List<int> playerIds;

        INativeManager nativeManager;
        Random gameRandom;

        Tile[,] tiles;
        List<TileObject> actors;
        List<SpecEffect> specEffects;
        TileObject tempTileObject;

        List<TileObject> deletedActors;
        List<SpecEffect> deletedEffects;
        uint idsCounter;
        int randomCounter;

        public INativeManager NativeManager { get { return nativeManager; } }

        public TileObject TempTileObject { get { return tempTileObject; } }
        public int Id { get { return id; } }
        public int RandomCounter { get { return randomCounter; } }
        public IEnumerable<int> PlayerIds { get { return playerIds; } }

        public Scene(int id, List<int> playerIds, IProfilesServiceSceneLink profilesService, ILevelGenerator generator, INativeManager nativeManager, int seed)
        {
            //TODO playerSignatures
            this.playerIds = playerIds;
            this.id = id;
            this.gameRandom = new Random(seed);
            this.nativeManager = nativeManager;
            idsCounter = 0;
            actors = new List<TileObject>();
            specEffects = new List<SpecEffect>();
            deletedActors = new List<TileObject>();
            deletedEffects = new List<SpecEffect>();
            GenerationSet set = generator.GenerateNewScene(profilesService, playerIds, unchecked(seed * id));
            tiles = new Tile[set.TileSet.GetLength(0), set.TileSet.GetLength(1)];
            for(int x = 0; x<tiles.GetLength(0);x++)
            {
                for(int y = 0; y<tiles.GetLength(1);y++)
                {
                    CreateTile(set.TileSet[x, y].Native, x, y, set.TileSet[x, y].Height);
                }
            }
            foreach(PlayerActor actor in set.PlayerActors)
            {
                CreateActor(actor.Owner, actor.Native, actor.RoleModel, tiles[actor.TileX, actor.TileY], null);
            }
            foreach(GenerationObject actor in set.Actors)
            {
                CreateActor(actor.Owner, actor.Native, actor.RoleNative, tiles[actor.TileX, actor.TileY], null);
            }
            foreach(GenerationObject decoration in set.Decorations)
            {
                CreateDecoration(decoration.Owner, decoration.Native, tiles[decoration.TileX, decoration.TileY], null, null, null, null);
            }
            EndTurn();
        }

        public float GetNextRandom()
        {
            randomCounter++;
            return (float)gameRandom.NextDouble();
        }

        public uint GetNextId()
        {
            uint tempId = idsCounter;
            idsCounter++;
            return tempId;
        }

        public Tile GetTile(float x, float y)
        {
            return tiles[(int)(x / Misc.tileSize), (int)(y / Misc.tileSize)];
        }

        #region Creation
        public Actor CreateActor(int? ownerId, string native, string roleNative, Tile target, float? z)
        {
            Actor actor = new Actor(this, ownerId, target, z, nativeManager.GetActorNative(native), nativeManager.GetRoleModelNative(roleNative));
            actors.Add(actor);
            return actor;
        }

        public Actor CreateActor(int? ownerId, string native, RoleModel roleModel, Tile target, float? z)
        {
            Actor actor = new Actor(this, ownerId, target, z, nativeManager.GetActorNative(native), roleModel);
            actors.Add(actor);
            return actor;
        }

        public ActiveDecoration CreateDecoration(int? ownerId, string native, Tile target, float? z, int? health, TagSynergy[] armor, float? mod)
        {
            ActiveDecoration actor = new ActiveDecoration(this, ownerId, target, z, health, armor, nativeManager.GetDecorationNative(native), mod);
            actors.Add(actor);
            return actor;
        }

        public SpecEffect CreateEffect(int? ownerId, string native, float x, float y, float? z, float? duration, float? mod)
        {
            SpecEffect effect = new SpecEffect(this, ownerId, x, y, z, nativeManager.GetEffectNative(native), duration, mod);
            specEffects.Add(effect);
            return effect;
        }

        public Tile CreateTile(string native, int x, int y, int? height)
        {
            Tile tile = new Tile(this, x, y, nativeManager.GetTileNative(native), height);
            tiles[x,y] = tile;
            return tile;
        }
        #endregion

        #region Sync
        public SynchronizationObject GetSynchronizationData (bool nullify)
        {
            SynchronizationObject sync = new SynchronizationObject(actors, deletedActors, deletedEffects, tiles,randomCounter);
            if (nullify)
            {
                sync.ChangedActors.ForEach(x => x.Affected = false);
                sync.ChangedTiles.ForEach(x => x.Affected = false);
                this.deletedActors.Clear();
                this.deletedEffects.Clear();
            }
            return sync;
        }

        public SynchronizationObject GetSynchronizationData ()
        {
            return GetSynchronizationData(false);
        }

        public FullSynchronizationObject GetFullSynchronizationData()
        {
            return new FullSynchronizationObject(actors, specEffects, tiles,randomCounter);
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
                this.tempTileObject = newObject;
                Update(minInitiativePosition);
                ReturnAction(this, Engine.Helpers.Action.EndTurn, null, null, null, null, GetSynchronizationData(true));
                this.tempTileObject.StartTurn();
            }
        }

        void Update(float time)
        {
            foreach(TileObject obj in actors)
            {
                obj.Update(time);
            }
            foreach(SpecEffect eff in specEffects)
            {
                eff.Update(time);
            }
            AfterActionUpdate();
        }

        void AfterActionUpdate()
        {
            for(int i = 0; i<actors.Count;i++)
            {
                if(!actors[i].IsAlive)
                {
                    deletedActors.Add(actors[i]);
                    actors.RemoveAt(i);
                    i--;
                }
            }
            for(int i = 0; i<specEffects.Count;i++)
            {
                if(!specEffects[i].IsAlive)
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
        public bool DecorationCast (ActiveDecoration actor)
        {
            if (tempTileObject == actor)
            {
                actor.Cast();
                AfterActionUpdate();
                this.ReturnAction(this, Engine.Helpers.Action.Move, actor.Id, null, null, null, GetSynchronizationData(true));
                return true;
            }
            return false;
        }

        public bool ActorMove (Actor actor, Tile target)
        {
            if(tempTileObject == actor)
            {
                bool result = actor.Move(target);
                if(result)
                {
                    AfterActionUpdate();
                    this.ReturnAction(this, Engine.Helpers.Action.Move, actor.Id,null,target.X,target.Y, GetSynchronizationData(true));
                }
                return result;
            }
            return false;
        }

        public bool ActorCast (Actor actor, int id, Tile target)
        {
            if (tempTileObject == actor)
            {
                bool result = actor.Cast(id, target);
                if (result)
                {
                    AfterActionUpdate();
                    this.ReturnAction(this, Engine.Helpers.Action.Cast, actor.Id, id, target.X,target.Y, GetSynchronizationData(true));
                }
                return result;
            }
            return false;
        }

        public bool ActorAttack (Actor actor, Tile target)
        {
            if (tempTileObject == actor)
            {
                bool result = actor.Attack(target);
                if (result)
                {
                    AfterActionUpdate();
                    this.ReturnAction(this, Engine.Helpers.Action.Attack, actor.Id,null,target.X,target.Y, GetSynchronizationData(true));
                }
                return result;
            }
            return false;
        }

        public bool ActorWait (Actor actor)
        {
            if (tempTileObject == actor)
            {
                bool result = actor.Wait();
                if (result)
                {
                    AfterActionUpdate();
                    this.ReturnAction(this, Engine.Helpers.Action.Wait, actor.Id, null, null, null, GetSynchronizationData(true));
                }
                return result;
            }
            return false;
        }
        #endregion
    }
}
