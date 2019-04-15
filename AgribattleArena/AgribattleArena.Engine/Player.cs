using AgribattleArena.Engine.Helpers;
using AgribattleArena.Engine.Objects.Abstract;
using System.Collections.Generic;

namespace AgribattleArena.Engine
{
    public class Player
    {
        readonly ISceneParentRef parent;

        readonly int id;
        readonly List<TileObject> keyActors;
        int turnsSkipped;
        PlayerStatus status;

        public ISceneParentRef Parent { get { return parent; } }
        public int Id { get { return id; } }
        public List<TileObject> KeyActors { get { return keyActors; } }
        public int TurnsSkipped { get { return turnsSkipped; } }
        public PlayerStatus Status { get { return status; } set { status = value; } }

        public Player (ISceneParentRef parent, int id)
        {
            this.parent = parent;
            this.id = id;
            this.keyActors = new List<TileObject>();
            this.turnsSkipped = 0;
            this.status = PlayerStatus.Playing;
        }

        public bool SkipTurn()
        {
            turnsSkipped++;
            if(turnsSkipped>=parent.VarManager.SkippedTurnsLimit)
            {
                return true;
            }
            return false;
        }

        public void Defeat()
        {
            status = PlayerStatus.Defeated;
        }

        public void Victory()
        {
            status = PlayerStatus.Victorious;
        }
    }
}
