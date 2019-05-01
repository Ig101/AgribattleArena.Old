using AgribattleArena.DBProvider.Contexts;
using AgribattleArena.DBProvider.Contexts.StoreEntities;
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

        public async Task<OfferDto> CreateNewOffer (string profileId)
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
            IEnumerable<Actor> actors = _context.Actor.OrderBy(o => _random.Next()).Take(_constants.AmountOfOfferedActors);
            offer.Items.AddRange(actors.Select(actor => new OfferItem()
            {
                Offer = offer,
                Actor = actor
            }));
            await _context.Offer.AddAsync(offer);
            await _context.SaveChangesAsync();
            return AutoMapper.Mapper.Map<OfferDto>(offer);
        }

        public async Task<OfferDto> GetOffer (string profileId)
        {
            IEnumerable<Offer> offers = _context.Offer.Where(x => x.DateTo > DateTime.Now && !x.Closed);
            if(offers.Count() == 1)
            {
                return AutoMapper.Mapper.Map<OfferDto>(offers.First());
            }
            else if(offers.Count() == 0)
            {
                return await CreateNewOffer(profileId);
            }
            throw new ArgumentOutOfRangeException();
        }

        public async Task<ActorDto> BuyActor (string profileId, int money, int offerId, int offerItemId)
        {
            Offer offer = _context.Offer.Find(offerId);
            if (!offer.Closed && offer.DateTo > DateTime.Now)
            {
                OfferItem item = offer.Items.Find(x => x.Id == offerItemId);
                if (item != null && money >= item.Actor.Cost)
                {
                    offer.Closed = true;
                    await _context.ActorTransaction.AddAsync(new ActorTransaction()
                    {
                        Actor = item.Actor,
                        ProfileId = profileId,
                        Sum = item.Actor.Cost
                    });
                    await _context.SaveChangesAsync();
                    return AutoMapper.Mapper.Map<ActorDto>(item.Actor);
                }
            }
            return null;
        }
    }
}
