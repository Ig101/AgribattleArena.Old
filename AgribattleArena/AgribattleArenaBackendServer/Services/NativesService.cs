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

        public List<ActionNative> GetActions()
        {
            return AutoMapper.Mapper.Map<List<ActionNative>>(context.Actions.ToList());
        }

        public List<ActorNative> GetActors()
        {
            return AutoMapper.Mapper.Map<List<ActorNative>>(context.Actors.ToList());
        }

        public List<DecorationNative> GetDecorations()
        {
            return AutoMapper.Mapper.Map<List<DecorationNative>>(context.Decorations.ToList());
        }

        public List<RoleModelNative> GetRoleModels()
        {
            return AutoMapper.Mapper.Map<List<RoleModelNative>>(context.RoleModels.ToList());
        }

        public List<SkillNative> GetSkills()
        {
            return AutoMapper.Mapper.Map<List<SkillNative>>(context.Skills.ToList());
        }

        public List<BuffNative>  GetBuffs()
        {
            return AutoMapper.Mapper.Map<List<BuffNative>>(context.Buffs.ToList());
        }

        public List<TileNative> GetTiles()
        {
            return AutoMapper.Mapper.Map<List<TileNative>>(context.Tiles.ToList());
        }

        public List<EffectNative> GetEffects()
        {
            return AutoMapper.Mapper.Map<List<EffectNative>>(context.SpecEffects.ToList());
        }

        void WriteTaggingObjectsToDictionary (List<TaggingNative> input, ref Dictionary<string, TaggingNative> output)
        {
            foreach (TaggingNative obj in input)
            {
                output.Add(obj.Id, obj);
            }
        }

        public IDictionary<string, TaggingNative> GetAllNatives()
        {
            Dictionary<string, TaggingNative> natives = new Dictionary<string, TaggingNative>();
            List<SkillNative> skills = GetSkills();
            List<ActionNative> actions = GetActions();
            WriteTaggingObjectsToDictionary(skills.ToList<TaggingNative>(), ref natives);
            WriteTaggingObjectsToDictionary(GetActors().ToList<TaggingNative>(),ref natives);
            List<DecorationNative> decorations = context.Decorations.Select(decoration => new DecorationNative()
            {
                Action = actions.Find(x => x.Name == decoration.ActionId),
                DefaultArmor = decoration.TagSynergies.Select(x => new TagSynergy(x.SelfTag, x.TargetTag, x.Mod)).ToList(),
                DefaultHealth = decoration.DefaultHealth,
                DefaultMod = decoration.DefaultMod,
                DefaultZ = decoration.DefaultZ,
                Id = decoration.Id,
                Tags = decoration.Tags.Select(x => x.Name).ToList()
            }).ToList();
            WriteTaggingObjectsToDictionary(decorations.ToList<TaggingNative>(), ref natives);
            List<EffectNative> effects = context.SpecEffects.Select(effect => new EffectNative()
            {
                Action = actions.Find(x => x.Name == effect.ActionId),
                DefaultDuration = effect.DefaultDuration,
                DefaultMod = effect.DefaultMod,
                DefaultZ = effect.DefaultZ,
                Id = effect.Id,
                Tags = effect.Tags.Select(x => x.Name).ToList()
            }).ToList();
            WriteTaggingObjectsToDictionary(effects.ToList<TaggingNative>(), ref natives);
            List<BuffNative> buffs = context.Buffs.Select(buff => new BuffNative()
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
            WriteTaggingObjectsToDictionary(buffs.ToList<TaggingNative>(), ref natives);
            List<RoleModelNative> models = context.RoleModels.Select(model => new RoleModelNative()
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
            WriteTaggingObjectsToDictionary(models.ToList<TaggingNative>(), ref natives);
            List<TileNative> tiles = context.Tiles.Select(tile => new TileNative()
            {
                Action = actions.Find(x => x.Name == tile.ActionId),
                ActionMod = tile.ActionMod,
                DefaultHeight = tile.DefaultHeight,
                Flat = tile.Flat,
                Unbearable = tile.Unbearable,
                Id = tile.Id,
                Tags = tile.Tags.Select(x => x.Name).ToList()
            }).ToList();
            WriteTaggingObjectsToDictionary(tiles.ToList<TaggingNative>(), ref natives);
            return natives;
        }

        public ActionNative GetAction (string id)
        {
            return AutoMapper.Mapper.Map<ActionNative>(context.Actions.Find(id));
        }

        public ActorNative GetActor(string id)
        {
            return AutoMapper.Mapper.Map<ActorNative>(context.Actors.Find(id));
        }

        public DecorationNative GetDecoration(string id)
        {
            return AutoMapper.Mapper.Map<DecorationNative>(context.Decorations.Find(id));
        }

        public RoleModelNative GetRoleModel(string id)
        {
            return AutoMapper.Mapper.Map<RoleModelNative>(context.RoleModels.Find(id));
        }

        public SkillNative GetSkill(string id)
        {
            return AutoMapper.Mapper.Map<SkillNative>(context.Skills.Find(id));
        }

        public BuffNative GetBuff(string id)
        {
            return AutoMapper.Mapper.Map<BuffNative>(context.Buffs.Find(id));
        }

        public TileNative GetTile(string id)
        {
            return AutoMapper.Mapper.Map<TileNative>(context.Tiles.Find(id));
        }

        public EffectNative GetEffect(string id)
        {
            return AutoMapper.Mapper.Map<EffectNative>(context.SpecEffects.Find(id));
        }

        public bool AddActor(ActorNative actor)
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

        public bool AddDecoration(DecorationNative decoration)
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

        public bool AddRoleModel(RoleModelNativeToAdd roleModel)
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

        public bool AddSkill(SkillNative skill)
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

        public bool AddBuff(BuffNative buff)
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

        public bool AddTile(TileNative tile)
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

        public bool AddEffect(EffectNative effect)
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

        public bool AddAction(ActionNative action)
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
