using AgribattleArenaBackendServer.Models.Battle;
using AgribattleArenaBackendServer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Controllers
{
    [Authorize]
    [Route("api/battle")]
    public class EngineController: ControllerBase
    {
        IProfilesService profilesService;
        IEngineService engineService;

        public EngineController(IEngineService engineService, IProfilesService profilesService)
        {
            this.profilesService = profilesService;
            this.engineService = engineService;
        }
    }
}
