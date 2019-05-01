using AgribattleArena.ConfigurationServer.Models;
using AgribattleArena.DBProvider.Contexts;
using AgribattleArena.DBProvider.Contexts.StoreEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArena.ConfigurationServer.Services
{
    public class StoreRepository: IStoreRepository
    {
        StoreContext _context;

        public StoreRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<bool> AddNewActor(ActorToAddDto actor)
        {
            if(_context.Actor.Where(x => x.Name == actor.Name).Count()==0)
            {
                await _context.Actor.AddAsync(AutoMapper.Mapper.Map<Actor>(actor));
                return true;
            }
            return false;
        }
    }
}
