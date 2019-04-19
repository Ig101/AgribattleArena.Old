using AgribattleArena.Engine.Natives;
using AgribattleArena.Engine.Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgribattleArena.Engine.Helpers
{
    public static class GeneratorHelper
    {
        public static Player ConvertExternalPlayerFromGeneration (ISceneForSceneGenerator scene, ForExternalUse.Generation.ObjectInterfaces.IPlayer player)
        {
            return scene.CreatePlayer(player.Id);
        }

        public static Actor ConvertExternalActorFromGeneration (ISceneForSceneGenerator scene, Player player, Tile target,
            ForExternalUse.Generation.ObjectInterfaces.IActor actor, float? z)
        {
            return scene.CreateActor(player, actor.ExternalId, actor.NativeId,
                new RoleModelNative(scene.NativeManager, null, actor.Strength, actor.Willpower, actor.Constitution, actor.Speed,
                actor.ActionPointsIncome, actor.AttackingSkillName, actor.SkillNames.ToArray()),
                target, z);
        }
    }
}
