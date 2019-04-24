using AgribattleArena.Engine.ForExternalUse;
using AgribattleArena.Engine.Helpers;
using AgribattleArena.Engine.Natives;
using AgribattleArena.Engine.Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgribattleArena.Engine
{
    public interface ISceneForSceneGenerator
    {
        VarManagers.IVarManager VarManager { get; }
        NativeManagers.INativeManager NativeManager { get; }

        Tile[][] SetupEmptyTileSet(int width, int height);
        Player CreatePlayer(int id, int? team);
        Actor CreateActor(Player owner, string nativeName, string roleNativeName, Tile target, float? z);
        Actor CreateActor(Player owner, int? externalId, string nativeName, RoleModelNative roleModel, Tile target, float? z);
        ActiveDecoration CreateDecoration(Player owner, string nativeName, Tile target, float? z, int? health, TagSynergy[] armor, float? mod);
        SpecEffect CreateEffect(Player owner, string nativeName, Tile target, float? z, float? duration, float? mod);
        Tile CreateTile(string nativeName, int x, int y, int? height);
    }
}
