using System;
using System.Collections.Generic;
using System.Text;

namespace AgribattleArena.Engine.Objects.Abstract
{
    abstract class GameObject
    {

        IPlayerParentRef owner;
        readonly ISceneParentRef parent;

        readonly int id;
        bool isAlive;
        float x;
        float y;
        float z;

        public int Id { get { return id; } }
        public IPlayerParentRef Owner { get { return owner; } set { owner = value; } }
        public ISceneParentRef Parent { get { return parent; } }
        public bool IsAlive { get { return isAlive; } set { isAlive = value; } }
        public float X { get { return x; } set { x = value; } }
        public float Y { get { return y; } set { y = value; } }
        public float Z { get { return z; } set { z = value; } }

        public GameObject(ISceneParentRef parent, IPlayerParentRef owner, float x, float y, float z)
        {
            this.owner = owner;
            this.isAlive = true;
            this.parent = parent;
            this.x = x;
            this.y = y;
            this.z = z;
            this.id = parent.GetNextId();
        }

        public abstract void Update(float time);
    }
}
