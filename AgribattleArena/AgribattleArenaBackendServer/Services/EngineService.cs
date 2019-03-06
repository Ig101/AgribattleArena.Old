﻿using AgribattleArenaBackendServer.Engine;
using AgribattleArenaBackendServer.Engine.Generator;
using AgribattleArenaBackendServer.Engine.NativeManager;
using AgribattleArenaBackendServer.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Services
{
    public class EngineService: IEngineService
    {
        INativeManager nativeManager;
        ILevelGenerator levelGenerator;
        List<IScene> scenes;

        public EngineService()
        {

        }

        public void SynchronizeHandler (IScene sender, Engine.Helpers.Action action, uint? id, int? actionId, int? targetX, int? targetY)
        {
            
        }
    }
}
