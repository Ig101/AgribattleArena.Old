using AgribattleArena.Engine.Helpers;
using AgribattleArena.Engine.Objects;
using System.Collections.Generic;

namespace AgribattleArena.Engine
{
    public class Player: IPlayerParentRef, ForExternalUse.IPlayerShort
    {
        ISceneParentRef parent;

        string id;
        int? team;
        List<Actor> keyActors;
        int turnsSkipped;
        PlayerStatus status;

        public int? Team { get { return team; } }
        public ISceneParentRef Parent { get { return parent; } }
        public string Id { get { return id; } }
        public List<Actor> KeyActors { get { return keyActors; } }
        public int TurnsSkipped { get { return turnsSkipped; } }
        public PlayerStatus Status { get { return status; } set { status = value; } }

        public Player (ISceneParentRef parent, string id, int? team)
        {
            this.team = team;
            this.parent = parent;
            this.id = id;
            this.keyActors = new List<Actor>();
            this.turnsSkipped = 0;
            this.status = PlayerStatus.Playing;
        }

        public void SkipTurn()
        {
            turnsSkipped++;
        }

        public bool ActThisTurn()
        {
            if (turnsSkipped > 0)
            {
                turnsSkipped = 0;
                return true;
            }
            return false;
        }

        public void Defeat()
        {
            status = PlayerStatus.Defeated;
            parent.GetPlayerActors(this).ForEach(x => x.Kill());
        }

        public void Victory()
        {
            status = PlayerStatus.Victorious;
        }
    }
}
