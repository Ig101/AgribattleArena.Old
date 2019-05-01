using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArena.BackendServer.Models.Natives
{
    public class TileDto
    {
        public string Id { get; set; }
        public IEnumerable<string> Tags { get; set; }
        public bool Flat { get; set; }
        public int DefaultHeight { get; set; }
        public bool Unbearable { get; set; }
        public float Mod { get; set; }
        public IEnumerable<string> Action { get; set; }
        public IEnumerable<string> OnStepAction { get; set; }
    }
}
