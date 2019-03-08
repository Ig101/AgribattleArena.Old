using AgribattleArenaBackendServer.Models.Natives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Services
{
    public interface INativesService
    {
        List<ActionNative> GetActions();
        List<ActorNative> GetActors();
        List<DecorationNative> GetDecorations();
        List<RoleModelNative> GetRoleModels();
        List<SkillNative> GetSkills();
        List<BuffNative> GetBuffs();
        List<TileNative> GetTiles();
        List<EffectNative> GetEffects();

        ActionNative GetAction(string id);
        ActorNative GetActor(string id);
        DecorationNative GetDecoration(string id);
        RoleModelNative GetRoleModel(string id);
        SkillNative GetSkill(string id);
        BuffNative GetBuff(string id);
        TileNative GetTile(string id);
        EffectNative GetEffect(string id);

        bool AddAction(ActionNative id);
        bool AddActor(ActorNative actor);
        bool AddDecoration(DecorationNative decoration);
        bool AddRoleModel(RoleModelNativeToAdd roleModel);
        bool AddSkill(SkillNative skill);
        bool AddBuff(BuffNative buff);
        bool AddTile(TileNative tile);
        bool AddEffect(EffectNative effect);

        bool RemoveAction(string id);
        bool RemoveActor(string id);
        bool RemoveDecoration(string id);
        bool RemoveRoleModel(string id);
        bool RemoveSkill(string id);
        bool RemoveBuff(string id);
        bool RemoveTile(string id);
        bool RemoveEffect(string id);
    }
}
