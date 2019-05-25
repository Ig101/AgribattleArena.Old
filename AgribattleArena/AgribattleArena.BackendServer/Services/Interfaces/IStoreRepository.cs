using AgribattleArena.DBProvider.Contexts.StoreEntities;
using AgribattleArena.BackendServer.Models.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArena.BackendServer.Services.Interfaces
{
    public interface IStoreRepository
    {
        Task<OfferDto> CreateNewOffer(string profileId);
        Task<OfferDto> GetOffer(string profileId);
        Task<ActorBoughtDto> BuyActor(string profileId, int money, int offerId, int offerItemId);
        Task AcceptTransaction(string profileId, int actorId, int actorCost);
        Task DeclineTransaction(int offerId);
        Task<ActorBoughtDto> GetActorByName(string unitName);
    }
}
