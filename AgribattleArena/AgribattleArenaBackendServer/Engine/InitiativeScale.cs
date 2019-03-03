using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Engine
{
    public class InitiativeScale
    {
        Scene scene;

        TileObject tempTileObject;

        public Scene Scene { get { return scene; } }
        public TileObject TempTileObject { get { return tempTileObject; } set { tempTileObject = value; } }

        public InitiativeScale(Scene scene)
        {
            this.scene = scene;
            EndTurn();
        }

        public void EndTurn()
        { 
            float minInitiativePosition = float.MaxValue;
            TileObject newObject = null;
            foreach(TileObject obj in scene.Actors)
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
                scene.Update(minInitiativePosition);
                this.tempTileObject.StartTurn();
                //TODO ReturnAction
            }
        }

    }
}
