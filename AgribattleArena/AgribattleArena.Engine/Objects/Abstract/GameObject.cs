namespace AgribattleArena.Engine.Objects.Abstract
{
    public abstract class GameObject: IdObject
    {
        IPlayerParentRef owner;

        bool isAlive;
        float x;
        float y;
        float z;

        public IPlayerParentRef Owner { get { return owner; } set { owner = value; } }
        public bool IsAlive { get { return isAlive; } set { isAlive = value; } }
        public float X { get { return x; } set { x = value; } }
        public float Y { get { return y; } set { y = value; } }
        public float Z { get { return z; } set { z = value; } }

        public GameObject(ISceneParentRef parent, IPlayerParentRef owner, float x, float y, float z)
            : base(parent)
        {
            this.owner = owner;
            this.isAlive = true;
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public abstract void Update(float time);
    }
}