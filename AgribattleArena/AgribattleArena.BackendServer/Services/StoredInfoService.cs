using AgribattleArena.BackendServer.Models.Profile;
using AgribattleArena.BackendServer.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArena.BackendServer.Services
{
    public class StoredInfoService: IStoredInfoService, IStoredInfoServiceGenerator
    {
        ILogger<StoredInfoService> _logger;

        public int RevelationsMemory { get; set; }

        public StoredInfoService(ILogger<StoredInfoService> logger)
        {
            _logger = logger;
        }

        void SetupRevelationsMemory(IEnumerable<ProfileInfoDto> profiles)
        {
            _logger.Log(LogLevel.Information, "Setup revelations memory");
            RevelationsMemory = 0;
            foreach(var profile in profiles)
            {
                RevelationsMemory += profile.Revelations;
            }
        }

        public void SetupNew(IServiceProvider services)
        {
            IEnumerable<ProfileInfoDto> profiles = services.GetRequiredService<IProfilesServiceAggregated>().GetAllProfiles();
            SetupRevelationsMemory(profiles);
        }
    }
}
