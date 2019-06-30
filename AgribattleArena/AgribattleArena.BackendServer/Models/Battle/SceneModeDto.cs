using AgribattleArena.BackendServer.Helpers;
using AgribattleArena.Engine.ForExternalUse;
using AgribattleArena.Engine.ForExternalUse.Generation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArena.BackendServer.Models.Battle
{
    public class SceneModeDto
    {
        public int MaxPlayers { get; set; }
        public ISceneGenerator Generator { get; set; }
        public IVarManager VarManager { get; set; }
        public ProcessBattleResult BattleResultProcessor { get; set; }
    }
}
