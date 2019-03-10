using AgribattleArenaBackendServer.Contexts.ProfilesEntities;
using AgribattleArenaBackendServer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Controllers
{
    [Route("api/login")]
    public class AuthorizationController: ControllerBase
    {
        UserManager<Profile> userManager;
        SignInManager<Profile> signInManager;

        public AuthorizationController(ProfilesService userManager,
            SignInManager<Profile> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register()
        {
            var result = await userManager.CreateAsync(null);
            return null;
        }


        [HttpPost]
        public async Task<IActionResult> Login()
        {
            
            var result = await signInManager.PasswordSignInAsync("11", "22", false, false);
            return null;
        }
    }
}
