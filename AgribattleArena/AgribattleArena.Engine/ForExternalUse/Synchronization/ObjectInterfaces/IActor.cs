﻿using AgribattleArena.Engine.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgribattleArena.Engine.ForExternalUse.Synchronization.ObjectInterfaces
{
    public interface IActor
    {
        int Id { get; }
        int? ExternalId { get; }
        string NativeId { get; }
        ISkill AttackingSkill { get; }
        int Strength { get; }
        int Willpower { get; }
        int Constitution { get; }
        int Speed { get; }
        List<ISkill> Skills { get; }
        int ActionPointsIncome { get; }
        List<IBuff> Buffs { get; }
        float InitiativePosition { get; }
        float Health { get; }
        int? OwnerId { get; }
        bool IsAlive { get; }
        float X { get; }
        float Y { get; }
        float Z { get; }
        int MaxHealth { get; }
        int ActionPoints { get; }
        float SkillPower { get; }
        float AttackPower { get; }
        float Initiative { get; }
        List<TagSynergy> Armor { get; }
        List<TagSynergy> AttackModifiers { get; }
    }
}
