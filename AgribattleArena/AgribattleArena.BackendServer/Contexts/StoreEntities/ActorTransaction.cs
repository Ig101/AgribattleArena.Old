using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArena.BackendServer.Contexts.StoreEntities
{
    public class ActorTransaction
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string ProfileId { get; set; }
        public int Sum { get; set; }

        public int ActorId { get; set; }
        [ForeignKey("ActorId")]
        public Actor Actor { get; set; }
    }
}
