using AgribattleArena.BackendServer.Contexts.StoreEntities;
using AgribattleArena.BackendServer.Models.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArena.BackendServer.Services.Interfaces
{
    public interface IStoreRepository
    {
        Task<Offer> CreateNewOffer(string profileId);
        Task<Offer> GetOffer(string profileId);
        Task<Actor> BuyActor(string profileId, int money, int offerId, int offerItemId);

        Task<bool> AddNewActor(ActorToAddDto actor);
    }
}
