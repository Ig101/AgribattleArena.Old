using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Contexts.ProfilesEntities
{
    public class Actor
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(30)]
        [Required]
        public string ActorNative { get; set; }
        [MaxLength(30)]
        [Required]
        public string AttackingSkillNative { get; set; }
        public List<Skill> Skills { get; set; }
        public int Strength { get; set; }
        public int Willpower { get; set; }
        public int Constitution { get; set; }
        public int Speed { get; set; }
        public int ActionPointsIncome { get; set; }
        public List<TagsArmor> Armor { get; set; }

        public int PartyId { get; set; }
        [ForeignKey("PartyId")]
        public Party Party { get; set; }
    }
}
