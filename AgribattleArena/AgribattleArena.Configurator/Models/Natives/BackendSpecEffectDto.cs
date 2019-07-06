using System;
using System.Collections.Generic;
using System.Text;

namespace AgribattleArena.Configurator.Models.Natives
{
    class BackendSpecEffectDto: ActionObject
    {
        public string Name { get; set; }
        public List<string> Tags { get; set; }
        public float? Z { get; set; }
        public float? Duration { get; set; }
        public float? Mod { get; set; }
        public List<string> Action { get; set; }
        public List<string> OnDeathAction { get; set; }
    }
}
