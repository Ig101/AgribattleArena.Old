using System;
using System.Collections.Generic;
using System.Text;

namespace AgribattleArena.Configurator.Models.Natives
{
    class BackendNativesDto
    {
        public IEnumerable<BackendActorDto> Actors { get; set; }
        public IEnumerable<BackendBuffDto> Buffs { get; set; }
        public IEnumerable<BackendDecorationDto> Decorations { get; set; }
        public IEnumerable<BackendRoleModelDto> RoleModels { get; set; }
        public IEnumerable<BackendSkillDto> Skills { get; set; }
        public IEnumerable<BackendSpecEffectDto> SpecEffects { get; set; }
        public IEnumerable<BackendTileDto> Tiles { get; set; }
    }
}
