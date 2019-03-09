using AgribattleArenaBackendServer.Contexts.NativesEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Models.Natives
{
    public class RoleModelNativeToAddDto: TaggingNativeDto
    {
        public string AttackSkill { get; set; }
        public int Strength { get; set; }
        public int Willpower { get; set; }
        public int Constitution { get; set; }
        public int Speed { get; set; }
        public List<TagSynergy> Armor { get; set; }
        public List<string> Skills { get; set; }
        public int ActionPointsIncome { get; set; }
    }
}
