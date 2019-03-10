using AgribattleArenaBackendServer.Models.Natives;
using AgribattleArenaBackendServer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Engine.NativeManager
{
    public interface INativeManager
    {
        void Initialize(INativesServiceSceneLink nativesService);
        TileNativeDto GetTileNative(string id);
        ActorNativeDto GetActorNative(string id);
        DecorationNativeDto GetDecorationNative(string id);
        BuffNativeDto GetBuffNative(string id);
        SkillNativeDto GetSkillNative(string id);
        RoleModelNativeDto GetRoleModelNative(string id);
        EffectNativeDto GetEffectNative(string id);
    }
}
