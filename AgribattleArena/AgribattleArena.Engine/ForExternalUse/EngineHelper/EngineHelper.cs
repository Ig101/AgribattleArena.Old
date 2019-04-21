using AgribattleArena.Engine.ForExternalUse.Generation;
using AgribattleArena.Engine.ForExternalUse.Generation.ObjectInterfaces;
using AgribattleArena.Engine.ForExternalUse.Synchronization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgribattleArena.Engine.ForExternalUse.EngineHelper
{
    public static class EngineHelper
    {
        #region SceneGenerators
        /// <summary>
        /// 2 players, less than 5 actors per player, 20x20 tileSet, actors will be at 2 and 17 X tile and 2+i*2 Y tile
        /// </summary>
        /// <param name="tileSet"></param>
        /// <returns></returns>
        public static ISceneGenerator CreateTestSceneGenerator(string[,] tileSet)
        {
            return new SceneGenerators.TestSceneGenerator(tileSet);
        }
        #endregion

        public static IActor CreateActorForGeneration (int externalId, string nativeId, string attackingSkillName, int strength, int willpower,
            int constitution, int speed, IEnumerable<string> skillNames, int actionPointsIncome)
        {
            return new Synchronizers.SynchronizationObjects.Actor(externalId, nativeId, attackingSkillName, strength, willpower, constitution,
                speed, skillNames.ToList(), actionPointsIncome);
        }

        public static IPlayer CreatePlayerForGeneration (int id, IEnumerable<IActor> keyActors)
        {
            return new Synchronizers.SynchronizationObjects.Player(id, keyActors.ToList());
        }

        public static IVarManager CreateVarManager(float turnTimeLimit, float turnTimeLimitAfterSkip, int skippedTurnsLimit, int tileSize, int maxActionPoints,
            int constitutionMod, float willpowerMod, float strengthMod, float speedMod)
        {
            return new VarManagers.VarManager(turnTimeLimit, turnTimeLimitAfterSkip, skippedTurnsLimit, tileSize, maxActionPoints,
                constitutionMod, willpowerMod, strengthMod, speedMod);
        }

        public static INativeManager CreateNativeManager()
        {
            return new NativeManagers.NativeManager();
        }

        public static IScene CreateNewScene(int id, IEnumerable<Generation.ObjectInterfaces.IPlayer> players, ISceneGenerator generator,
            INativeManager nativeManager, IVarManager varManager, int seed, EventHandler<ISyncEventArgs> eventHandler)
        {
            Scene scene = new Scene(id, players, generator, nativeManager, varManager, seed);
            scene.ReturnAction += eventHandler;
            scene.StartGame();
            return scene;
        }
    }
}
