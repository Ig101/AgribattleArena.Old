using AgribattleArenaBackendServer.Engine.Natives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Services
{
    public interface INativesService
    {
        IEnumerable<ActorNative> GetActors();
        IEnumerable<DecorationNative> GetDecorations();
        IEnumerable<RoleModelNative> GetRoleModels();
        IEnumerable<SkillNative> GetSkills();
        IEnumerable<BuffNative> GetBuffs();
        IEnumerable<TileNative> GetTiles();
        IEnumerable<EffectNative> GetEffects();

        //TODO Dto methods
    }
}
