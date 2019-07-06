using System;
using System.Collections.Generic;
using System.Text;

namespace AgribattleArena.Configurator.Models.Natives
{
    class BackendDecorationDto : ActionObject
    {
        public string Name { get; set; }
        public List<string> Tags { get; set; }
        public int? Health { get; set; }
        public float? Z { get; set; }
        public float? Mod { get; set; }
        public List<TagSynergyDto> Armor { get; set; }
        public List<string> Action { get; set; }
        public List<string> OnDeathAction { get; set; }
    }
}
