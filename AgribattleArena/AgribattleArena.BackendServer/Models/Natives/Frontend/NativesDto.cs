using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArena.BackendServer.Models.Natives.Frontend
{
    public class NativesDto
    {
        public List<ActorDto> Actors { get; set; }
        public List<BuffDto> Buffs { get; set; }
        public List<DecorationDto> Decorations { get; set; }
        public List<SkillDto> Skills { get; set; }
        public List<SpecEffectDto> SpecEffects { get; set; }
        public List<TileDto> Tiles { get; set; }
    }
}
