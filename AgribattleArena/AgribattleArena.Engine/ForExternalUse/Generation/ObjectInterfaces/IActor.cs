using System;
using System.Collections.Generic;
using System.Text;

namespace AgribattleArena.Engine.ForExternalUse.Generation.ObjectInterfaces
{
    public interface IActor
    {
        int? ExternalId { get; }
        string NativeId { get; }
        string AttackingSkillName { get; }
        int Strength { get; }
        int Willpower { get; }
        int Constitution { get; }
        int Speed { get; }
        List<string> SkillNames { get; }
        int ActionPointsIncome { get; }
        List<string> StartBuffs { get; }
    }
}
