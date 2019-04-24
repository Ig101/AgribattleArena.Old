namespace AgribattleArena.Engine.Objects.Abstract
{
    public abstract class GameObject: IdObject
    {
        IPlayerParentRef owner;

        bool isAlive;
        int x;
        int y;
        float z;

        public IPlayerParentRef Owner { get { return owner; } set { owner = value; } }
        public bool IsAlive { get { return isAlive; } set
            {
                bool prevoiusState = isAlive;
                isAlive = value;
                if (!isAlive && prevoiusState) OnDeathAction();
            } }
        public int X { get { return x; } set { x = value; } }
        public int Y { get { return y; } set { y = value; } }
        public float Z { get { return z; } set { z = value; } }

        public GameObject(ISceneParentRef parent, IPlayerParentRef owner, int x, int y, float z)
            : base(parent)
        {
            this.owner = owner;
            this.isAlive = true;
            this.x = x;
            this.y = y;
            this.z = parent.GetTileByPoint(x,y).Height + z;
        }

        public abstract void Update(float time);

        public abstract void OnDeathAction();
    }
}