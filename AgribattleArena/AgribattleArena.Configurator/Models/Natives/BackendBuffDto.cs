using System;
using System.Collections.Generic;
using System.Text;

namespace AgribattleArena.Configurator.Models.Natives
{
    class BackendBuffDto : ActionObject
    {
        public string Name { get; set; }
        public List<string> Tags { get; set; }
        public bool? Eternal { get; set; }
        public int? Repeatable { get; set; }
        public bool? SummarizeLength { get; set; }
        public int? Duration { get; set; }
        public float? Mod { get; set; }
        public List<string> Action { get; set; }
        public List<string> BuffApplier { get; set; }
        public List<string> OnPurgeAction { get; set; }
    }
}
