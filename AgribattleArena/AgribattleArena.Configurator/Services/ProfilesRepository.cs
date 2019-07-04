using AgribattleArena.Configurator.Models;
using AgribattleArena.DBProvider.Contexts;
using AgribattleArena.DBProvider.Contexts.ProfileEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgribattleArena.Configurator.Services
{
    class ProfilesRepository: IRepository<RevelationLevelDto>
    {
        ProfilesContext _context;

        public ProfilesRepository(ProfilesContext context)
        {
            _context = context;
        }

        public async Task<Response> Add(RevelationLevelDto level)
        {
            if (_context.RevelationLevel.Where(x => x.Level == level.Level).Count() == 0)
            {
                await _context.RevelationLevel.AddAsync(AutoMapper.Mapper.Map<RevelationLevel>(level));
                await _context.SaveChangesAsync();
                return Response.Success;
            }
            return Response.NoChanges;
        }

        public async Task<Response> Update(RevelationLevelDto level)
        {
            IEnumerable<RevelationLevel> levelsToChange;
            if ((levelsToChange = _context.RevelationLevel.Where(x => x.Level == level.Level)).Count() > 0)
            {
                foreach(var levelToChange in levelsToChange)
                { 
                    levelToChange.Revelations = level.Revelations;
                }
                await _context.SaveChangesAsync();
                return Response.Success;
            }
            return Response.NoChanges;
        }

        public async Task<Response> Delete(RevelationLevelDto level)
        {
            IEnumerable<RevelationLevel> levelsToDelete;
            if ((levelsToDelete = _context.RevelationLevel.Where(x => x.Level == level.Level)).Count() > 0)
            {
                _context.RevelationLevel.RemoveRange(levelsToDelete);
                await _context.SaveChangesAsync();
                return Response.Success;
            }
            return Response.NoChanges;
        }
    }
}
