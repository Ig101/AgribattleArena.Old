using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Engine
{
    public class Scene
    {
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
                    //TODO ReturnAction
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
                    //TODO ReturnAction
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
                    //TODO ReturnAction
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
                    //TODO ReturnAction
                }
                return result;
            }
            return false;
        }

        public Tile GetTile (float x, float y)
        {
            return null;
        }
    }
}
