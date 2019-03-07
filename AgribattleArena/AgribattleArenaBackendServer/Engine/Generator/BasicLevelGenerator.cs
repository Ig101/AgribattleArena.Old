using AgribattleArenaBackendServer.Engine.Generator.GeneratorEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Engine.Generator
{
    //TODO Not Ready
    public class BasicLevelGenerator : ILevelGenerator
    {
        IProfileServiceSceneLink profiles;

        int sizeX;
        int sizeY;

        public BasicLevelGenerator(IProfileServiceSceneLink profiles, int sizeX, int sizeY)
        {
            this.sizeX=sizeX;
            this.sizeY=sizeY;
            this.profiles = profiles;
        }

        public GenerationSet GenerateNewScene(List<int> playerIds, int seed)
        {
            Random random = new Random(seed);
            GenerationTile[,] tileSet = new GenerationTile[sizeX,sizeY];
            List<GenerationObject> actors = new List<GenerationObject>();
            List<GenerationObject> decorations = new List<GenerationObject>();
            List<PlayerActor> playerActors = new List<PlayerActor>();

            List<PlayerActor> actorsFromService = new List<PlayerActor>();
            foreach(int playerId in playerIds)
            {

            }
            //TODO Generation method
            return new GenerationSet(tileSet, actors,decorations, playerActors);
        }
    }
}
