using AgribattleArena.Engine.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AgribattleArena.Engine.Objects.Immaterial.Buffs
{
    public class BuffManager: IBuffManagerParentRef
    {

        IActorParentRef parent;

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
        int skillCd;
        int skillCost;
        int skillRange;

        public float AdditionStrength { get { return strength; } }
        public float AdditionWillpower { get { return willpower; } }
        public float AdditionConstitution { get { return constitution; } }
        public float AdditionSpeed { get { return speed; } }
        public float AdditionActionPointsIncome { get { return actionPointsIncome; } }
        public float AdditionMaxHealth { get { return maxHealth; } }
        public float AdditionInitiative { get { return initiative; } }

        public int SkillCd { get { return skillCd; } set { skillCd = value; } }
        public int SkillCost { get { return skillCost; } set { skillCost = value; } }
        public int SkillRange { get { return skillRange; } set { skillRange = value; } }
        public bool CanMove { get { return canMove; } set { canMove = value; } }
        public bool CanAct { get { return canAct; } set { canAct = value; } }
        public IActorParentRef Parent { get { return parent; } set { parent = value; } }
        public float Initiative
        {
            get { return initiative; }
            set
            {
                float oldInitiative = parent.Initiative;
                initiative = value;
                float newInitiative = parent.Initiative;
                if (parent.Speed <= 0)
                {
                    parent.DamageModel.Health = 0;
                    parent.IsAlive = false;
                    RemoveAllBuffs();
                }
                else
                {
                    parent.InitiativePosition *= oldInitiative / newInitiative;
                }
            }
        }
        public float AttackPower { get { return attackPower; } set { attackPower = value; } }
        public float SkillPower { get { return skillPower; } set { skillPower = value; } }
        public int MaxHealth
        {
            get { return (int)maxHealth; }
            set
            {
                float oldHealth = parent.MaxHealth;
                maxHealth = value;
                float newHealth = parent.MaxHealth;
                if (parent.Constitution <= 0)
                {
                    parent.DamageModel.Health = 0;
                    parent.IsAlive = false;
                    RemoveAllBuffs();
                }
                else
                {
                    parent.DamageModel.Health *= newHealth / oldHealth;
                }
            }
        }
        public int ActionPointsIncome { get { return (int)actionPointsIncome; } set { actionPointsIncome = value; } }
        public int Strength
        {
            get { return (int)strength; }
            set
            {
                strength = value; if (parent.Strength <= 0)
                {
                    parent.DamageModel.Health = 0;
                    parent.IsAlive = false;
                    RemoveAllBuffs();
                }
            }
        }
        public int Willpower
        {
            get { return (int)willpower; }
            set
            {
                willpower = value; if (parent.Willpower <= 0)
                {
                    parent.DamageModel.Health = 0;
                    parent.IsAlive = false;
                    RemoveAllBuffs();
                }
            }
        }
        public int Constitution
        {
            get { return (int)constitution; }
            set
            {
                float oldHealth = parent.MaxHealth;
                constitution = value;
                float newHealth = parent.MaxHealth;
                if (parent.Constitution <= 0)
                {
                    parent.DamageModel.Health = 0;
                    parent.IsAlive = false;
                    RemoveAllBuffs();
                }
                else
                {
                    parent.DamageModel.Health *= newHealth / oldHealth;
                }
            }
        }
        public int Speed
        {
            get { return (int)speed; }
            set
            {
                float oldInitiative = parent.Initiative;
                speed = value;
                float newInitiative = parent.Initiative;
                if (parent.Speed <= 0)
                {
                    parent.DamageModel.Health = 0;
                    parent.IsAlive = false;
                    RemoveAllBuffs();
                }
                else
                {
                    parent.InitiativePosition *= oldInitiative / newInitiative;
                }
            }
        }
        public List<TagSynergy> Armor { get { return armor; } }
        public List<TagSynergy> Attack { get { return attack; } }
        public List<Buff> Buffs { get { return buffs; } }

        public BuffManager(IActorParentRef parent)
        {
            this.parent = parent;
            armor = new List<TagSynergy>();
            attack = new List<TagSynergy>();
            buffs = new List<Buff>();
            armor.AddRange(parent.DefaultArmor);
            canMove = true;
            canAct = true;
        }

        public void RemoveAllBuffs()
        {
            foreach(Buff buff in buffs)
            {
                buff.Purge();
            }
            this.Buffs.Clear();
            RecalculateBuffs();
        }

        void RecalculateBuffs()
        {
            SkillCd = 0;
            SkillCost = 0;
            SkillRange = 0;
            CanMove = true;
            CanAct = true;
            Strength = 0;
            Willpower = 0;
            Constitution = 0;
            Speed = 0;
            Armor.Clear();
            Armor.AddRange(parent.DefaultArmor);
            Attack.Clear();
            ActionPointsIncome = 0;
            MaxHealth = 0;
            SkillPower = 0;
            AttackPower = 0;
            Initiative = 0;
            foreach (Buff buff in buffs)
            {
                buff.Native.Applier?.Invoke(this, buff);
            }
        }

        public Buff AddBuff(Buff buff)
        {
            parent.Affected = true;
            Buff tempVersion;
            if (!buff.Native.Repeatable && (tempVersion = buffs.Find(x => x.Native.Id == buff.Native.Id)) != null)
            {
                if (buff.Native.SummarizeLength)
                {
                    tempVersion.Duration += buff.Duration;
                }
                else
                {
                    tempVersion.Duration = buff.Duration;
                }
                tempVersion.Mod = Math.Max(tempVersion.Mod, buff.Mod);
                buff = tempVersion;
            }
            else
            {
                buffs.Add(buff);
            }
            RecalculateBuffs();
            return buff;
        }

        public Buff AddBuff(string native, float? mod, float? duration)
        {
            return AddBuff(new Buff(this, parent.Parent.NativeManager.GetBuffNative(native), mod, duration));
        }

        public Buff RemoveBuff(Buff buff)
        {
            parent.Affected = true;
            buff.Purge();
            buffs.Remove(buff);
            RecalculateBuffs();
            return buff;
        }

        public Buff RemoveBuff(int id)
        {
            return RemoveBuff(buffs.Find(x => x.Id == id));
        }

        public void RemoveBuffsByTagsCondition(Func<string[], bool> condition)
        {
            parent.Affected = true;
            for(int i =0;i<buffs.Count;i++)
            {
                if(condition(buffs[i].Native.Tags))
                {
                    buffs[i].Purge();
                    buffs.RemoveAt(i);
                    i--;
                }
            }
            RecalculateBuffs();
        }

        public void Update(float time)
        {
            for (int i = 0; i < buffs.Count; i++)
            {
                buffs[i].Update(time);
                if (buffs[i].Duration != null && buffs[i].Duration <= 0)
                {
                    buffs.RemoveAt(i);
                    i--;
                }
            }
        }
    }
}
