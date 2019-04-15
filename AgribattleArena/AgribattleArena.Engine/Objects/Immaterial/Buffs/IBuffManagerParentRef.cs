using AgribattleArena.Engine.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgribattleArena.Engine.Objects.Immaterial.Buffs
{
    public interface IBuffManagerParentRef
    {
        int SkillCd { get; set; }
        int SkillCost { get; set; }
        int SkillRange { get; set; }
        bool CanMove { get; set; }
        bool CanAct { get; set; }
        IActorParentRef Parent { get; set; }
        float Initiative { get; set; }
        float AttackPower { get; set; }
        float SkillPower { get; set; }
        int MaxHealth { get; set; }
        int ActionPointsIncome { get; set; }
        int Strength { get; set; }
        int Willpower { get; set; }
        int Constitution { get; set; }
        int Speed { get; set; }
        List<TagSynergy> Armor { get; }
        List<TagSynergy> Attack { get; }
        List<Buff> Buffs { get; }
    }
}
