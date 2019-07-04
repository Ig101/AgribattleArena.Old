using System;
using System.Collections.Generic;
using System.Text;

namespace AgribattleArena.Configurator.Models.Natives
{
    class TagSynergyDto: ActionObject
    {
        public string SelfTag { get; set; }
        public string TargetTag { get; set; }
        public float Mod { get; set; }
    }
}
