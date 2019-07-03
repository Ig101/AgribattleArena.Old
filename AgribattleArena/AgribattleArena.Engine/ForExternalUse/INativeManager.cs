﻿using AgribattleArena.Engine.Helpers;
using AgribattleArena.Engine.Natives;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgribattleArena.Engine.ForExternalUse
{
    public interface INativeManager
    {
        void AddActorNative(string id, string[] tags, float defaultZ, TagSynergy[] armor);
        void AddBuffNative(string id, string[] tags, bool eternal, int repeatable, bool summarizeLength, int? defaultDuration, float defaultMod,
            IEnumerable<string> actions, IEnumerable<string> appliers, IEnumerable<string> onPurgeActions);
        void AddDecorationNative(string id, string[] tags, TagSynergy[] defaultArmor, int defaultHealth, float defaultZ, float defaultMod, 
            IEnumerable<string> actions, IEnumerable<string> onDeathActions);
        void AddEffectNative(string id, string[] tags, float defaultZ, float? defaultDuration, float defaultMod, IEnumerable<string> actions,
            IEnumerable<string> onDeathActions);
        void AddRoleModelNative(string id, int defaultStrength, int defaultWillpower, int defaultConstitution, int defaultSpeed,
            int defaultActionPointsIncome, string attackingSkill, string[] skills);
        void AddSkillNative(string id, string[] tags, int defaultRange, int defaultCost, float defaultCd, float defaultMod, IEnumerable<string> actions);
        void AddTileNative(string id, string[] tags, bool flat, int defaultHeight, bool unbearable, float defaultMod, IEnumerable<string> actions,
            IEnumerable<string> onStepActions);
    }
}
