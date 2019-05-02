using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArena.BackendServer.Services
{
    public class ConstantsConfig
    {
        public int StartResourcesAmount { get; set; }
        public int StartDonationResourcesAmount { get; set; }
        public int StartProfileActorsLimit { get; set; }

        public int AmountOfOfferedActors { get; set; }
        public int OfferUpdateHour { get; set; }

        public int QueueTimeout { get; set; }
        public int QueueRevelationLevelCompareLimit { get; set; }
        public int QueueRevelationLevelCompareLimitAfterTimeout { get; set; }
    }
}
