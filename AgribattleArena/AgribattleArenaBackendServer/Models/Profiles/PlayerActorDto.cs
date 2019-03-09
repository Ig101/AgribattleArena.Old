using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Models.Profiles
{
    public class PlayerActorDto
    {
        public string ActorNative { get; set; }
        public string AttackingSkillNative { get; set; }
        public List<string> Skills { get; set; }
        public int Strength { get; set; }
        public int Willpower { get; set; }
        public int Constitution { get; set; }
        public int Speed { get; set; }
        public int ActionPointsIncome { get; set; }
        public List<TagsArmorDto> Armor { get; set; }
    }
}
