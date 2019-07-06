using System;
using System.Collections.Generic;
using System.Text;

namespace AgribattleArena.Configurator.Models.Natives
{
    class BackendTileDto: ActionObject
    {
        public string Name { get; set; }
        public List<string> Tags { get; set; }
        public bool? Flat { get; set; }
        public int? DefaultHeight { get; set; }
        public bool? Unbearable { get; set; }
        public float? Mod { get; set; }
        public List<string> Action { get; set; }
        public List<string> OnStepAction { get; set; }
    }
}
