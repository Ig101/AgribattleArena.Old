using AgribattleArena.Engine.ForExternalUse.Synchronization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArena.BackendServer.Models.Battle.Synchronization
{
    public class SynchronizerDto
    {
        public int Version { get; set; }
        public ISynchronizer Synchronizer { get; set; }
        public Engine.Helpers.Action? Action { get; set; }
        public int? ActorId { get; set; }
        public int? SkillActionId { get; set; }
        public int? TargetX { get; set; }
        public int? TargetY { get; set; }
        /*
         *         int RandomCounter { get; }
        IActor TempActor { get; }
        IActiveDecoration TempDecoration { get; }
        IEnumerable<IPlayer> Players { get; }
        IEnumerable<IActor> ChangedActors { get; }
        IEnumerable<IActiveDecoration> ChangedDecorations { get; }
        IEnumerable<ISpecEffect> ChangedEffects { get; }
        IEnumerable<IActor> DeletedActors { get; }
        IEnumerable<IActiveDecoration> DeletedDecorations { get; }
        IEnumerable<ISpecEffect> DeletedEffects { get; }
        IEnumerable<ITile> ChangedTiles { get; }
        ITile[,] TileSet { get; }*/
    }
}
