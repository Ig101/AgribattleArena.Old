using AgribattleArena.BackendServer.Contexts;
using AgribattleArena.BackendServer.Contexts.StoreEntities;
using AgribattleArena.BackendServer.Models.Store;
using AgribattleArena.BackendServer.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArena.BackendServer.Services
{
    public class StoreRepository: IStoreRepository
    {
        StoreContext _context;
        ILogger<StoreRepository> _logger;
        ConstantsConfig _constants;
        Random _random;

        public StoreRepository(StoreContext context, Random random, IOptions<ConstantsConfig> constants, ILogger<StoreRepository> logger)
        {
            _random = random;
            _context = context;
            _logger = logger;
            _constants = constants.Value;
        }

        public async Task<Offer> CreateNewOffer (string profileId)
        {
            DateTime offerExpirationDate = DateTime.Today.AddHours(_constants.OfferUpdateHour);
            if (DateTime.Now.Hour >= _constants.OfferUpdateHour - 1)
                offerExpirationDate.AddDays(1);
            Offer offer = new Offer()
            {
                DateFrom = DateTime.Now,
                DateTo = offerExpirationDate,
                Closed = false,
                ProfileId = profileId
            };
            IEnumerable<Actor> actors = _context.Actors.OrderBy(o => _random.Next()).Take(_constants.AmountOfOfferedActors);
            offer.Items.AddRange(actors.Select(actor => new OfferItem()
            {
                Offer = offer,
                Actor = actor
            }));
            await _context.Offers.AddAsync(offer);
            await _context.SaveChangesAsync();
            return offer;
        }

        public async Task<Offer> GetOffer (string profileId)
        {
            IEnumerable<Offer> offers = _context.Offers.Where(x => x.DateTo > DateTime.Now && !x.Closed);
            if(offers.Count() == 1)
            {
                return offers.First();
            }
            else if(offers.Count() == 0)
            {
                return await CreateNewOffer(profileId);
            }
            throw new ArgumentOutOfRangeException();
        }

        public async Task<Actor> BuyActor (string profileId, int money, int offerId, int offerItemId)
        {
            Offer offer = _context.Offers.Find(offerId);
            if (!offer.Closed && offer.DateTo > DateTime.Now)
            {
                OfferItem item = offer.Items.Find(x => x.Id == offerItemId);
                if (item != null && money >= item.Actor.Cost)
                {
                    offer.Closed = true;
                    await _context.ActorTransactions.AddAsync(new ActorTransaction()
                    {
                        Actor = item.Actor,
                        ProfileId = profileId,
                        Sum = item.Actor.Cost
                    });
                    await _context.SaveChangesAsync();
                    return item.Actor;
                }
            }
            return null;
        }

        public async Task AddNewActor(ActorToAddDto actor)
        {
            await _context.Actors.AddAsync(AutoMapper.Mapper.Map<Actor>(actor));
        }
    }
}
