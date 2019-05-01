using AgribattleArena.DBProvider.Contexts.ProfileEntities;
using AgribattleArena.BackendServer.Models.Authorization;
using AgribattleArena.BackendServer.Models.Profile;
using AgribattleArena.BackendServer.Services;
using AgribattleArena.BackendServer.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArena.BackendServer.Controllers
{
    [Route("api/auth")]
    public class AuthorizationController : ControllerBase
    {
        UserManager<Profile> _userManager;
        SignInManager<Profile> _signInManager;
        ConstantsConfig _constants;

        public AuthorizationController(ProfilesService userManager,
            SignInManager<Profile> signInManager, IOptions<ConstantsConfig> constants)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _constants = constants.Value;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]RegisterDto registrationModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Profile newUser = new Profile()
            {
                UserName = registrationModel.Login,
                Email = registrationModel.Email,
                Resources = _constants.StartResourcesAmount,
                DonationResources = _constants.StartDonationResourcesAmount,
                Revelations = 0
            };
            var result = await _userManager.CreateAsync(newUser, registrationModel.Password);
            if (result.Succeeded)
            {
                return CreatedAtRoute("GetProfileWithInfo", AutoMapper.Mapper.Map<ProfileDto>(newUser));
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(error.Code, error.Description);
            }
            return BadRequest(ModelState);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]AuthorizeDto authorizationModel)
        {
            var result = await _signInManager.PasswordSignInAsync(authorizationModel.Login, authorizationModel.Password, false, false);
            if(result.Succeeded)
            {
                return NoContent();
            }
            return Unauthorized();
        }
    }
}
