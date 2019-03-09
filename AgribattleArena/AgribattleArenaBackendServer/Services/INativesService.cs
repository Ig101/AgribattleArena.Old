using AgribattleArenaBackendServer.Models.Natives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Services
{
    public interface INativesService
    {
        List<ActionNativeDto> GetActions();
        List<ActorNativeDto> GetActors();
        List<DecorationNativeDto> GetDecorations();
        List<RoleModelNativeDto> GetRoleModels();
        List<SkillNativeDto> GetSkills();
        List<BuffNativeDto> GetBuffs();
        List<TileNativeDto> GetTiles();
        List<EffectNativeDto> GetEffects();

        ActionNativeDto GetAction(string id);
        ActorNativeDto GetActor(string id);
        DecorationNativeDto GetDecoration(string id);
        RoleModelNativeDto GetRoleModel(string id);
        SkillNativeDto GetSkill(string id);
        BuffNativeDto GetBuff(string id);
        TileNativeDto GetTile(string id);
        EffectNativeDto GetEffect(string id);

        bool AddAction(ActionNativeDto id);
        bool AddActor(ActorNativeDto actor);
        bool AddDecoration(DecorationNativeDto decoration);
        bool AddRoleModel(RoleModelNativeToAddDto roleModel);
        bool AddSkill(SkillNativeDto skill);
        bool AddBuff(BuffNativeDto buff);
        bool AddTile(TileNativeDto tile);
        bool AddEffect(EffectNativeDto effect);

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
