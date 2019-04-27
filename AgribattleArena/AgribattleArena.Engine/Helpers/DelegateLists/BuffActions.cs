using AgribattleArena.Engine.Objects;
using AgribattleArena.Engine.Objects.Immaterial.Buffs;

namespace AgribattleArena.Engine.Helpers.DelegateLists
{
    public class BuffActions
    {
        public delegate void Action(ISceneParentRef scene, IActorParentRef actor, Buff buff, float time);
        public delegate void Applier(IBuffManagerParentRef manager, Buff buff);
        public delegate void OnPurgeAction(ISceneParentRef scene, IActorParentRef actor, Buff buff);

        public static void AddMaxHealth(IBuffManagerParentRef manager, Buff buff)
        {
            manager.AdditionMaxHealth +=buff.Mod;
        }

        public static void AddStrength(IBuffManagerParentRef manager, Buff buff)
        {
            manager.AdditionStrength += buff.Mod;
        }

        public static void DamageSelf(ISceneParentRef scene, IActorParentRef actor, Buff buff, float time)
        {
            actor.Damage(buff.Mod * time, buff.Native.Tags);
        }

        public static void DamageSelfPurge(ISceneParentRef scene, IActorParentRef actor, Buff buff)
        {
            DamageSelf(scene, actor, buff, 1);
        }

        public static void AddTestAttackAndArmor(IBuffManagerParentRef manager, Buff buff)
        {
            manager.Attack.Add(new TagSynergy("test_self_tag", "test_target_tag", buff.Mod));
            manager.Armor.Add(new TagSynergy("test_target_tag", buff.Mod));
        }
    }
}
