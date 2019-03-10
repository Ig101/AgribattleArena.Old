using AgribattleArenaBackendServer.Engine.ActorModel;
using AgribattleArenaBackendServer.Engine.NativeManager;
using AgribattleArenaBackendServer.Models.Natives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Engine.Generator.GeneratorEntities
{
    public class GenerationPartyActor
    {
        int? owner;
        string native;
        RoleModelNativeToAddDto roleModel;

        public int? Owner { get { return owner; } }
        public RoleModelNativeToAddDto RoleModel { get { return roleModel; } }
        public string Native { get { return native; } }

        public GenerationPartyActor(string native, int? owner, RoleModelNativeToAddDto roleModel)
        {
            this.owner = owner;
            this.roleModel = roleModel;
            this.native = native;
        }
    }
}
