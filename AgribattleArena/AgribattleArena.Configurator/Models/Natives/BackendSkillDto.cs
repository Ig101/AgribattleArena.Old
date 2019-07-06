using System;
using System.Collections.Generic;
using System.Text;

namespace AgribattleArena.Configurator.Models.Natives
{
    class BackendSkillDto: ActionObject
    {
        public string Name { get; set; }
        public List<string> Tags { get; set; }
        public int? Range { get; set; }
        public int? Cost { get; set; }
        public float? Cd { get; set; }
        public float? Mod { get; set; }
        public List<string> Action { get; set; }
    }
}
