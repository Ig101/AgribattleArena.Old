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
        public Engine.Helpers.Action? Action { get; set; }
        public int? ActorId { get; set; }
        public int? SkillActionId { get; set; }
        public int? TargetX { get; set; }
        public int? TargetY { get; set; }
        public ActorDto TempActor { get; set; }
        public ActiveDecorationDto TempDecoration { get; set; }
        public IEnumerable<PlayerDto> Players { get; set; }
        public IEnumerable<ActorDto> ChangedActors { get; set; }
        public IEnumerable<ActiveDecorationDto> ChangedDecorations { get; set; }
        public IEnumerable<SpecEffectDto> ChangedEffects { get; set; }
        public IEnumerable<ActorDto> DeletedActors { get; set; }
        public IEnumerable<ActiveDecorationDto> DeletedDecorations { get; set; }
        public IEnumerable<SpecEffectDto> DeletedEffects { get; set; }
        public IEnumerable<TileDto> ChangedTiles { get; set; }
        public int TilesetWidth { get; set; }
        public int TilesetHeight { get; set; }
    }
}
