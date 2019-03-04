using AgribattleArenaBackendServer.Engine.Generator;
using AgribattleArenaBackendServer.Engine.Synchronization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Engine
{
    public delegate void SyncHandler(Scene scene, Action action, uint? id, int? actionId, int? targetX, int? targetY, ISynchronizationObject syncInfo);

    public class Scene
    {
        public event SyncHandler ReturnAction;

        Tile[,] tiles;
        List<TileObject> actors;
        List<SpecEffect> specEffects;
        TileObject tempTileObject;

        List<TileObject> deletedActors;
        List<SpecEffect> deletedEffects;
        uint idsCounter;

        #region Sync
        public SynchronizationObject GetSynchronizationData (bool nullify)
        {
            SynchronizationObject sync = new SynchronizationObject(actors, deletedActors, deletedEffects, tiles);
            if (nullify)
            {
                sync.ChangedActors.ForEach(x => x.Affected = false);
                sync.ChangedTiles.ForEach(x => x.Affected = false);
                this.deletedActors.Clear();
                this.deletedEffects.Clear();
            }
            return sync;
        }

        public FullSynchronizationObject GetFullSynchronizationData()
        {
            return new FullSynchronizationObject(actors, specEffects, tiles);
        }
        #endregion

        public Scene(ILevelGenerator generator)
        {
            idsCounter = 0;
            actors = new List<TileObject>();
            specEffects = new List<SpecEffect>();
            deletedActors = new List<TileObject>();
            deletedEffects = new List<SpecEffect>();
            EndTurn();
        }

        public uint GetNextId ()
        {
            uint tempId = idsCounter;
            idsCounter++;
            return tempId;
        }

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
                ReturnAction(this, Action.EndTurn, null, null, null, null, GetSynchronizationData(true));
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
                this.ReturnAction(this, Action.Move, actor.Id, null, null, null, GetSynchronizationData(true));
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
                    this.ReturnAction(this, Action.Move, actor.Id,null,target.X,target.Y, GetSynchronizationData(true));
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
                    this.ReturnAction(this, Action.Cast, actor.Id, id, target.X,target.Y, GetSynchronizationData(true));
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
                    this.ReturnAction(this, Action.Attack, actor.Id,null,target.X,target.Y, GetSynchronizationData(true));
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
                    this.ReturnAction(this, Action.Wait, actor.Id, null, null, null, GetSynchronizationData(true));
                }
                return result;
            }
            return false;
        }
        #endregion

        public Tile GetTile (float x, float y)
        {
            return null;
        }
    }
}
