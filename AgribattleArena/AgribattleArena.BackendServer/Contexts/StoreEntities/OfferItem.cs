using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArena.BackendServer.Contexts.StoreEntities
{
    public class OfferItem
    {
        [Key]
        public int Id { get; set; }
        public int OfferId { get; set; }
        [ForeignKey("OfferId")]
        public Offer Offer { get; set; }

        public int ActorId { get; set; }
        [ForeignKey("ActorId")]
        public Actor Actor { get; set; }
    }
}
