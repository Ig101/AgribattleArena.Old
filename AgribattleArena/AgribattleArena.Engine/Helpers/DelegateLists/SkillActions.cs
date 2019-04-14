using AgribattleArena.Engine.Objects;
using AgribattleArena.Engine.Objects.Immaterial;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgribattleArena.Engine.Helpers.DelegateLists
{
    class SkillActions
    {
        public delegate void Action(ISceneParentRef scene, IActorParentRef owner, Tile targetTile, Skill skill);
    }
}
