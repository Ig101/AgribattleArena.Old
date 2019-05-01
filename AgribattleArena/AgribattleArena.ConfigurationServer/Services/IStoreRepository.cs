using AgribattleArena.ConfigurationServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArena.ConfigurationServer.Services
{
    public interface IStoreRepository
    {
        Task<bool> AddNewActor(ActorToAddDto actor);
    }
}
