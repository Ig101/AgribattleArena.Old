using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArena.BackendServer.Contexts.StoreEntities
{
    public class Offer
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string ProfileId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public bool Closed { get; set; }

        public List<OfferItem> Items { get; set; }
    }
}
