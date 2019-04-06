using AgribattleArenaBackendServer.Engine.ActorModel.Buffs;
using AgribattleArenaBackendServer.Engine.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Engine.ActorModel
{
    public class DamageModel
    {
        RoleModel roleModel;

        int? maxHealth;
        TagSynergy[] armor;

        float health;

        public List<Buff> Buffs { get { return roleModel?.Buffs; } }
        public int MaxHealth { get { return roleModel?.MaxHealth ?? maxHealth.Value; } }
        public TagSynergy[] Armor { get { return roleModel?.Armor.ToArray() ?? armor; } }
        public float Health { get { return health; } set { health = Math.Min(value,MaxHealth); } }

        public DamageModel (int maxHealth, TagSynergy[] armor)
        {
            this.roleModel = null;
            this.maxHealth = maxHealth;
            this.armor = armor;
            this.health = maxHealth;
        }

        public DamageModel ()
        {

        }

        public void SetupRoleModel (RoleModel model)
        {
            this.roleModel = model;
            this.health = MaxHealth;
        }

        public bool Damage (float amount, string[] tags)
        {
            if (tags != null && armor!=null)
            {
                foreach (string atackerTag in tags)
                {
                    foreach (TagSynergy synergy in Armor)
                    {
                        if (synergy.TargetTag == atackerTag)
                        {
                            amount *= synergy.Mod;
                        }
                    }
                }
            }
            Health -= amount;
            
            return health <= 0;
        }
    }
}
