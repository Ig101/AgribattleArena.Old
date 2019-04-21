namespace AgribattleArena.Engine.Natives
{
    public class TaggingNative
    {
        public string Id { get; }
        public string IdForFront { get; }
        public string[] Tags { get; }

        public TaggingNative (string id, string[] tags)
            :this(id,id,tags)
        {

        }

        public TaggingNative (string id, string idForFront, string[] tags)
        {
            this.Id = id;
            this.IdForFront = idForFront;
            this.Tags = tags;
        }
    }
}
