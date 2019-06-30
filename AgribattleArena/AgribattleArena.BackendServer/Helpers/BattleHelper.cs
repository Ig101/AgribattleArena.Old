using AgribattleArena.BackendServer.Models.Battle;
using AgribattleArena.BackendServer.Models.Battle.Synchronization;
using AgribattleArena.Engine.ForExternalUse;
using AgribattleArena.Engine.ForExternalUse.EngineHelper;
using AgribattleArena.Engine.ForExternalUse.Synchronization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArena.BackendServer.Helpers
{
    public static class BattleHelper
    {
        public static Dictionary<string, SceneModeQueueDto> GetNewModeQueue()
        {
            return new Dictionary<string, SceneModeQueueDto>()
            {
                { "main_duel", new SceneModeQueueDto()
                    {
                        Queue = new List<ProfileQueueDto>(),
                        Mode = new SceneModeDto()
                        {
                            Generator = EngineHelper.CreateDuelSceneGenerator(),
                            VarManager = EngineHelper.CreateVarManager(80, 20, 3, 8, 5, 0.1f, 0.1f, 0.1f),
                            BattleResultProcessor = BattleResultDelegates.ProcessMainDuelBattleResult,
                            MaxPlayers = 2
                        }
                    }
                }
            };
        }

        public static SynchronizerDto GetFullSynchronizationData (IScene scene)
        {
            var synchronizer = scene.GetFullSynchronizationData();
            return new SynchronizerDto()
            {
                Action = null,
                ActorId = null,
                SkillActionId = null,
                Synchronizer = synchronizer,
                TargetX = null,
                TargetY = null,
                Version = scene.Version
            };
        }
    }
}
