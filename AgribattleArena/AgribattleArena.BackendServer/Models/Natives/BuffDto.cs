using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArena.BackendServer.Models.Natives
{
    public class BuffDto
    {
        public string Id { get; set; }
        public IEnumerable<string> Tags { get; set; }
        public bool Eternal { get; set; }
        public bool Repeatable { get; set; }
        public bool SummarizeLength { get; set; }
        public float? Duration { get; set; }
        public float Mod { get; set; }
        public IEnumerable<string> Action { get; set; }
        public IEnumerable<string> BuffApplier { get; set; }
        public IEnumerable<string> OnPurgeAction { get; set; }
    }
}
