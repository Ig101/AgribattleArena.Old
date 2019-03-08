using AgribattleArenaBackendServer.Contexts;
using AgribattleArenaBackendServer.Engine.Helpers;
using AgribattleArenaBackendServer.Engine.NativeManager;
using AgribattleArenaBackendServer.Engine.Natives;
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

        string MapAction(Contexts.NativesEntities.Action action)
        {
            return Encoding.Unicode.GetString(action.Script);
        }

        TagSynergy MapSynergy(Contexts.NativesEntities.TagSynergy synergy)
        {
            return new TagSynergy(synergy.SelfTag, synergy.TargetTag, synergy.Mod);
        }

        ActorNative MapActor(Contexts.NativesEntities.Actor actor)
        {
            return new ActorNative(actor.Id, actor.Tags.Select(x => x.Name).ToArray(), actor.DefaultZ);
        }

        DecorationNative MapDecoration(Contexts.NativesEntities.Decoration decoration)
        {
            return new DecorationNative(decoration.Id, decoration.Tags.Select(x => x.Name).ToArray(), decoration.DefaultMod, decoration.Action.Script.ToString(),
                    decoration.DefaultZ, decoration.DefaultHealth, decoration.TagSynergies.Select(x => MapSynergy(x)).ToArray());
        }

        SkillNative MapSkill(Contexts.NativesEntities.Skill skill)
        {
            return new SkillNative(skill.Id, skill.Tags.Select(x => x.Name).ToArray(), MapAction(skill.Action), 
                skill.DefaultCost, skill.DefaultCd, skill.DefaultMod, skill.DefaultRange);
        }

        RoleModelNative MapRoleModel(Contexts.NativesEntities.RoleModel roleModel)
        {
            return new RoleModelNative(roleModel.Id, roleModel.Strength, roleModel.Willpower, roleModel.Constitution, roleModel.Speed,
                roleModel.TagSynergies.Select(x => new TagSynergy(x.SelfTag, x.TargetTag, x.Mod)).ToArray(), MapSkill(roleModel.AttackingSkill),
                roleModel.RoleModelSkills.Select(x => MapSkill(x.Skill)).ToArray(), roleModel.ActionPointsIncome);
        }

        BuffNative MapBuff (Contexts.NativesEntities.Buff buff)
        {
            return new BuffNative(buff.Id, buff.Tags.Select(x => x.Name).ToArray(), MapAction(buff.Action), MapAction(buff.BuffApplier),
                buff.Duration, buff.Mod, buff.SummarizeLength, buff.Repeatable);
        }

        TileNative MapTile (Contexts.NativesEntities.Tile tile)
        {
            return new TileNative(tile.Id, MapAction(tile.Action), tile.ActionMod, tile.Unbearable, tile.Flat, tile.DefaultHeight,
                tile.Tags.Select(x => x.Name).ToArray());
        }

        EffectNative MapEffect (Contexts.NativesEntities.SpecEffect effect)
        {
            return new EffectNative(effect.Id, effect.Tags.Select(x => x.Name).ToArray(), MapAction(effect.Action),
                effect.DefaultDuration, effect.DefaultMod, effect.DefaultZ);
        }

        public IEnumerable<ActorNative> GetActors()
        {
            List<ActorNative> natives = new List<ActorNative>();
            natives.AddRange(context.Actors.Select(actor=> MapActor(actor)));
            return natives;
        }

        public IEnumerable<DecorationNative> GetDecorations()
        {
            List<DecorationNative> natives = new List<DecorationNative>();
            natives.AddRange(context.Decorations.Select(decoration =>
                MapDecoration(decoration)));
            return natives;
        }

        public IEnumerable<RoleModelNative> GetRoleModels()
        {
            List<RoleModelNative> natives = new List<RoleModelNative>();
            natives.AddRange(context.RoleModels.Select(roleModel =>
                MapRoleModel(roleModel)));
            return natives;
        }

        public IEnumerable<SkillNative> GetSkills()
        {
            List<SkillNative> natives = new List<SkillNative>();
            natives.AddRange(context.Skills.Select(skill =>
                MapSkill(skill)));
            return natives;
        }

        public IEnumerable<BuffNative>  GetBuffs()
        {
            List<BuffNative> natives = new List<BuffNative>();
            natives.AddRange(context.Buffs.Select(buff =>
                MapBuff(buff)));
            return natives;
        }

        public IEnumerable<TileNative> GetTiles()
        {
            List<TileNative> natives = new List<TileNative>();
            natives.AddRange(context.Tiles.Select(tile =>
                MapTile(tile)));
            return natives;
        }

        public IEnumerable<EffectNative> GetEffects()
        {
            List<EffectNative> natives = new List<EffectNative>();
            natives.AddRange(context.SpecEffects.Select(effect =>
                MapEffect(effect)));
            return natives;
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
            WriteTaggingObjectsToDictionary(GetActors().ToList<TaggingNative>(),ref natives);
            WriteTaggingObjectsToDictionary(GetDecorations().ToList<TaggingNative>(), ref natives);
            WriteTaggingObjectsToDictionary(GetEffects().ToList<TaggingNative>(), ref natives);
            WriteTaggingObjectsToDictionary(GetBuffs().ToList<TaggingNative>(), ref natives);
            WriteTaggingObjectsToDictionary(GetSkills().ToList<TaggingNative>(), ref natives);
            WriteTaggingObjectsToDictionary(GetRoleModels().ToList<TaggingNative>(), ref natives);
            WriteTaggingObjectsToDictionary(GetTiles().ToList<TaggingNative>(), ref natives);
            return natives;
        }
    }
}
