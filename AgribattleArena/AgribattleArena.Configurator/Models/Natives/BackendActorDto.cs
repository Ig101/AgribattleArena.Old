using System;
using System.Collections.Generic;
using System.Text;

namespace AgribattleArena.Configurator.Models.Natives
{
    class BackendActorDto: ActionObject
    {
        public string Name { get; set; }
        public List<string> Tags { get; set; }
        public float? Z { get; set; }
        public List<TagSynergyDto> Armor { get; set; }
    }
}
