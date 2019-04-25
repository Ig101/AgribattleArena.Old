using AgribattleArena.Engine.Objects;
using AgribattleArena.Engine.Objects.Immaterial;

namespace AgribattleArena.Engine.Helpers.DelegateLists
{
    public class SkillActions
    {
        public delegate void Action(ISceneParentRef scene, IActorParentRef owner, Tile targetTile, Skill skill);

        public static void DoDamageAttack (ISceneParentRef scene, IActorParentRef owner, Tile targetTile, Skill skill)
        {
            if(targetTile.TempObject!=null)
            {
                float mod = skill.CalculateModAttackPower(targetTile.TempObject.Native.Tags);
                targetTile.TempObject.Damage(mod, skill.AggregatedTags);
            }
        }

        public static void DoDamageSkill(ISceneParentRef scene, IActorParentRef owner, Tile targetTile, Skill skill)
        {
            if (targetTile.TempObject != null)
            {
                float mod = skill.CalculateModSkillPower(targetTile.TempObject.Native.Tags);
                targetTile.TempObject.Damage(mod, skill.AggregatedTags);
            }
        }
    }
}
