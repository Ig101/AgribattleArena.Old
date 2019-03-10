using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgribattleArenaBackendServer.Models.Natives;
using AgribattleArenaBackendServer.Services;

namespace AgribattleArenaBackendServer.Engine.NativeManager
{
    public class NativeManager : INativeManager
    {
        IDictionary<string,TaggingNativeDto> natives;

        public NativeManager(INativesServiceSceneLink repository)
        {
            Initialize(repository);
        }

        public ActorNativeDto GetActorNative(string id)
        {
            object native = natives[id];
            if (native != null && native is ActorNativeDto)
            {
                return (ActorNativeDto)natives[id];
            }
            else
            {
                return null;
            }
        }

        public BuffNativeDto GetBuffNative(string id)
        {
            object native = natives[id];
            if (native != null && native is BuffNativeDto)
            {
                return (BuffNativeDto)natives[id];
            }
            else
            {
                return null;
            }
        }

        public DecorationNativeDto GetDecorationNative(string id)
        {
            object native = natives[id];
            if (native != null && native is DecorationNativeDto)
            {
                return (DecorationNativeDto)natives[id];
            }
            else
            {
                return null;
            }
        }

        public EffectNativeDto GetEffectNative(string id)
        {
            object native = natives[id];
            if (native != null && native is EffectNativeDto)
            {
                return (EffectNativeDto)natives[id];
            }
            else
            {
                return null;
            }
        }

        public RoleModelNativeDto GetRoleModelNative(string id)
        {
            object native = natives[id];
            if (native != null && native is RoleModelNativeDto)
            {
                return (RoleModelNativeDto)natives[id];
            }
            else
            {
                return null;
            }
        }

        public SkillNativeDto GetSkillNative(string id)
        {
            object native = natives[id];
            if (native != null && native is SkillNativeDto)
            {
                return (SkillNativeDto)natives[id];
            }
            else
            {
                return null;
            }
        }

        public TileNativeDto GetTileNative(string id)
        {
            object native = natives[id];
            if (native != null && native is TileNativeDto)
            {
                return (TileNativeDto)natives[id];
            }
            else
            {
                return null;
            }
        }

        public void Initialize(INativesServiceSceneLink nativesService)
        {
            natives = nativesService.GetAllNatives();
        }
    }
}
