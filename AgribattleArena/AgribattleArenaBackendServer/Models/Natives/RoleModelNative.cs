using AgribattleArenaBackendServer.Engine.ActorModel;
using AgribattleArenaBackendServer.Engine.Helpers;
using AgribattleArenaBackendServer.Engine.NativeManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Models.Natives
{
    public class RoleModelNative: TaggingNative
    {
        public SkillNative AttackingSkill { get; set; }
        public int Strength { get; set; }
        public int Willpower { get; set; }
        public int Constitution { get; set; }
        public int Speed { get; set; }
        public List<TagSynergy> Armor { get; set; }
        public List<SkillNative> Skills { get; set; }
        public int ActionPointsIncome { get; set; }
    }
}
