using AgribattleArenaBackendServer.Contexts;
using AgribattleArenaBackendServer.Engine.Generator;
using AgribattleArenaBackendServer.Engine.Generator.GeneratorEntities;
using AgribattleArenaBackendServer.Models.Natives;
using AgribattleArenaBackendServer.Models.Profiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Services
{
    public class ProfilesService : IProfilesService, IProfilesServiceSceneLink
    {
        ProfilesContext context;

        public ProfilesService(ProfilesContext context)
        {
            this.context = context;
        }

        public PlayerDto GetPlayer(int playerId)
        {
            return AutoMapper.Mapper.Map<PlayerDto>(context.Players.Find(playerId));
        }

        public PlayerActorDto GetPlayerActor(int actorId)
        {
            return AutoMapper.Mapper.Map<PlayerActorDto>(context.Actors.Find(actorId));
        }

        public List<PlayerActor> GetPlayerActors(int playerId)
        {
            PlayerDto player = GetPlayer(playerId);
            return player.Actors.Select(x => new PlayerActor(x.ActorNative, playerId, AutoMapper.Mapper.Map<PlayerActorDto,RoleModelNativeToAddDto>(x))).ToList();
        }

        public ProfileDto GetProfile(string login)
        {
            return AutoMapper.Mapper.Map<ProfileDto>(context.Profiles.Find(login));
        }

        public List<RightDto> GetRights()
        {
            return AutoMapper.Mapper.Map<List<RightDto>>(context.Rights);
        }

        public RoleDto GetRole(int roleId)
        {
            return AutoMapper.Mapper.Map<RoleDto>(context.Players.Find(roleId));
        }

        public List<RoleDto> GetRoles()
        {
            return AutoMapper.Mapper.Map<List<RoleDto>>(context.Roles);
        }
    }
}
