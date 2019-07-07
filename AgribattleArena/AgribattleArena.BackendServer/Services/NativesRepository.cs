using AgribattleArena.DBProvider.Contexts;
using AgribattleArena.BackendServer.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgribattleArena.Engine.ForExternalUse;
using AgribattleArena.Engine.ForExternalUse.EngineHelper;
using Microsoft.EntityFrameworkCore;
using Frontend = AgribattleArena.BackendServer.Models.Natives.Frontend;

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

        public INativeManager CreateBackendNativesManager()
        {
            var nativeManager = EngineHelper.CreateNativeManager();
            var decorations = _context.Decoration
                .Include(x => x.Armor)
                .Include(x => x.Action)
                .Include(x => x.OnDeathAction)
                .Include(x => x.Tags)
                .ToList();
            var actors = _context.Actor
                .Include(x => x.Tags)
                .Include(x => x.Armor)
                .ToList();
            var roleModels = _context.RoleModel
                .Include(x => x.AttackingSkill)
                .Include(x => x.RoleModelSkills).ThenInclude(x => x.Skill);
            var specEffects = _context.SpecEffect
                .Include(x => x.Tags)
                .Include(x => x.Action)
                .Include(x => x.OnDeathAction);
            var buffs = _context.Buff
                .Include(x => x.BuffApplier)
                .Include(x => x.Action)
                .Include(x => x.OnPurgeAction)
                .Include(x => x.Tags);
            var skills = _context.Skill
                .Include(x => x.Action)
                .Include(x => x.Tags);
            var tiles = _context.Tile
                .Include(x => x.Action)
                .Include(x => x.OnStepAction)
                .Include(x => x.Tags);
            foreach(var skill in skills)
            {
                nativeManager.AddSkillNative(skill.Name, skill.Tags.Select(x => x.Name).ToArray(), skill.Range, skill.Cost, skill.Cd, 
                    skill.Mod, skill.Action.Select(x => x.Name));
            }
            foreach(var tile in tiles)
            {
                nativeManager.AddTileNative(tile.Name, tile.Tags.Select(x => x.Name).ToArray(), tile.Flat, tile.DefaultHeight, tile.Unbearable,
                    tile.Mod, tile.Action.Select(x => x.Name), tile.OnStepAction.Select(x => x.Name));
            }
            foreach(var buff in buffs)
            {
                nativeManager.AddBuffNative(buff.Name, buff.Tags.Select(x => x.Name).ToArray(), buff.Eternal, buff.Repeatable, buff.SummarizeLength,
                    buff.Duration, buff.Mod, buff.Action.Select(x => x.Name), buff.BuffApplier.Select(x => x.Name), 
                    buff.OnPurgeAction.Select(x => x.Name));
            }
            foreach(var actor in actors)
            {
                nativeManager.AddActorNative(actor.Name, actor.Tags.Select(x => x.Name).ToArray(), actor.Z,
                    actor.Armor.Select(x => new Engine.Helpers.TagSynergy(x.SelfTag, x.TargetTag, x.Mod)).ToArray());
            }
            foreach(var decoration in decorations)
            {
                nativeManager.AddDecorationNative(decoration.Name, decoration.Tags.Select(x => x.Name).ToArray(),
                    decoration.Armor.Select(x => new Engine.Helpers.TagSynergy(x.SelfTag, x.TargetTag, x.Mod)).ToArray(), decoration.Health, 
                    decoration.Z, decoration.Mod, decoration.Action.Select(x => x.Name), decoration.OnDeathAction.Select(x => x.Name));
            }
            foreach(var specEffect in specEffects)
            {
                nativeManager.AddEffectNative(specEffect.Name, specEffect.Tags.Select(x => x.Name).ToArray(), specEffect.Z,
                    specEffect.Duration, specEffect.Mod, specEffect.Action.Select(x => x.Name), specEffect.OnDeathAction.Select(x => x.Name));
            }
            foreach(var roleModel in roleModels)
            {
                nativeManager.AddRoleModelNative(roleModel.Name, roleModel.Strength, roleModel.Willpower, roleModel.Constitution,
                    roleModel.Speed, roleModel.ActionPointsIncome, roleModel.AttackingSkill.Name,
                    roleModel.RoleModelSkills.Select(x => x.Skill.Name).ToArray());
            }
            return nativeManager;
        }

        public async Task<Frontend.NativesDto> GetFrontendNatives()
        {
            //TODO From context
            var actors = new List<Frontend.ActorDto>();
            var decorations = new List<Frontend.DecorationDto>();
            var effects = new List<Frontend.SpecEffectDto>();
            var skills = new List<Frontend.SkillDto>();
            var buffs = new List<Frontend.BuffDto>();
            var tiles = new List<Frontend.TileDto>();
            return new Frontend.NativesDto()
            {
                Actors = actors,
                Decorations = decorations,
                SpecEffects = effects,
                Skills = skills,
                Buffs = buffs,
                Tiles = tiles
            };
        }
    }
}
