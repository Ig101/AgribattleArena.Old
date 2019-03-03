﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Engine.ActorModel
{
    public class BuffManager
    {
        RoleModel roleModel;

        List<Buff> buffs;

        float strength;
        float willpower;
        float constitution;
        float speed;
        List<TagSynergy> armor;
        List<TagSynergy> attack;

        float actionPointsIncome;
        float maxHealth;
        float skillPower;
        float attackPower;
        float initiative;
        bool canMove;
        bool canAct;

        public bool CanMove { get { return canMove; } }
        public bool CanAct { get { return canAct; } }
        public RoleModel RoleModel { get { return roleModel; } }
        public float Initiative { get { return initiative; } }
        public float AttackPower { get { return attackPower; } }
        public float SkillPower { get { return skillPower; } }
        public int MaxHealth { get { return (int)maxHealth; } }
        public int ActionPointsIncome { get { return (int)actionPointsIncome; } }
        public int Strength { get { return (int)strength; } }
        public int Willpower { get { return (int)willpower; } }
        public int Constitution { get { return (int)constitution; } }
        public int Speed { get { return (int)speed; } }
        public List<TagSynergy> Armor { get { return armor; } }
        public List<TagSynergy> Attack { get { return attack; } }
        public List<Buff> Buffs { get { return buffs; } }

        public BuffManager(RoleModel roleModel)
        {
            this.roleModel = roleModel;
            armor = new List<TagSynergy>();
            attack = new List<TagSynergy>();
            buffs = new List<Buff>();
            RecalculateBuffs();
        }

        void RecalculateBuffs()
        {
            canMove = true;
            canAct = true;
            strength = 0;
            willpower = 0;
            constitution = 0;
            speed = 0;
            armor.Clear();
            armor.AddRange(roleModel.DefaultArmor);
            attack.Clear();
            actionPointsIncome = 0;
            maxHealth = 0;
            skillPower = 0;
            attackPower = 0;
            initiative = 0;
            foreach(Buff buff in buffs)
            {
                buff.Native.BuffAplier?.Invoke(this, buff.Mod);
            }
        }

        public void AddBuff(Buff buff)
        {
            buffs.Add(buff);
            RecalculateBuffs();
        }

        public void RemoveBuff(Buff buff)
        {
            buffs.Remove(buff);
            RecalculateBuffs();
        }

        public void RemoveBuff(int id)
        {
            buffs.RemoveAt(id);
            RecalculateBuffs();
        }

        public void Update(float time)
        {
            for(int i = 0; i<buffs.Count;i++)
            {
                buffs[i].Update(time);
                if(buffs[i].Duration != null && buffs[i].Duration<=0)
                {
                    RemoveBuff(i);
                    i--;
                }
            }
        }
    }
}
