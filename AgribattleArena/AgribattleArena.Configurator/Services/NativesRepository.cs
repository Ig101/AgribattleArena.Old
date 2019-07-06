using AgribattleArena.Configurator.Models.Natives;
using AgribattleArena.DBProvider.Contexts;
using AgribattleArena.DBProvider.Contexts.NativeEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgribattleArena.Configurator.Services
{
    class NativesRepository: IRepository<BackendActorDto>, IRepository<BackendDecorationDto>, IRepository<BackendBuffDto>, IRepository<BackendTileDto>
        , IRepository<BackendSpecEffectDto>, IRepository<BackendSkillDto>, IRepository<BackendRoleModelDto>
    {
        NativesContext _context;

        public NativesRepository(NativesContext context)
        {
            _context = context;
        }

        public async Task<Response> Add(BackendActorDto entity)
        {
            if (_context.Actor.Where(x => x.Name == entity.Name).Count() == 0)
            {
                if (entity.Armor != null &&
                    entity.Z != null &&
                    entity.Tags != null
                    )
                {
                    await _context.Actor.AddAsync(AutoMapper.Mapper.Map<Actor>(entity));
                    await _context.SaveChangesAsync();
                    return Response.Success;
                }
                else
                {
                    return Response.Error;
                }
            }
            return Response.NoChanges;
        }

        public async Task<Response> Add(BackendSkillDto entity)
        {
            if (_context.Skill.Where(x => x.Name == entity.Name).Count() == 0)
            {
                if (entity.Tags != null &&
                    entity.Range != null &&
                    entity.Action != null &&
                    entity.Cd != null &&
                    entity.Cost != null
                    )
                {
                    await _context.Skill.AddAsync(AutoMapper.Mapper.Map<Skill>(entity));
                    await _context.SaveChangesAsync();
                    return Response.Success;
                }
                else
                {
                    return Response.Error;
                }
            }
            return Response.NoChanges;
        }

        public async Task<Response> Add(BackendSpecEffectDto entity)
        {
            if (_context.SpecEffect.Where(x => x.Name == entity.Name).Count() == 0)
            {
                if (entity.Tags != null &&
                    entity.Z != null
                    )
                {
                    await _context.SpecEffect.AddAsync(AutoMapper.Mapper.Map<SpecEffect>(entity));
                    await _context.SaveChangesAsync();
                    return Response.Success;
                }
                else
                {
                    return Response.Error;
                }
            }
            return Response.NoChanges;
        }

        public async Task<Response> Add(BackendRoleModelDto entity)
        {
            if (_context.RoleModel.Where(x => x.Name == entity.Name).Count() == 0)
            {
                if (entity.ActionPointsIncome != null &&
                    entity.AttackingSkill != null &&
                    entity.Constitution != null &&
                    entity.RoleModelSkills != null &&
                    entity.Speed != null &&
                    entity.Strength != null &&
                    entity.Willpower != null
                    )
                {
                    var roleModel = AutoMapper.Mapper.Map<RoleModel>(entity);
                    roleModel.AttackingSkill = _context.Skill.FirstOrDefault(x => x.Name == entity.AttackingSkill);
                    foreach (var skill in entity.RoleModelSkills) {
                        roleModel.RoleModelSkills.Add(new RoleModelSkill()
                        {
                            Skill = _context.Skill.FirstOrDefault(x => x.Name == skill)
                        });
                    }
                    await _context.RoleModel.AddAsync(roleModel);
                    await _context.SaveChangesAsync();
                    return Response.Success;
                }
                else
                {
                    return Response.Error;
                }
            }
            return Response.NoChanges;
        }

        public async Task<Response> Add(BackendTileDto entity)
        {
            if (_context.Tile.Where(x => x.Name == entity.Name).Count() == 0)
            {
                if (entity.Tags != null &&
                    entity.Flat != null &&
                    entity.Unbearable != null
                    )
                {
                    await _context.Tile.AddAsync(AutoMapper.Mapper.Map<Tile>(entity));
                    await _context.SaveChangesAsync();
                    return Response.Success;
                }
                else
                {
                    return Response.Error;
                }
            }
            return Response.NoChanges;
        }

        public async Task<Response> Add(BackendBuffDto entity)
        {
            if (_context.Buff.Where(x => x.Name == entity.Name).Count() == 0)
            {
                if (entity.Tags != null &&
                    entity.Repeatable != null &&
                    entity.SummarizeLength != null &&
                    entity.Eternal != null
                    )
                {
                    await _context.Buff.AddAsync(AutoMapper.Mapper.Map<Buff>(entity));
                    await _context.SaveChangesAsync();
                    return Response.Success;
                }
                else
                {
                    return Response.Error;
                }
            }
            return Response.NoChanges;
        }

        public async Task<Response> Add(BackendDecorationDto entity)
        {
            if (_context.Decoration.Where(x => x.Name == entity.Name).Count() == 0)
            {
                if (entity.Tags != null &&
                    entity.Armor != null &&
                    entity.Health != null &&
                    entity.Z != null
                    )
                {
                    await _context.Decoration.AddAsync(AutoMapper.Mapper.Map<Decoration>(entity));
                    await _context.SaveChangesAsync();
                    return Response.Success;
                }
                else
                {
                    return Response.Error;
                }
            }
            return Response.NoChanges;
        }

        public async Task<Response> Delete(BackendActorDto entity)
        {
            if (entity == null) return Response.Error;
            IEnumerable<Actor> entitiesToDelete;
            if ((entitiesToDelete = _context.Actor.Where(x => x.Name == entity.Name).Include(x => x.Armor).Include(x => x.Tags)).Count() > 0)
            {
                foreach (var entityToDelete in entitiesToDelete)
                {
                    _context.Tag.RemoveRange(entityToDelete.Tags);
                    _context.TagSynergy.RemoveRange(entityToDelete.Armor);
                    _context.Actor.Remove(entityToDelete);
                }
                await _context.SaveChangesAsync();
                return Response.Success;
            }
            return Response.NoChanges;
        }

        public async Task<Response> Delete(BackendSkillDto entity)
        {
            if (entity == null) return Response.Error;
            IEnumerable<Skill> entitiesToDelete;
            if ((entitiesToDelete = _context.Skill.Where(x => x.Name == entity.Name).Include(x => x.RoleModelSkills).Include(x => x.Tags)
                    .Include(x => x.Action)).Count() > 0)
            {
                foreach (var entityToDelete in entitiesToDelete)
                {
                    _context.Action.RemoveRange(entityToDelete.Action);
                    _context.Tag.RemoveRange(entityToDelete.Tags);
                    _context.Skill.Remove(entityToDelete);
                }
                await _context.SaveChangesAsync();
                return Response.Success;
            }
            return Response.NoChanges;
        }

        public async Task<Response> Delete(BackendSpecEffectDto entity)
        {
            if (entity == null) return Response.Error;
            IEnumerable<SpecEffect> entitiesToDelete;
            if ((entitiesToDelete = _context.SpecEffect.Where(x => x.Name == entity.Name).Include(x => x.Tags).Include(x => x.OnDeathAction)
                .Include(x => x.Action)).Count() > 0)
            {
                foreach (var entityToDelete in entitiesToDelete)
                {
                    _context.Action.RemoveRange(entityToDelete.Action);
                    _context.Action.RemoveRange(entityToDelete.OnDeathAction);
                    _context.Tag.RemoveRange(entityToDelete.Tags);
                    _context.SpecEffect.Remove(entityToDelete);
                }
                await _context.SaveChangesAsync();
                return Response.Success;
            }
            return Response.NoChanges;
        }

        public async Task<Response> Delete(BackendRoleModelDto entity)
        {
            if (entity == null) return Response.Error;
            IEnumerable<RoleModel> entitiesToDelete;
            if ((entitiesToDelete = _context.RoleModel.Where(x => x.Name == entity.Name).Include(x => x.RoleModelSkills)).Count() > 0)
            {
                foreach (var entityToDelete in entitiesToDelete)
                {
                    _context.RoleModel.Remove(entityToDelete);
                }
                await _context.SaveChangesAsync();
                return Response.Success;
            }
            return Response.NoChanges;
        }

        public async Task<Response> Delete(BackendTileDto entity)
        {
            if (entity == null) return Response.Error;
            IEnumerable<Tile> entitiesToDelete;
            if ((entitiesToDelete = _context.Tile.Where(x => x.Name == entity.Name).Include(x => x.Action).Include(x => x.OnStepAction)
                .Include(x => x.Tags)).Count() > 0)
            {
                foreach (var entityToDelete in entitiesToDelete)
                {
                    _context.Action.RemoveRange(entityToDelete.Action);
                    _context.Action.RemoveRange(entityToDelete.OnStepAction);
                    _context.Tag.RemoveRange(entityToDelete.Tags);
                    _context.Tile.Remove(entityToDelete);
                }
                await _context.SaveChangesAsync();
                return Response.Success;
            }
            return Response.NoChanges;
        }

        public async Task<Response> Delete(BackendBuffDto entity)
        {
            if (entity == null) return Response.Error;
            IEnumerable<Buff> entitiesToDelete;
            if ((entitiesToDelete = _context.Buff.Where(x => x.Name == entity.Name).Include(x => x.Action).Include(x => x.BuffApplier)
                .Include(x => x.Tags).Include(x => x.OnPurgeAction)).Count() > 0)
            {
                foreach (var entityToDelete in entitiesToDelete)
                {
                    _context.Action.RemoveRange(entityToDelete.Action);
                    _context.Action.RemoveRange(entityToDelete.BuffApplier);
                    _context.Action.RemoveRange(entityToDelete.OnPurgeAction);
                    _context.Tag.RemoveRange(entityToDelete.Tags);
                    _context.Buff.Remove(entityToDelete);
                }
                await _context.SaveChangesAsync();
                return Response.Success;
            }
            return Response.NoChanges;
        }

        public async Task<Response> Delete(BackendDecorationDto entity)
        {
            if (entity == null) return Response.Error;
            IEnumerable<Decoration> entitiesToDelete;
            if ((entitiesToDelete = _context.Decoration.Where(x => x.Name == entity.Name).Include(x => x.Action).Include(x => x.Armor)
                .Include(x => x.Tags).Include(x => x.OnDeathAction)).Count() > 0)
            {
                foreach (var entityToDelete in entitiesToDelete)
                {
                    _context.Action.RemoveRange(entityToDelete.Action);
                    _context.Action.RemoveRange(entityToDelete.OnDeathAction);
                    _context.TagSynergy.RemoveRange(entityToDelete.Armor);
                    _context.Tag.RemoveRange(entityToDelete.Tags);
                    _context.Decoration.Remove(entityToDelete);
                }
                await _context.SaveChangesAsync();
                return Response.Success;
            }
            return Response.NoChanges;
        }

        public async Task<Response> Update(BackendActorDto entity)
        {
            IEnumerable<Actor> entitiesToChange;
            if ((entitiesToChange = _context.Actor.Where(x => x.Name == entity.Name).Include(x => x.Armor).Include(x => x.Tags)).Count() > 0)
            {
                foreach (var entityToChange in entitiesToChange)
                {
                    if (entity.Armor != null)
                    {
                        _context.TagSynergy.RemoveRange(entityToChange.Armor);
                        entityToChange.Armor = AutoMapper.Mapper.Map<List<TagSynergy>>(entity.Armor);
                    }
                    if (entity.Z != null) entityToChange.Z = entity.Z.Value;
                    if (entity.Tags != null)
                    {
                        _context.Tag.RemoveRange(entityToChange.Tags);
                        entityToChange.Tags = AutoMapper.Mapper.Map<List<Tag>>(entity.Tags);
                    }
                }
                await _context.SaveChangesAsync();
                return Response.Success;
            }
            return Response.NoChanges;
        }

        public async Task<Response> Update(BackendSkillDto entity)
        {
            IEnumerable<Skill> entitiesToChange;
            if ((entitiesToChange = _context.Skill.Where(x => x.Name == entity.Name).Include(x => x.RoleModelSkills).Include(x => x.Tags)
                .Include(x => x.Action)).Count() > 0)
            {
                foreach (var entityToChange in entitiesToChange)
                {
                    if (entity.Action != null)
                    {
                        _context.Action.RemoveRange(entityToChange.Action);
                        entityToChange.Action = AutoMapper.Mapper.Map<List<SceneAction>>(entity.Action);
                    }
                    if (entity.Cd != null) entityToChange.Cd = entity.Cd.Value;
                    if (entity.Cost != null) entityToChange.Cost = entity.Cost.Value;
                    if (entity.Mod != null) entityToChange.Mod = entity.Mod.Value;
                    if (entity.Range != null) entityToChange.Range = entity.Range.Value;
                    if (entity.Tags != null)
                    {
                        _context.Tag.RemoveRange(entityToChange.Tags);
                        entityToChange.Tags = AutoMapper.Mapper.Map<List<Tag>>(entity.Tags);
                    }
                }
                await _context.SaveChangesAsync();
                return Response.Success;
            }
            return Response.NoChanges;
        }

        public async Task<Response> Update(BackendSpecEffectDto entity)
        {
            IEnumerable<SpecEffect> entitiesToChange;
            if ((entitiesToChange = _context.SpecEffect.Where(x => x.Name == entity.Name).Include(x => x.OnDeathAction).Include(x => x.Tags)
                .Include(x => x.Action)).Count() > 0)
            {
                foreach (var entityToChange in entitiesToChange)
                {
                    if (entity.Action != null)
                    {
                        _context.Action.RemoveRange(entityToChange.Action);
                        entityToChange.Action = AutoMapper.Mapper.Map<List<SceneAction>>(entity.Action);
                    }
                    if (entity.OnDeathAction != null)
                    {
                        _context.Action.RemoveRange(entityToChange.OnDeathAction);
                        entityToChange.OnDeathAction = AutoMapper.Mapper.Map<List<SceneAction>>(entity.Action);
                    }
                    if (entity.Duration != null) entityToChange.Duration = entity.Duration.Value;
                    if (entity.Mod != null) entityToChange.Mod = entity.Mod.Value;
                    if (entity.Z != null) entityToChange.Z = entity.Z.Value;
                    if (entity.Tags != null)
                    {
                        _context.Tag.RemoveRange(entityToChange.Tags);
                        entityToChange.Tags = AutoMapper.Mapper.Map<List<Tag>>(entity.Tags);
                    }
                }
                await _context.SaveChangesAsync();
                return Response.Success;
            }
            return Response.NoChanges;
        }

        public async Task<Response> Update(BackendRoleModelDto entity)
        {
            IEnumerable<RoleModel> entitiesToChange;
            if ((entitiesToChange = _context.RoleModel.Where(x => x.Name == entity.Name).Include(x => x.RoleModelSkills)).Count() > 0)
            {
                foreach (var entityToChange in entitiesToChange)
                {
                    if (entity.RoleModelSkills != null)
                    {
                        entityToChange.RoleModelSkills.Clear();
                        foreach (var skill in entity.RoleModelSkills)
                        {
                            entityToChange.RoleModelSkills.Add(new RoleModelSkill()
                            {
                                Skill = _context.Skill.FirstOrDefault(x => x.Name == skill)
                            });
                        }
                    }
                    if (entity.AttackingSkill != null)
                    {
                        entityToChange.AttackingSkill = _context.Skill.FirstOrDefault(x => x.Name == entity.AttackingSkill);
                    }
                    if (entity.ActionPointsIncome != null) entityToChange.ActionPointsIncome = entity.ActionPointsIncome.Value;
                    if (entity.Constitution != null) entityToChange.Constitution = entity.Constitution.Value;
                    if (entity.Speed != null) entityToChange.Speed = entity.Speed.Value;
                    if (entity.Strength != null) entityToChange.Strength = entity.Strength.Value;
                    if (entity.Willpower != null) entityToChange.Willpower = entity.Willpower.Value;
                }
                await _context.SaveChangesAsync();
                return Response.Success;
            }
            return Response.NoChanges;
        }

        public async Task<Response> Update(BackendTileDto entity)
        {
            IEnumerable<Tile> entitiesToChange;
            if ((entitiesToChange = _context.Tile.Where(x => x.Name == entity.Name).Include(x => x.OnStepAction).Include(x => x.Tags)
                .Include(x => x.Action)).Count() > 0)
            {
                foreach (var entityToChange in entitiesToChange)
                {
                    if (entity.Action != null)
                    {
                        _context.Action.RemoveRange(entityToChange.Action);
                        entityToChange.Action = AutoMapper.Mapper.Map<List<SceneAction>>(entity.Action);
                    }
                    if (entity.OnStepAction != null)
                    {
                        _context.Action.RemoveRange(entityToChange.OnStepAction);
                        entityToChange.OnStepAction = AutoMapper.Mapper.Map<List<SceneAction>>(entity.Action);
                    }
                    if (entity.DefaultHeight != null) entityToChange.DefaultHeight = entity.DefaultHeight.Value;
                    if (entity.Flat != null) entityToChange.Flat = entity.Flat.Value;
                    if (entity.Mod != null) entityToChange.Mod = entity.Mod.Value;
                    if (entity.Unbearable != null) entityToChange.Unbearable = entity.Unbearable.Value;
                    if (entity.Tags != null)
                    {
                        _context.Tag.RemoveRange(entityToChange.Tags);
                        entityToChange.Tags = AutoMapper.Mapper.Map<List<Tag>>(entity.Tags);
                    }
                }
                await _context.SaveChangesAsync();
                return Response.Success;
            }
            return Response.NoChanges;
        }

        public async Task<Response> Update(BackendBuffDto entity)
        {
            IEnumerable<Buff> entitiesToChange;
            if ((entitiesToChange = _context.Buff.Where(x => x.Name == entity.Name).Include(x => x.BuffApplier).Include(x => x.Tags)
                .Include(x => x.Action).Include(x => x.OnPurgeAction)).Count() > 0)
            {
                foreach (var entityToChange in entitiesToChange)
                {
                    if (entity.Action != null)
                    {
                        _context.Action.RemoveRange(entityToChange.Action);
                        entityToChange.Action = AutoMapper.Mapper.Map<List<SceneAction>>(entity.Action);
                    }
                    if (entity.OnPurgeAction != null)
                    {
                        _context.Action.RemoveRange(entityToChange.OnPurgeAction);
                        entityToChange.OnPurgeAction = AutoMapper.Mapper.Map<List<SceneAction>>(entity.Action);
                    }
                    if (entity.BuffApplier != null)
                    {
                        _context.Action.RemoveRange(entityToChange.BuffApplier);
                        entityToChange.OnPurgeAction = AutoMapper.Mapper.Map<List<SceneAction>>(entity.BuffApplier);
                    }
                    if (entity.Duration != null) entityToChange.Duration = entity.Duration.Value;
                    if (entity.Eternal != null) entityToChange.Eternal = entity.Eternal.Value;
                    if (entity.Mod != null) entityToChange.Mod = entity.Mod.Value;
                    if (entity.Repeatable != null) entityToChange.Repeatable = entity.Repeatable.Value;
                    if (entity.SummarizeLength != null) entityToChange.SummarizeLength = entity.SummarizeLength.Value;
                    if (entity.Tags != null)
                    {
                        _context.Tag.RemoveRange(entityToChange.Tags);
                        entityToChange.Tags = AutoMapper.Mapper.Map<List<Tag>>(entity.Tags);
                    }
                }
                await _context.SaveChangesAsync();
                return Response.Success;
            }
            return Response.NoChanges;
        }

        public async Task<Response> Update(BackendDecorationDto entity)
        {
            IEnumerable<Decoration> entitiesToChange;
            if ((entitiesToChange = _context.Decoration.Where(x => x.Name == entity.Name).Include(x => x.Armor).Include(x => x.Tags)
                .Include(x => x.Action).Include(x => x.OnDeathAction)).Count() > 0)
            {
                foreach (var entityToChange in entitiesToChange)
                {
                    if (entity.Action != null)
                    {
                        _context.Action.RemoveRange(entityToChange.Action);
                        entityToChange.Action = AutoMapper.Mapper.Map<List<SceneAction>>(entity.Action);
                    }
                    if (entity.OnDeathAction != null)
                    {
                        _context.Action.RemoveRange(entityToChange.OnDeathAction);
                        entityToChange.OnDeathAction = AutoMapper.Mapper.Map<List<SceneAction>>(entity.Action);
                    }
                    if (entity.Z != null) entityToChange.Z = entity.Z.Value;
                    if (entity.Health != null) entityToChange.Health = entity.Health.Value;
                    if (entity.Mod != null) entityToChange.Mod = entity.Mod.Value;
                    if (entity.Armor != null)
                    {
                        _context.TagSynergy.RemoveRange(entityToChange.Armor);
                        entityToChange.Armor = AutoMapper.Mapper.Map<List<TagSynergy>>(entity.Armor);
                    }
                    if (entity.Tags != null)
                    {
                        _context.Tag.RemoveRange(entityToChange.Tags);
                        entityToChange.Tags = AutoMapper.Mapper.Map<List<Tag>>(entity.Tags);
                    }
                }
                await _context.SaveChangesAsync();
                return Response.Success;
            }
            return Response.NoChanges;
        }
    }
}
