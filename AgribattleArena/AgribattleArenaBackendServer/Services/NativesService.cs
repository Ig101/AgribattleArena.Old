using AgribattleArenaBackendServer.Contexts;
using AgribattleArenaBackendServer.Engine.Helpers;
using AgribattleArenaBackendServer.Engine.NativeManager;
using AgribattleArenaBackendServer.Models.Natives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Services
{
    public class NativesService : INativesService, INativesServiceSceneLink
    {
        NativesContext context;

        public NativesService(NativesContext context)
        {
            this.context = context;
        }

        public List<ActionNativeDto> GetActions()
        {
            return AutoMapper.Mapper.Map<List<ActionNativeDto>>(context.Actions.ToList());
        }

        public List<ActorNativeDto> GetActors()
        {
            return AutoMapper.Mapper.Map<List<ActorNativeDto>>(context.Actors.ToList());
        }

        public List<DecorationNativeDto> GetDecorations()
        {
            return AutoMapper.Mapper.Map<List<DecorationNativeDto>>(context.Decorations.ToList());
        }

        public List<RoleModelNativeDto> GetRoleModels()
        {
            return AutoMapper.Mapper.Map<List<RoleModelNativeDto>>(context.RoleModels.ToList());
        }

        public List<SkillNativeDto> GetSkills()
        {
            return AutoMapper.Mapper.Map<List<SkillNativeDto>>(context.Skills.ToList());
        }

        public List<BuffNativeDto>  GetBuffs()
        {
            return AutoMapper.Mapper.Map<List<BuffNativeDto>>(context.Buffs.ToList());
        }

        public List<TileNativeDto> GetTiles()
        {
            return AutoMapper.Mapper.Map<List<TileNativeDto>>(context.Tiles.ToList());
        }

        public List<EffectNativeDto> GetEffects()
        {
            return AutoMapper.Mapper.Map<List<EffectNativeDto>>(context.SpecEffects.ToList());
        }

        void WriteTaggingObjectsToDictionary (List<TaggingNativeDto> input, ref Dictionary<string, TaggingNativeDto> output)
        {
            foreach (TaggingNativeDto obj in input)
            {
                output.Add(obj.Id, obj);
            }
        }

        public IDictionary<string, TaggingNativeDto> GetAllNatives()
        {
            Dictionary<string, TaggingNativeDto> natives = new Dictionary<string, TaggingNativeDto>();
            List<SkillNativeDto> skills = GetSkills();
            List<ActionNativeDto> actions = GetActions();
            WriteTaggingObjectsToDictionary(skills.ToList<TaggingNativeDto>(), ref natives);
            WriteTaggingObjectsToDictionary(GetActors().ToList<TaggingNativeDto>(),ref natives);
            List<DecorationNativeDto> decorations = context.Decorations.Select(decoration => new DecorationNativeDto()
            {
                Action = actions.Find(x => x.Name == decoration.ActionId),
                DefaultArmor = decoration.TagSynergies.Select(x => new TagSynergy(x.SelfTag, x.TargetTag, x.Mod)).ToList(),
                DefaultHealth = decoration.DefaultHealth,
                DefaultMod = decoration.DefaultMod,
                DefaultZ = decoration.DefaultZ,
                Id = decoration.Id,
                Tags = decoration.Tags.Select(x => x.Name).ToList()
            }).ToList();
            WriteTaggingObjectsToDictionary(decorations.ToList<TaggingNativeDto>(), ref natives);
            List<EffectNativeDto> effects = context.SpecEffects.Select(effect => new EffectNativeDto()
            {
                Action = actions.Find(x => x.Name == effect.ActionId),
                DefaultDuration = effect.DefaultDuration,
                DefaultMod = effect.DefaultMod,
                DefaultZ = effect.DefaultZ,
                Id = effect.Id,
                Tags = effect.Tags.Select(x => x.Name).ToList()
            }).ToList();
            WriteTaggingObjectsToDictionary(effects.ToList<TaggingNativeDto>(), ref natives);
            List<BuffNativeDto> buffs = context.Buffs.Select(buff => new BuffNativeDto()
            {
                Action = actions.Find(x => x.Name == buff.ActionId),
                BuffAplier = actions.Find(x=>x.Name == buff.BuffApplierId),
                Duration = buff.Duration,
                Mod = buff.Mod,
                Repeatable = buff.Repeatable,
                SummarizeLength = buff.SummarizeLength,
                Id = buff.Id,
                Tags = buff.Tags.Select(x => x.Name).ToList()
            }).ToList();
            WriteTaggingObjectsToDictionary(buffs.ToList<TaggingNativeDto>(), ref natives);
            List<RoleModelNativeDto> models = context.RoleModels.Select(model => new RoleModelNativeDto()
            {
                AttackingSkill = skills.Find(x => x.Id == model.AttackingSkillId),
                Skills = skills.Where(x => model.RoleModelSkills.Exists(b => b.SkillId == x.Id)).ToList(),
                ActionPointsIncome = model.ActionPointsIncome,
                Armor = model.TagSynergies.Select(x => new TagSynergy(x.SelfTag, x.TargetTag, x.Mod)).ToList(),
                Constitution = model.Constitution,
                Speed = model.Speed,
                Strength = model.Strength,
                Willpower = model.Willpower,
                Id = model.Id
            }).ToList();
            WriteTaggingObjectsToDictionary(models.ToList<TaggingNativeDto>(), ref natives);
            List<TileNativeDto> tiles = context.Tiles.Select(tile => new TileNativeDto()
            {
                Action = actions.Find(x => x.Name == tile.ActionId),
                ActionMod = tile.ActionMod,
                DefaultHeight = tile.DefaultHeight,
                Flat = tile.Flat,
                Unbearable = tile.Unbearable,
                Id = tile.Id,
                Tags = tile.Tags.Select(x => x.Name).ToList()
            }).ToList();
            WriteTaggingObjectsToDictionary(tiles.ToList<TaggingNativeDto>(), ref natives);
            return natives;
        }

        public ActionNativeDto GetAction (string id)
        {
            Contexts.NativesEntities.SceneAction action = context.Actions.Find(id);
            if (action != null)
                return AutoMapper.Mapper.Map<ActionNativeDto>(action);
            else return null;
        }

        public ActorNativeDto GetActor(string id)
        {
            Contexts.NativesEntities.Actor actor = context.Actors.Find(id);
            if (actor != null)
                return AutoMapper.Mapper.Map<ActorNativeDto>(actor);
            else return null;
        }

        public DecorationNativeDto GetDecoration(string id)
        {
            Contexts.NativesEntities.Decoration decoration = context.Decorations.Find(id);
            if (decoration != null)
                return AutoMapper.Mapper.Map<DecorationNativeDto>(decoration);
            else return null;
        }

        public RoleModelNativeDto GetRoleModel(string id)
        {
            Contexts.NativesEntities.RoleModel model = context.RoleModels.Find(id);
            if (model != null)
                return AutoMapper.Mapper.Map<RoleModelNativeDto>(model);
            else return null;
        }

        public SkillNativeDto GetSkill(string id)
        {
            Contexts.NativesEntities.Skill skill = context.Skills.Find(id);
            if (skill != null)
                return AutoMapper.Mapper.Map<SkillNativeDto>(skill);
            else return null;
        }

        public BuffNativeDto GetBuff(string id)
        {
            Contexts.NativesEntities.Buff buff = context.Buffs.Find(id);
            if (buff != null)
                return AutoMapper.Mapper.Map<BuffNativeDto>(buff);
            else return null;
        }

        public TileNativeDto GetTile(string id)
        {
            Contexts.NativesEntities.Tile tile = context.Tiles.Find(id);
            if (tile != null)
                return AutoMapper.Mapper.Map<TileNativeDto>(tile);
            else return null;
        }

        public EffectNativeDto GetEffect(string id)
        {
            Contexts.NativesEntities.SpecEffect effect = context.SpecEffects.Find(id);
            if (effect != null)
                return AutoMapper.Mapper.Map<EffectNativeDto>(effect);
            else return null;
        }

        public bool AddActor(ActorNativeDto actor)
        {
            if (context.Actors.Find(actor.Id) == null)
            {
                context.Actors.Add(new Contexts.NativesEntities.Actor()
                {
                    Id = actor.Id,
                    DefaultZ = actor.DefaultZ,
                    Tags = actor.Tags.Select(x => new Contexts.NativesEntities.Tag()
                    {
                        Name = x
                    }).ToList()
                });
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool AddDecoration(DecorationNativeDto decoration)
        {
            if (context.Decorations.Find(decoration.Id) == null)
            {
                context.Decorations.Add(new Contexts.NativesEntities.Decoration()
                {
                    Id = decoration.Id,
                    DefaultZ = decoration.DefaultZ,
                    Tags = decoration.Tags.Select(x => new Contexts.NativesEntities.Tag()
                    {
                        Name = x
                    }).ToList(),
                    DefaultHealth = decoration.DefaultHealth,
                    DefaultMod = decoration.DefaultMod,
                    TagSynergies = decoration.DefaultArmor.Select(x => new Contexts.NativesEntities.TagSynergy()
                    {
                        Mod = x.Mod,
                        SelfTag = x.SelfTag,
                        TargetTag = x.TargetTag
                    }).ToList(),
                    Action = context.Actions.Find(decoration.Action.Name) ?? new Contexts.NativesEntities.SceneAction()
                    {
                        Id = decoration.Action.Name,
                        Script = Encoding.Unicode.GetBytes(decoration.Action.Script)
                    }
                });
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool AddRoleModel(RoleModelNativeToAddDto roleModel)
        {
            if (context.RoleModels.Find(roleModel.Id) == null)
            {
                bool allSkillsExists = true;
                if (context.Skills.Find(roleModel.AttackSkill) == null) allSkillsExists = false;
                foreach(string skill in roleModel.Skills)
                {
                    if (!allSkillsExists) continue;
                    if (context.Skills.Find(skill) == null) allSkillsExists = false;
                }
                if (allSkillsExists)
                {
                    Contexts.NativesEntities.RoleModel newModel = new Contexts.NativesEntities.RoleModel()
                    {
                        Id = roleModel.Id,
                        ActionPointsIncome = roleModel.ActionPointsIncome,
                        AttackingSkill = context.Skills.Find(roleModel.AttackSkill),
                        Constitution = roleModel.Constitution,
                        Speed = roleModel.Speed,
                        Strength = roleModel.Strength,
                        Willpower = roleModel.Willpower,
                        TagSynergies = roleModel.Armor.Select(x => new Contexts.NativesEntities.TagSynergy()
                        {
                            Mod = x.Mod,
                            SelfTag = x.SelfTag,
                            TargetTag = x.TargetTag
                        }).ToList()
                    };
                    newModel.RoleModelSkills = context.Skills.Where(x => roleModel.Skills.Exists(b => b == x.Id)).Select(x => new Contexts.NativesEntities.RoleModelSkill()
                    {
                        RoleModel = newModel,
                        Skill = x
                    }).ToList();
                    context.RoleModels.Add(newModel);
                    context.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        public bool AddSkill(SkillNativeDto skill)
        {
            if (context.Skills.Find(skill.Id) == null)
            {
                context.Skills.Add(new Contexts.NativesEntities.Skill()
                {
                    Id = skill.Id,
                    Tags = skill.Tags.Select(x => new Contexts.NativesEntities.Tag()
                    {
                        Name = x
                    }).ToList(),
                    DefaultMod = skill.DefaultMod,
                    Action = context.Actions.Find(skill.Action.Name) ?? new Contexts.NativesEntities.SceneAction()
                    {
                        Id = skill.Action.Name,
                        Script = Encoding.Unicode.GetBytes(skill.Action.Script)
                    }
                });
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool AddBuff(BuffNativeDto buff)
        {
            if (context.Buffs.Find(buff.Id) == null)
            {
                context.Buffs.Add(new Contexts.NativesEntities.Buff()
                {
                    Id = buff.Id,
                    Tags = buff.Tags.Select(x => new Contexts.NativesEntities.Tag()
                    {
                        Name = x
                    }).ToList(),
                    Action = context.Actions.Find(buff.Action.Name) ?? new Contexts.NativesEntities.SceneAction()
                    {
                        Id = buff.Action.Name,
                        Script = Encoding.Unicode.GetBytes(buff.Action.Script)
                    }
                });
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool AddTile(TileNativeDto tile)
        {
            if (context.Tiles.Find(tile.Id) == null)
            {
                context.Tiles.Add(new Contexts.NativesEntities.Tile()
                {
                    Id = tile.Id,
                    Tags = tile.Tags.Select(x => new Contexts.NativesEntities.Tag()
                    {
                        Name = x
                    }).ToList(),
                    Action = context.Actions.Find(tile.Action.Name) ?? new Contexts.NativesEntities.SceneAction()
                    {
                        Id = tile.Action.Name,
                        Script = Encoding.Unicode.GetBytes(tile.Action.Script)
                    }
                });
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool AddEffect(EffectNativeDto effect)
        {
            if (context.SpecEffects.Find(effect.Id) == null)
            {
                context.SpecEffects.Add(new Contexts.NativesEntities.SpecEffect()
                {
                    Id = effect.Id,
                    DefaultZ = effect.DefaultZ,
                    Tags = effect.Tags.Select(x => new Contexts.NativesEntities.Tag()
                    {
                        Name = x
                    }).ToList(),
                    DefaultMod = effect.DefaultMod,
                    Action = context.Actions.Find(effect.Action.Name) ?? new Contexts.NativesEntities.SceneAction()
                    {
                        Id = effect.Action.Name,
                        Script = Encoding.Unicode.GetBytes(effect.Action.Script)
                    }
                });
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool AddAction(ActionNativeDto action)
        {
            if (context.Actions.Find(action.Name) == null)
            {
                context.Actions.Add(new Contexts.NativesEntities.SceneAction()
                {
                    Id = action.Name,
                    Script = Encoding.Unicode.GetBytes(action.Script)
                });
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool RemoveActor(string id)
        {
            Contexts.NativesEntities.Actor actor;
            if ((actor = context.Actors.Find(id)) != null)
            {
                context.Actors.Remove(actor);
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool RemoveDecoration(string id)
        {
            Contexts.NativesEntities.Decoration decoration;
            if ((decoration = context.Decorations.Find(id)) != null)
            {
                context.Decorations.Remove(decoration);
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool RemoveRoleModel(string id)
        {
            Contexts.NativesEntities.RoleModel model;
            if ((model = context.RoleModels.Find(id)) != null)
            {
                context.RoleModels.Remove(model);
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool RemoveSkill(string id)
        {
            Contexts.NativesEntities.Skill skill;
            if ((skill = context.Skills.Find(id)) != null)
            {
                context.Skills.Remove(skill);
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool RemoveBuff(string id)
        {
            Contexts.NativesEntities.Buff buff;
            if ((buff = context.Buffs.Find(id)) != null)
            {
                context.Buffs.Remove(buff);
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool RemoveTile(string id)
        {
            Contexts.NativesEntities.Tile tile;
            if ((tile = context.Tiles.Find(id)) != null)
            {
                context.Tiles.Remove(tile);
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool RemoveEffect(string id)
        {
            Contexts.NativesEntities.SpecEffect effect;
            if ((effect = context.SpecEffects.Find(id)) != null)
            {
                context.SpecEffects.Remove(effect);
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool RemoveAction(string id)
        {
            Contexts.NativesEntities.SceneAction action;
            if((action = context.Actions.Find(id))!=null)
            {
                context.Actions.Remove(action);
                context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
