using AgribattleArena.Configurator.Models;
using AgribattleArena.DBProvider.Contexts;
using AgribattleArena.DBProvider.Contexts.StoreEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgribattleArena.Configurator.Services
{
    class StoreRepository
    {
        StoreContext _context;

        public StoreRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<bool> AddNewActor(StoreActorDto actor)
        {
            if (_context.Actor.Where(x => x.Name == actor.Name).Count() == 0)
            {
                await _context.Actor.AddAsync(AutoMapper.Mapper.Map<Actor>(actor));
                return true;
            }
            return false;
        }
    }
}
