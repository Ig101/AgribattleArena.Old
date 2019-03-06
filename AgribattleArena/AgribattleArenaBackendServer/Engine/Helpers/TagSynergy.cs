namespace AgribattleArenaBackendServer.Engine.Helpers
{
    public class TagSynergy
    {
        string selfTag;
        string targetTag;
        float mod;

        public string SelfTag { get { return selfTag; } }
        public string TargetTag { get { return targetTag; } }
        public float Mod { get { return mod; } set { mod = value; } }

        public TagSynergy (string selfTag, string targetTag, float mod)
        {
            this.selfTag = selfTag;
            this.targetTag = targetTag;
            this.mod = mod;
        }
    }
}
