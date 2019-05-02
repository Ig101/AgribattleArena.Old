using AgribattleArena.BackendServer.Models.Battle;
using AgribattleArena.Engine.ForExternalUse.EngineHelper;
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
                            BattleResultProcessor = BattleResultDelegates.ProcessMainDuelBattleResult,
                            MaxPlayers = 2
                        }
                    }
                }
            };
        }
    }
}
