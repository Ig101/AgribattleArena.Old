﻿using AgribattleArena.Engine.Helpers;
using AgribattleArena.Engine.Natives;
using AgribattleArena.Engine.Objects.Immaterial;
using AgribattleArena.Engine.Objects.Immaterial.Buffs;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgribattleArena.Engine.Objects
{
    public interface IActorParentRef
    {
        string[] Tags { get; }
        ISceneParentRef Parent { get; }
        IPlayerParentRef Owner { get; set; }
        bool IsAlive { get; set; }
        int X { get; set; }
        int Y { get; set; }
        float Z { get; set; }
        bool Affected { get; set; }
        float InitiativePosition { get; set; }
        DamageModel DamageModel { get; }
        ITileParentRef TempTile { get; }

        Skill AttackingSkill { get; }
        BuffManager BuffManager { get; }
        int Strength { get; }
        int Willpower { get; }
        int Constitution { get; }
        int Speed { get; }

        int MaxHealth { get; }
        int ActionPointsIncome { get; }
        int ActionPoints { get; }
        float SkillPower { get; }
        float AttackPower { get; }
        float Initiative { get; }

        List<Skill> Skills { get; }
        List<Buff> Buffs { get; }
        TagSynergy[] DefaultArmor { get; }
        List<TagSynergy> Armor { get; }
        List<TagSynergy> AttackModifiers { get; }

        void SpendActionPoints(int amount);
    }
}
