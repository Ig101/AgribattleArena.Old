using AgribattleArena.Engine.Objects;

namespace AgribattleArena.Engine.Helpers.DelegateLists
{
    public class TileActions
    {
        public delegate void Action(ISceneParentRef parent, Tile tile, float time);
        public delegate void OnStepAction(ISceneParentRef parent, Tile tile);

        public static void DoDamageOnStep (ISceneParentRef parent, Tile tile)
        {
            DoDamage(parent, tile, 1);
        }

        public static void DoDamage (ISceneParentRef parent, Tile tile, float time)
        {
            if(tile.TempObject!=null)
            {
                tile.TempObject.Damage(tile.Native.DefaultMod * time, tile.Native.Tags);
            }
        }
    }
}
