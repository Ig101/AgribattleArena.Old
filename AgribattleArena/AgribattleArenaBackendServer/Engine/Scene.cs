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
        
        InitiativeScale initiativeScale;

        List<TileObject> actors;
        List<SpecEffect> specEffects;

        public InitiativeScale InitiativeScale { get { return initiativeScale; } }
        public List<TileObject> Actors { get { return actors; } }
        public List<SpecEffect> SpecEffects { get { return specEffects; } }

        public Scene()
        {
            initiativeScale = new InitiativeScale(this);
            actors = new List<TileObject>();
            specEffects = new List<SpecEffect>();
        }

        public void Update(float time)
        {
            foreach(TileObject obj in Actors)
            {
                obj.Update(time);
            }
            foreach(SpecEffect eff in SpecEffects)
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

        public bool ActorMove (Actor actor, Tile target)
        {
            if(initiativeScale.TempTileObject == actor)
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
            if (initiativeScale.TempTileObject == actor)
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
            if (initiativeScale.TempTileObject == actor)
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
            if (initiativeScale.TempTileObject == actor)
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

        public void ReturnActionImplementation(Action action, int? id)
        {
            this.ReturnAction(this, action, id, null, null, null);
        }

        public Tile GetTile (float x, float y)
        {
            return null;
        }
    }
}
