using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Engine.Natives
{
    public class TileNative
    {
        string id;
        SceneTileAction action;
        float actionMod;
        bool unbearable;
        int defaultHeight;

        public int DefaultHeight { get { return defaultHeight; } }
        public bool Unbearable { get { return unbearable; } }
        public string Id { get { return id; } }
        public SceneTileAction Action { get { return action; } }
        public float ActionMod { get { return actionMod; } }

        public TileNative (string id, SceneTileAction action, float actionMod, bool unbearable, int defaultHeight)
        {
            this.id = id;
            this.action = action;
            this.actionMod = actionMod;
            this.unbearable = unbearable;
            this.defaultHeight = defaultHeight;
        }
    }
}
