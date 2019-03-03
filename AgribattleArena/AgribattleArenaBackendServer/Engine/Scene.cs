using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Engine
{
    public delegate void SyncHandler(Scene scene, Action action, int? id, int? actionId, int? targetX, int? targetY);

    public class Scene
    {
        public event SyncHandler ReturnAction;

        List<TileObject> actors;
        List<SpecEffect> specEffects;
        TileObject tempTileObject;


        public Scene()
        {
            actors = new List<TileObject>();
            specEffects = new List<SpecEffect>();
            EndTurn();
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
                this.tempTileObject.StartTurn();
                ReturnAction(this, Action.EndTurn, null, null, null, null);
            }
        }

        public void Update(float time)
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

        public void AfterActionUpdate()
        {
            for(int i = 0; i<actors.Count;i++)
            {
                if(!actors[i].IsAlive)
                {
                    actors.RemoveAt(i);
                    i--;
                }
            }
            for(int i = 0; i<specEffects.Count;i++)
            {
                if(!specEffects[i].IsAlive)
                {
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
                this.ReturnAction(this, Action.Move, actors.FindIndex(x => x == actor), null, null, null);
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
                    this.ReturnAction(this, Action.Move, actors.FindIndex(x => x == actor),null,target.X,target.Y);
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
                    this.ReturnAction(this, Action.Cast, actors.FindIndex(x => x == actor), id, target.X,target.Y);
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
                    this.ReturnAction(this, Action.Attack, actors.FindIndex(x => x == actor),null,target.X,target.Y);
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
                    this.ReturnAction(this, Action.Wait, actors.FindIndex(x => x == actor), null, null, null);
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
