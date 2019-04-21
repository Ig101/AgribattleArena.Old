using AgribattleArena.Engine.Objects;

namespace AgribattleArena.Engine.Helpers.DelegateLists
{
    public class TileActions
    {
        public delegate void Action(ISceneParentRef parent, Tile tile, float time);
        public delegate void OnStepAction(ISceneParentRef parent, Tile tile);
    }
}
