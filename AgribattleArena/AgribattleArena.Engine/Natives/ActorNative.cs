namespace AgribattleArena.Engine.Natives
{
    public class ActorNative: TaggingNative
    {
        public float DefaultZ { get; }

        public ActorNative (string id, string[] tags, float defaultZ)
            :base (id,tags)
        {
            this.DefaultZ = defaultZ;
        }
    }
}
