using AgribattleArena.Engine.Helpers;
using AgribattleArena.Engine.Natives;
using AgribattleArena.Engine.Objects.Abstract;
using AgribattleArena.Engine.Objects.Immaterial;
using AgribattleArena.Engine.Objects.Immaterial.Buffs;
using AgribattleArena.Engine.VarManagers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgribattleArena.Engine.Objects
{
    public class Actor : TileObject, IActorParentRef, IActorDamageModelRef
    {
        ActorNative native;
        long? externalId;

        IVarManager varManager;

        BuffManager buffManager;

        int strength;
        int willpower;
        int constitution;
        int speed;

        readonly TagSynergy[] armor;
        int actionPointsIncome;

        int actionPoints;
        List<Skill> skills;
        Skill attackingSkill;

        public string[] Tags { get { return native.Tags; } }
        public int SelfStrength { get { return strength; } }
        public int SelfWillpower { get { return willpower; } }
        public int SelfConstitution { get { return constitution; } }
        public int SelfSpeed { get { return speed; } }
        public int SelfActionPointsIncome { get { return actionPointsIncome; } }

        public long? ExternalId { get { return externalId; } }
        public Skill AttackingSkill { get { return attackingSkill; } }
        public BuffManager BuffManager { get { return buffManager; } }
        public int Strength { get { return strength + buffManager.Strength; } }
        public int Willpower { get { return willpower + buffManager.Willpower; } }
        public int Constitution { get { return constitution + buffManager.Constitution; } }
        public int Speed { get { return speed + buffManager.Speed; } }

        public int MaxHealth { get { return Constitution * varManager.ConstitutionMod + buffManager.MaxHealth; } }
        public int ActionPointsIncome { get { return actionPointsIncome + buffManager.ActionPointsIncome; } set { actionPointsIncome = value; } }
        public int ActionPoints { get { return actionPoints; } set { actionPoints = value > varManager.MaxActionPoints ? varManager.MaxActionPoints : value < 0? 0: value; } }
        public float SkillPower { get { return Willpower * varManager.WillpowerMod + buffManager.SkillPower; } }
        public float AttackPower { get { return Strength * varManager.StrengthMod + buffManager.AttackPower; } }
        public float Initiative { get { return Speed * varManager.SpeedMod + buffManager.Initiative; } }

        public List<Skill> Skills { get { return skills; } }
        public List<Buff> Buffs { get { return buffManager.Buffs; } }
        public TagSynergy[] DefaultArmor { get { return armor; } }
        public List<TagSynergy> Armor { get { return buffManager.Armor; } }
        public List<TagSynergy> AttackModifiers { get { return buffManager.Attack; } }

        public Actor(ISceneParentRef parent, IPlayerParentRef owner, long? externalId, ITileParentRef tempTile, float? z, ActorNative native, RoleModelNative roleModelNative)
            : base(parent, owner, tempTile, z ?? native.DefaultZ, new DamageModel(), native)
        {
            this.varManager = parent.VarManager;
            this.externalId = externalId;
            this.native = native;
            this.strength = roleModelNative.DefaultStrength;
            this.willpower = roleModelNative.DefaultWillpower;
            this.constitution = roleModelNative.DefaultConstitution;
            this.speed = roleModelNative.DefaultSpeed;
            this.actionPointsIncome = roleModelNative.DefaultActionPointsIncome;
            this.armor = native.Armor.ToArray();
            this.skills = new List<Skill>();
            foreach (SkillNative skill in roleModelNative.Skills)
            {
                skills.Add(new Skill(this, skill, null, null, null, null));
            }
            this.attackingSkill = new Skill(this, roleModelNative.AttackingSkill, 0, null, 1, null);
            this.buffManager = new BuffManager(this);
            this.InitiativePosition += (1f / this.Initiative);
            this.DamageModel.SetupRoleModel(this);
        }

        public override void Update(float time)
        {
            this.InitiativePosition -= time;
            buffManager.Update(time);
            foreach (Skill skill in skills)
            {
                skill.Update(time);
            }
        }

        public bool Attack(Tile target)
        {
            return AttackingSkill.Cast(target);
        }

        public bool Cast(int id, Tile target)
        {
            Skill skill = Skills.Find(x => x.Id == id);
            return skill.Cast(target);
        }

        public bool Wait()
        {
            return true;
        }

        public bool Move(Tile target)
        {
            if (target.TempObject == null && !target.Native.Unbearable && Math.Abs(target.Height - this.TempTile.Height) < 10 &&
                BuffManager.CanMove && ((target.X == X && Math.Abs(target.Y - Y) == 1) ||
                (target.Y == Y && Math.Abs(target.X - X) == 1)))
            {
                ChangePosition(target, true);
                SpendActionPoints(1);
                return true;
            }
            return false;
        }

        public override void EndTurn()
        {
            if (Parent.TempTileObject == this)
            {
                this.InitiativePosition += (1f / this.Initiative);
            }
        }

        public override bool StartTurn()
        {
            if (Parent.TempTileObject == this)
            {
                this.ActionPoints += ActionPointsIncome;
                return CheckActionAvailability();
            }
            return false;
        }

        public override void OnDeathAction()
        {
            buffManager.RemoveAllBuffs(false);
        }

        public Skill AddSkill(Skill skill)
        {
            Affected = true;
            skills.Add(skill);
            return skill;
        }

        public Skill AddSkill(string native, float? cd, float? mod, int? cost, int? range)
        {
            return AddSkill(new Skill(this, Parent.NativeManager.GetSkillNative(native), cd, mod, cost, range));
        }

        public Skill RemoveSkill(Skill skill)
        {
            Affected = true;
            skills.Remove(skill);
            return skill;
        }

        public Skill RemoveSkill(uint id)
        {
            return RemoveSkill(skills.Find(x => x.Id == id));
        }

        public void SpendActionPoints(int amount)
        {
            this.ActionPoints -= amount;
            this.Affected = true;
        }

        bool CheckStunnedState()
        {
            if (!buffManager.CanAct && !buffManager.CanMove)
            {
                this.actionPoints = 0;
                return true;
            }
            return false;
        }

        public bool CheckActionAvailability()
        {
            return this.IsAlive && this.ActionPoints > 0 && !CheckStunnedState();
        }
    }
}
