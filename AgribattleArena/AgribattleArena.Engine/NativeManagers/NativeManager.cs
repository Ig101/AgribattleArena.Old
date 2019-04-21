using System;
using System.Collections.Generic;
using System.Text;
using AgribattleArena.Engine.Helpers;
using AgribattleArena.Engine.Natives;

namespace AgribattleArena.Engine.NativeManagers
{
    public class NativeManager : INativeManager, ForExternalUse.INativeManager
    {
        Dictionary<string,ActorNative> actorNatives;
        Dictionary<string,ActiveDecorationNative> decorationNatives;
        Dictionary<string,BuffNative> buffNatives;
        Dictionary<string,SpecEffectNative> effectNatives;
        Dictionary<string,SkillNative> skillNatives;
        Dictionary<string,RoleModelNative> roleModelNatives;
        Dictionary<string,TileNative> tileNatives;

        public NativeManager()
        {
            actorNatives = new Dictionary<string, ActorNative>();
            decorationNatives = new Dictionary<string, ActiveDecorationNative>();
            buffNatives = new Dictionary<string, BuffNative>();
            effectNatives = new Dictionary<string, SpecEffectNative>();
            skillNatives = new Dictionary<string, SkillNative>();
            roleModelNatives = new Dictionary<string, RoleModelNative>();
            tileNatives = new Dictionary<string, TileNative>();
        }

        string[] InternTags(string[] tags)
        {
            string[] newTags = new string[tags.Length];
            for(int i=0; i<tags.Length;i++)
            {
                newTags[i] = string.Intern(tags[i]);
            }
            return newTags;
        }

        public void AddActorNative(string id, string[] tags, float defaultZ, TagSynergy[] armor)
        {
            actorNatives.Add(id, new ActorNative(id, InternTags(tags), defaultZ, armor));
        }

        public void AddBuffNative(string id, string[] tags, bool repeatable, bool summarizeLength, int? defaultDuration, float defaultMod,
            IEnumerable<string> actions, IEnumerable<string> appliers)
        {
            buffNatives.Add(id, new BuffNative(id, InternTags(tags), repeatable, summarizeLength, defaultDuration, defaultMod,
                    actions, appliers));
        }

        public void AddDecorationNative(string id, string[] tags, TagSynergy[] defaultArmor, int defaultHealth, float defaultZ, float defaultMod, IEnumerable<string> actions)
        {
            decorationNatives.Add(id, new ActiveDecorationNative(id, InternTags(tags), defaultArmor, defaultHealth, defaultZ, defaultMod, actions));
        }

        public void AddEffectNative(string id, string[] tags, float defaultZ, float? defaultDuration, float defaultMod, IEnumerable<string> actions)
        {
            effectNatives.Add(id, new SpecEffectNative(id, InternTags(tags), defaultZ, defaultDuration, defaultMod, actions));
        }

        public void AddRoleModelNative(string id, int defaultStrength, int defaultWillpower, int defaultConstitution, int defaultSpeed, 
            int defaultActionPointsIncome, SkillNative attackingSkill, SkillNative[] skills)
        {
            roleModelNatives.Add(id, new RoleModelNative(id, defaultStrength, defaultWillpower, defaultConstitution, defaultSpeed, defaultActionPointsIncome,
                    attackingSkill, skills));
        }

        public void AddSkillNative(string id, string[] tags, int defaultRange, int defaultCost, float defaultCd, float defaultMod, IEnumerable<string> actions)
        {
            skillNatives.Add(id, new SkillNative(id, InternTags(tags), defaultRange, defaultCost, defaultCd, defaultMod, actions));
        }

        public void AddTileNative(string id, string[] tags, bool flat, int defaultHeight, bool unbearable, float defaultMod, IEnumerable<string> actions, 
            IEnumerable<string> onStepActions)
        {
            tileNatives.Add(id, new TileNative(id, InternTags(tags), flat, defaultHeight, unbearable, defaultMod, actions, onStepActions));
        }

        public ActorNative GetActorNative(string id)
        {
            ActorNative native = actorNatives[id];
            return native;
        }

        public BuffNative GetBuffNative(string id)
        {
            BuffNative native = buffNatives[id];
            return native;
        }

        public ActiveDecorationNative GetDecorationNative(string id)
        {
            ActiveDecorationNative native = decorationNatives[id];
            return native;
        }

        public SpecEffectNative GetEffectNative(string id)
        {
            SpecEffectNative native = effectNatives[id];
            return native;
        }

        public RoleModelNative GetRoleModelNative(string id)
        {
            RoleModelNative native = roleModelNatives[id];
            return native;
        }

        public SkillNative GetSkillNative(string id)
        {
            SkillNative native = skillNatives[id];
            return native;
        }

        public TileNative GetTileNative(string id)
        {
            TileNative native = tileNatives[id];
            return native;
        }
    }
}
