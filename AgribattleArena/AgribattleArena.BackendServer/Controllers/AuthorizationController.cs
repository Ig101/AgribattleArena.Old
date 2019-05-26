﻿using AgribattleArena.DBProvider.Contexts.ProfileEntities;
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
using Microsoft.AspNetCore.Authorization;
using AgribattleArena.BackendServer.Models.Store;
using Microsoft.Extensions.Logging;

namespace AgribattleArena.BackendServer.Controllers
{
    [Route("api/auth")]
    public class AuthorizationController : ControllerBase
    {
        UserManager<Profile> _userManager;
        SignInManager<Profile> _signInManager;
        ConstantsConfig _constants;
        IStoreRepository _storeRepository;
        ILogger<AuthorizationController> _logger;

        public AuthorizationController(ProfilesService userManager, ILogger<AuthorizationController> logger,
            SignInManager<Profile> signInManager, IOptions<ConstantsConfig> constants,
            IStoreRepository storeRepository)
        {
            _storeRepository = storeRepository;
            _userManager = userManager;
            _signInManager = signInManager;
            _constants = constants.Value;
            _logger = logger;
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
                Revelations = 0,
                BarracksSize = _constants.StartProfileActorsLimit,
            };
            #region Add start squad
            newUser.Actors = new List<Actor>();
            newUser.Resources = 0;
            for (int i = 0; i < 5; i++)
            {
                _logger.LogInformation("Buy password");
                ActorBoughtDto newActor = await _storeRepository.GetActorByName("sorcerer");
                _logger.LogInformation(newActor.ToString());
                var actorToAdd = new Actor()
                {
                    Name = "sorc_" + i,
                    ActionPointsIncome = newActor.Actor.ActionPointsIncome,
                    ActorNative = newActor.Actor.ActorNative,
                    AttackingSkillNative = newActor.Actor.AttackingSkillNative,
                    Constitution = newActor.Actor.Constitution,
                    InParty = true,
                    Skills = AutoMapper.Mapper.Map<List<DBProvider.Contexts.ProfileEntities.Skill>>(newActor.Actor.Skills),
                    Speed = newActor.Actor.Speed,
                    Strength = newActor.Actor.Strength,
                    Willpower = newActor.Actor.Willpower
                };
                _logger.LogInformation("lalala");
                newUser.Actors.Add(actorToAdd);
            }
            #endregion
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
            var result = await _signInManager.PasswordSignInAsync(authorizationModel.Login, authorizationModel.Password, true, false);
            if(result.Succeeded)
            {
                return Ok();
            }
            return Unauthorized();
        }

        [HttpDelete("login")]
        public IActionResult LogOff()
        {
            Response.Cookies.Delete(".AspNetCore.Identity.Application");
            return Ok();
        }

        [HttpGet()]
        [Authorize]
        public IActionResult IsAuthorized()
        {
            return NoContent();
        }
    }
}
