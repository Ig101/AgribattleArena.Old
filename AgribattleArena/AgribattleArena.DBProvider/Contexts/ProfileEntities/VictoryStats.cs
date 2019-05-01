using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArena.DBProvider.Contexts.ProfileEntities
{
    public class VictoryStats
    {
        [Key]
        public int Id { get; set; }
        public int Count { get; set; }
        public DateTime AggregatedDate { get; set; }

        public string ProfileId { get; set; }
        [ForeignKey("ProfileId")]
        public Profile Profile { get; set; }
    }
}
