﻿namespace AgribattleArena.Engine.Natives
{
    class TaggingNative
    {
        public string Id { get; }
        public string[] Tags { get; }

        public TaggingNative (string id, string[] tags)
        {
            this.Id = id;
            this.Tags = tags;
        }
    }
}
