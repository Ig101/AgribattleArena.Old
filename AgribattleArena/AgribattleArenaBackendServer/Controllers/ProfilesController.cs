using AgribattleArenaBackendServer.Contexts.ProfilesEntities;
using AgribattleArenaBackendServer.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Controllers
{
    [Route("api/profiles")]
    public class ProfilesController: ControllerBase
    {
        UserManager<Profile> userManager;
        SignInManager<Profile> signInManager;

        public ProfilesController(ProfilesService userManager,
            SignInManager<Profile> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpPost]
        public async Task<IActionResult> Register()
        {
            var result = await userManager.CreateAsync(null);
            return null;
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login()
        {
            
            var result = await signInManager.PasswordSignInAsync("11", "22", false, false);
            return null;
        }
    }
}
