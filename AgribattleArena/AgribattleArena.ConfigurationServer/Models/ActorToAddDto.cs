using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArena.ConfigurationServer.Models
{
    public class ActorToAddDto
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        [Required]
        [MaxLength(30)]
        public string ActorNative { get; set; }
        [Required]
        [MaxLength(30)]
        public string AttackingSkillNative { get; set; }
        public List<string> Skills { get; set; }
        public int Strength { get; set; }
        public int Willpower { get; set; }
        public int Constitution { get; set; }
        public int Speed { get; set; }
        public int ActionPointsIncome { get; set; }
        public int Cost { get; set; }
    }
}
