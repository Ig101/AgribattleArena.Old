using AgribattleArena.DBProvider.Contexts;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgribattleArena.Configurator.Services
{
    class NativesRepository
    {
        NativesContext _context;

        public NativesRepository(NativesContext context)
        {
            _context = context;
        }
    }
}
