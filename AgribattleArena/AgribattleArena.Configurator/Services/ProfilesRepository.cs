using AgribattleArena.DBProvider.Contexts;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgribattleArena.Configurator.Services
{
    class ProfilesRepository
    {
        ProfilesContext _context;

        public ProfilesRepository(ProfilesContext context)
        {
            _context = context;
        }
    }
}
