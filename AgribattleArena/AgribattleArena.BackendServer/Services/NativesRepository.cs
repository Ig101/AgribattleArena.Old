using AgribattleArena.DBProvider.Contexts;
using AgribattleArena.BackendServer.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArena.BackendServer.Services
{
    public class NativesRepository: INativesRepository
    {
        NativesContext _context;
        ILogger<NativesRepository> _logger;
        ConstantsConfig _constants;
        Random _random;

        public NativesRepository(NativesContext context, Random random, IOptions<ConstantsConfig> constants, ILogger<NativesRepository> logger)
        {
            _random = random;
            _context = context;
            _logger = logger;
            _constants = constants.Value;
        }


    }
}
