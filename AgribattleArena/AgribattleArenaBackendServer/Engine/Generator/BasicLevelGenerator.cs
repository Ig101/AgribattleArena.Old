using AgribattleArenaBackendServer.Engine.Generator.GeneratorEntities;
using AgribattleArenaBackendServer.Models.Battle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Engine.Generator
{
    //TODO Not Ready
    public class BasicLevelGenerator : ILevelGenerator
    {
        int sizeX;
        int sizeY;

        public BasicLevelGenerator(int sizeX, int sizeY)
        {
            this.sizeX=sizeX;
            this.sizeY=sizeY;
        }

        public GenerationSet GenerateNewScene(List<PartyActor> playerActors, List<BattleUserDto> playerIds, int seed)
        {
            Random random = new Random(seed);
            GenerationTile[,] tileSet = new GenerationTile[sizeX,sizeY];
            List<GenerationObject> actors = new List<GenerationObject>();
            List<GenerationObject> decorations = new List<GenerationObject>();
            List<PlayerActorWithTile> filledPlayerActors = new List<PlayerActorWithTile>();

            //TODO Generation method
            return new GenerationSet(tileSet, actors,decorations, filledPlayerActors);
        }
    }
}
