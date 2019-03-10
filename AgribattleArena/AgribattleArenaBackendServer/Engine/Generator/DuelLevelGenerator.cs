using AgribattleArenaBackendServer.Engine.Generator.GeneratorEntities;
using AgribattleArenaBackendServer.Engine.Helpers;
using AgribattleArenaBackendServer.Models.Battle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Engine.Generator
{
    //TODO Not Ready
    public class DuelLevelGenerator : ILevelGenerator
    {
        int sizeX;
        int sizeY;

        public DuelLevelGenerator()
        {
            this.sizeX = Misc.duelMapSide;
            this.sizeY = Misc.duelMapSide;
        }

        public GenerationSet GenerateNewScene(List<GenerationPartyActor> playerActors, List<BattleUserDto> playerIds, int seed)
        {
            Random random = new Random(seed);
            GenerationTile[,] tileSet = new GenerationTile[sizeX,sizeY];
            List<GenerationObject> actors = new List<GenerationObject>();
            List<GenerationObject> decorations = new List<GenerationObject>();
            List<GenerationPartyActorWithTile> filledPlayerActors = new List<GenerationPartyActorWithTile>();

            //TODO Generation method
            return new GenerationSet(tileSet, actors,decorations, filledPlayerActors);
        }
    }
}
