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
using Microsoft.EntityFrameworkCore;

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
                offerExpirationDate = offerExpirationDate.AddDays(1);
            Offer offer = new Offer()
            {
                DateFrom = DateTime.Now,
                DateTo = offerExpirationDate,
                Closed = false,
                ProfileId = profileId,
                Items = new List<OfferItem>()
            };
            IEnumerable<Actor> actors = (await _context.Actor.Include(x => x.Skills).ToArrayAsync())
                .OrderBy(o => _random.Next()).Take(_constants.AmountOfOfferedActors);
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
            IEnumerable<Offer> offers = _context.Offer
                .Include(t => t.Items)
                .ThenInclude(t => t.Actor)
                .ThenInclude(t => t.Skills)
                .Where(x => x.DateTo > DateTime.Now && !x.Closed);
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

        public async Task<ActorBoughtDto> BuyActor (string profileId, int money, int offerId, int offerItemId)
        {
            Offer offer = _context.Offer
                .Include(t => t.Items)
                .ThenInclude(t => t.Actor)
                .ThenInclude(t => t.Skills)
                .FirstOrDefault(x => x.Id == offerId);
            if (!offer.Closed && offer.DateTo > DateTime.Now)
            {
                OfferItem item = offer.Items.Find(x => x.Id == offerItemId);
                if (item == null)
                {
                    return new ActorBoughtDto()
                    {
                        Error = "Actor not exists"
                    };
                }
                if (money >= item.Actor.Cost)
                {
                    offer.Closed = true;
                    await _context.SaveChangesAsync();
                    return new ActorBoughtDto(){
                        Actor = AutoMapper.Mapper.Map<ActorDto>(item.Actor)
                    };
                }
                else
                {
                    return new ActorBoughtDto()
                    {
                        Error = "Not enough money"
                    };
                }
            }
            return new ActorBoughtDto()
            {
                Error = "Offer not exists"
            };
        }

        public async Task AcceptTransaction(string profileId, int actorId, int actorCost)
        {
            await _context.ActorTransaction.AddAsync(new ActorTransaction()
            {
                ItemId = actorId,
                ProfileId = profileId,
                Sum = actorCost
            });
            await _context.SaveChangesAsync();
        }

        public async Task DeclineTransaction(int offerId)
        {
            Offer offer = _context.Offer
                .FirstOrDefault(x => x.Id == offerId);
            offer.Closed = false;
            await _context.SaveChangesAsync();
        }
    }
}
