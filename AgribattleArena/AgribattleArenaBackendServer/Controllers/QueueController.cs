using AgribattleArenaBackendServer.Models.Battle;
using AgribattleArenaBackendServer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Controllers
{
    [Authorize]
    [Route("api/queue")]
    public class QueueController: ControllerBase
    {
        IProfilesService profilesService;
        IQueueingStorageService queueingService;

        public QueueController(IQueueingStorageService queueingService, IProfilesService profilesService)
        {
            this.profilesService = profilesService;
            this.queueingService = queueingService;
        }

        [HttpPost]
        public async Task<IActionResult> EnqueueNewPlayer([FromBody]QueueRequestDto queueRequest)
        {
            if (!ModelState.IsValid) return BadRequest();
            string userId = (await profilesService.GetProfile(User)).Id;
            if (queueingService.Enqueue(queueRequest.Mode, new BattleUserDto()
            {
                PartyId = queueRequest.PartyId,
                UserId = userId
            }))
            {
                return NoContent();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DequeuePlayer()
        {
            string userId = (await profilesService.GetProfile(User)).Id;
            if (queueingService.Dequeue(userId))
            {
                return NoContent();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
