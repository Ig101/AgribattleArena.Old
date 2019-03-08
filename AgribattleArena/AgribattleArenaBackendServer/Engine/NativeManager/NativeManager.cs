using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgribattleArenaBackendServer.Models.Natives;

namespace AgribattleArenaBackendServer.Engine.NativeManager
{
    public class NativeManager : INativeManager
    {
        IDictionary<string,TaggingNative> natives;

        public NativeManager(INativesServiceSceneLink repository)
        {
            Initialize(repository);
        }

        public void AddNative(TaggingNative native)
        {
            this.natives.Add(native.Id, native);
        }

        public ActorNative GetActorNative(string id)
        {
            object native = natives[id];
            if (native != null && native is ActorNative)
            {
                return (ActorNative)natives[id];
            }
            else
            {
                return null;
            }
        }

        public BuffNative GetBuffNative(string id)
        {
            object native = natives[id];
            if (native != null && native is BuffNative)
            {
                return (BuffNative)natives[id];
            }
            else
            {
                return null;
            }
        }

        public DecorationNative GetDecorationNative(string id)
        {
            object native = natives[id];
            if (native != null && native is DecorationNative)
            {
                return (DecorationNative)natives[id];
            }
            else
            {
                return null;
            }
        }

        public EffectNative GetEffectNative(string id)
        {
            object native = natives[id];
            if (native != null && native is EffectNative)
            {
                return (EffectNative)natives[id];
            }
            else
            {
                return null;
            }
        }

        public RoleModelNative GetRoleModelNative(string id)
        {
            object native = natives[id];
            if (native != null && native is RoleModelNative)
            {
                return (RoleModelNative)natives[id];
            }
            else
            {
                return null;
            }
        }

        public SkillNative GetSkillNative(string id)
        {
            object native = natives[id];
            if (native != null && native is SkillNative)
            {
                return (SkillNative)natives[id];
            }
            else
            {
                return null;
            }
        }

        public TileNative GetTileNative(string id)
        {
            object native = natives[id];
            if (native != null && native is TileNative)
            {
                return (TileNative)natives[id];
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
