using AgribattleArenaBackendServer.Engine.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Engine.ActorModel.Buffs
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
        int skillCd;
        int skillCost;
        int skillRange;

        public int SkillCd { get { return skillCd; } set { skillCd = value; } }
        public int SkillCost { get { return skillCost; } set { skillCost = value; } }
        public int SkillRange { get { return skillRange; } set { skillRange = value; } }
        public bool CanMove { get { return canMove; } set { canMove = value; roleModel.CheckStunnedState(); } }
        public bool CanAct { get { return canAct; } set { canAct = value; roleModel.CheckStunnedState(); } }
        public RoleModel RoleModel { get { return roleModel; } set { roleModel = value; } }
        public float Initiative { get { return initiative; } set {
                float oldInitiative = roleModel.Initiative;
                initiative = value;
                float newInitiative = roleModel.Initiative;
                if (roleModel.Speed <= 0)
                {
                    roleModel.Owner.DamageModel.Health = 0;
                    roleModel.Owner.IsAlive = false;
                    RemoveAllBuffs();
                }
                else
                {
                    roleModel.Owner.InitiativePosition *= oldInitiative / newInitiative;
                }
            } }
        public float AttackPower { get { return attackPower; } set { attackPower = value; } }
        public float SkillPower { get { return skillPower; } set { skillPower = value; } }
        public int MaxHealth { get { return (int)maxHealth; } set {
                float oldHealth = roleModel.MaxHealth;
                maxHealth = value;
                float newHealth = roleModel.MaxHealth;
                if (roleModel.Constitution <= 0)
                {
                    roleModel.Owner.DamageModel.Health = 0;
                    roleModel.Owner.IsAlive = false;
                    RemoveAllBuffs();
                }
                else
                {
                    roleModel.Owner.DamageModel.Health *= newHealth / oldHealth;
                }
            } }
        public int ActionPointsIncome { get { return (int)actionPointsIncome; } set { actionPointsIncome = value; } }
        public int Strength { get { return (int)strength; } set { strength = value; if (roleModel.Strength <= 0)
                {
                    roleModel.Owner.DamageModel.Health = 0;
                    roleModel.Owner.IsAlive = false;
                    RemoveAllBuffs();
                }
            } }
        public int Willpower { get { return (int)willpower; } set { willpower = value; if (roleModel.Willpower <= 0)
                {
                    roleModel.Owner.DamageModel.Health = 0;
                    roleModel.Owner.IsAlive = false;
                    RemoveAllBuffs();
                }
            } }
        public int Constitution { get { return (int)constitution; }
            set
            {
                float oldHealth = roleModel.MaxHealth;
                constitution = value;
                float newHealth = roleModel.MaxHealth;
                if (roleModel.Constitution <= 0)
                {
                    roleModel.Owner.DamageModel.Health = 0;
                    roleModel.Owner.IsAlive = false;
                    RemoveAllBuffs();
                }
                else
                {
                    roleModel.Owner.DamageModel.Health *= newHealth / oldHealth;
                }
            }
        }
        public int Speed
        {
            get { return (int)speed; }
            set
            {
                float oldInitiative = roleModel.Initiative;
                speed = value;
                float newInitiative = roleModel.Initiative;
                if (roleModel.Speed <= 0)
                {
                    roleModel.Owner.DamageModel.Health = 0;
                    roleModel.Owner.IsAlive = false;
                    RemoveAllBuffs();
                }
                else
                {
                    roleModel.Owner.InitiativePosition *= oldInitiative / newInitiative;
                }
            }
        }
        public List<TagSynergy> Armor { get { return armor; }  }
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

        public void RemoveAllBuffs()
        {
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
            Armor.AddRange(roleModel.DefaultArmor);
            Attack.Clear();
            ActionPointsIncome = 0;
            MaxHealth = 0;
            SkillPower = 0;
            AttackPower = 0;
            Initiative = 0;
            foreach(Buff buff in buffs)
            {
                //buff.Native.BuffAplier?.Invoke(this, buff.Mod);
                if (buff.Native.BuffAplier != null)
                {
                    Jint.Engine actionEngine = new Jint.Engine();
                    actionEngine
                        .SetValue("buffManager", this)
                        .SetValue("mod", buff.Mod)
                        .Execute(buff.Native.BuffAplier.Script);
                }
            }
        }

        public Buff AddBuff(Buff buff)
        {
            roleModel.Owner.Affected = true;
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
            return AddBuff(new Buff(this, roleModel.Owner.Parent.NativeManager.GetBuffNative(native), mod, duration));
        }

        public Buff RemoveBuff(Buff buff)
        {
            roleModel.Owner.Affected = true;
            buffs.Remove(buff);
            RecalculateBuffs();
            return buff;
        }

        public Buff RemoveBuff(uint id)
        {
            return RemoveBuff(buffs.Find(x => x.Id == id));
        }

        public void RemoveBuff(int id)
        {
            roleModel.Owner.Affected = true;
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
