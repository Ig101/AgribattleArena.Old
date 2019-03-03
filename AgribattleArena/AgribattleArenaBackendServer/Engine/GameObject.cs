using AgribattleArenaBackendServer.Engine.Natives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Engine
{
    public abstract class GameObject
    {
        Scene parent;

        bool isAlive;
        float x;
        float y;
        float z;

        public Scene Parent { get { return parent; } }
        public bool IsAlive { get { return isAlive; } set { isAlive = value; } }
        public float X { get { return x; } set { x = value; } }
        public float Y { get { return y; } set { y = value; } }
        public float Z { get { return z; } set { z = value; } }

        public GameObject (Scene parent, float x, float y, float z)
        {
            this.isAlive = true;
            this.parent = parent;
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public abstract void Update(float time);
    }
}
