using AgribattleArena.Engine.Objects;
using AgribattleArena.Engine.Objects.Immaterial;

namespace AgribattleArena.Engine.Helpers.DelegateLists
{
    public class SkillActions
    {
        public delegate void Action(ISceneParentRef scene, IActorParentRef owner, Tile targetTile, Skill skill);
    }
}
