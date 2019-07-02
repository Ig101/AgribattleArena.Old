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

        static bool ComparePlayerTeams(ISynchronizer oldSynchronizer, string ownerId, IPlayerShort tempPlayer)
        {
            return tempPlayer.Team != null && oldSynchronizer.Players.FirstOrDefault(k => k.Id == ownerId).Team == tempPlayer.Team;
        }

        static ActorDto MapActor(ISynchronizer oldSynchronizer, Engine.ForExternalUse.Synchronization.ObjectInterfaces.IActor actor, IPlayerShort tempPlayer)
        {
            bool owner = ComparePlayerTeams(oldSynchronizer, actor.OwnerId, tempPlayer);
            return new ActorDto()
            {
                ActionPoints = actor.ActionPoints,
                ActionPointsIncome = actor.ActionPointsIncome,
                Armor = AutoMapper.Mapper.Map<List<Models.Natives.TagSynergyDto>>(actor.Armor),
                AttackingSkill = new SkillDto()
                {
                    Cd = actor.AttackingSkill.Cd,
                    Cost = actor.AttackingSkill.Cost,
                    Id = actor.AttackingSkill.Id,
                    Mod = actor.AttackingSkill.Mod,
                    NativeId = owner ? actor.AttackingSkill.SecretNativeId : actor.AttackingSkill.NativeId,
                    PreparationTime = actor.AttackingSkill.PreparationTime,
                    Range = actor.AttackingSkill.Range
                },
                AttackModifiers = AutoMapper.Mapper.Map<List<Models.Natives.TagSynergyDto>>(actor.AttackModifiers),
                AttackPower = actor.AttackPower,
                Buffs = actor.Buffs.Select(k => new BuffDto()
                {
                    Duration = k.Duration,
                    Id = k.Id,
                    Mod = k.Mod,
                    NativeId = owner ? k.SecretNativeId : k.NativeId
                }),
                Constitution = actor.Constitution,
                NativeId = actor.NativeId,
                Id = actor.Id,
                ExternalId = actor.ExternalId,
                Health = actor.Health,
                Initiative = actor.Initiative,
                InitiativePosition = actor.InitiativePosition,
                IsAlive = actor.IsAlive,
                MaxHealth = actor.MaxHealth,
                OwnerId = actor.OwnerId,
                SkillPower = actor.SkillPower,
                Skills = actor.Skills.Select(k => new SkillDto()
                {
                    Cd = k.Cd,
                    Cost = k.Cost,
                    Id = k.Id,
                    Mod = k.Mod,
                    NativeId = owner ? k.SecretNativeId : k.NativeId,
                    PreparationTime = k.PreparationTime,
                    Range = k.Range
                }),
                Speed = actor.Speed,
                Strength = actor.Strength,
                Willpower = actor.Willpower,
                X = actor.X,
                Y = actor.Y,
                Z = actor.Z
            };
        }

        static ActiveDecorationDto MapDecoration (ISynchronizer oldSynchronizer, Engine.ForExternalUse.Synchronization.ObjectInterfaces.IActiveDecoration decoration, IPlayerShort tempPlayer)
        {
            return new ActiveDecorationDto()
            {
                Armor = AutoMapper.Mapper.Map<List<Models.Natives.TagSynergyDto>>(decoration.Armor),
                Health = decoration.Health,
                Id = decoration.Id,
                InitiativePosition = decoration.InitiativePosition,
                IsAlive = decoration.IsAlive,
                MaxHealth = decoration.MaxHealth,
                Mod = decoration.Mod,
                NativeId = decoration.NativeId,
                OwnerId = decoration.OwnerId,
                X = decoration.X,
                Y = decoration.Y,
                Z = decoration.Z
            };
        }

        static SpecEffectDto MapEffect (ISynchronizer oldSynchronizer, Engine.ForExternalUse.Synchronization.ObjectInterfaces.ISpecEffect effect, IPlayerShort tempPlayer)
        {
            bool owner = ComparePlayerTeams(oldSynchronizer, effect.OwnerId, tempPlayer);
            return new SpecEffectDto()
            {
                Duration = effect.Duration,
                Id = effect.Id,
                IsAlive = effect.IsAlive,
                Mod = effect.Mod,
                NativeId = owner ? effect.SecretNativeId : effect.NativeId,
                OwnerId = effect.OwnerId,
                X = effect.X,
                Y = effect.Y,
                Z = effect.Z
            };
        }

        static TileDto MapTile(ISynchronizer oldSynchronizer, Engine.ForExternalUse.Synchronization.ObjectInterfaces.ITile tile, IPlayerShort tempPlayer)
        {
            bool owner = ComparePlayerTeams(oldSynchronizer, tile.OwnerId, tempPlayer);
            return new TileDto()
            {
                Height = tile.Height,
                NativeId = owner ? tile.SecretNativeId : tile.NativeId,
                OwnerId = tile.OwnerId,
                TempActorId = tile.TempActorId,
                X = tile.X,
                Y = tile.Y
            };
        }

        static SynchronizerDto MapSynchronizer(ISynchronizer oldSynchronizer, IPlayerShort tempPlayer)
        {
            var tileSet = oldSynchronizer.TileSet;
            return new SynchronizerDto()
            {
                ChangedActors = oldSynchronizer.ChangedActors.Select(x => MapActor(oldSynchronizer, x, tempPlayer)),
                DeletedActors = oldSynchronizer.DeletedActors.Select(x => MapActor(oldSynchronizer, x, tempPlayer)),
                ChangedDecorations = oldSynchronizer.ChangedDecorations.Select(x => MapDecoration(oldSynchronizer, x, tempPlayer)),
                DeletedDecorations = oldSynchronizer.DeletedDecorations.Select(x => MapDecoration(oldSynchronizer, x, tempPlayer)),
                ChangedEffects = oldSynchronizer.ChangedEffects.Select(x => MapEffect(oldSynchronizer, x, tempPlayer)),
                DeletedEffects = oldSynchronizer.DeletedEffects.Select(x => MapEffect(oldSynchronizer, x, tempPlayer)),
                Players = oldSynchronizer.Players.Select(x => new PlayerDto()
                {
                    Id = x.Id,
                    KeyActorsSync = x.KeyActorsSync,
                    Status = x.Status,
                    Team = x.Team,
                    TurnsSkipped = x.TurnsSkipped
                }),
                ChangedTiles = oldSynchronizer.ChangedTiles.Select(x => MapTile(oldSynchronizer, x, tempPlayer)),
                TempActor = oldSynchronizer.TempActor == null ? null : MapActor(oldSynchronizer, oldSynchronizer.TempActor, tempPlayer),
                TempDecoration = oldSynchronizer.TempDecoration == null ? null : MapDecoration(oldSynchronizer, oldSynchronizer.TempDecoration, tempPlayer),
                TilesetHeight = tileSet.GetLength(1),
                TilesetWidth = tileSet.GetLength(0)
            };
        }

        public static SynchronizerDto MapSynchronizer(ISyncEventArgs syncEventArgs, IPlayerShort tempPlayer)
        {
            var synchronizer = MapSynchronizer(syncEventArgs.SyncInfo, tempPlayer);
            synchronizer.Action = syncEventArgs.Action;
            synchronizer.TargetX = syncEventArgs.TargetX;
            synchronizer.TargetY = syncEventArgs.TargetY;
            synchronizer.SkillActionId = syncEventArgs.SkillActionId;
            synchronizer.ActorId = syncEventArgs.ActorId;
            synchronizer.Version = syncEventArgs.Version;
            return synchronizer;
        }

        public static SynchronizerDto MapSynchronizer(ISyncEventArgs syncEventArgs, string tempPlayer)
        {
            return MapSynchronizer(syncEventArgs, syncEventArgs.Scene.ShortPlayers.FirstOrDefault(x => x.Id == tempPlayer));
        }

        public static SynchronizerDto MapSynchronizer(ISynchronizer oldSynchronizer, IScene scene, IPlayerShort tempPlayer)
        {
            var synchronizer = MapSynchronizer(oldSynchronizer, tempPlayer);
            synchronizer.Version = scene.Version;
            return synchronizer;
        }

        public static SynchronizerDto MapSynchronizer(ISynchronizer oldSynchronizer, IScene scene, string tempPlayer)
        {
            return MapSynchronizer(oldSynchronizer, scene.ShortPlayers.FirstOrDefault(x => x.Id == tempPlayer));
        }

        public static SynchronizerDto GetFullSynchronizationData (IScene scene, string tempPlayer)
        {
            var synchronizer = scene.GetFullSynchronizationData();
            return MapSynchronizer(synchronizer, scene, tempPlayer);
        }
    }
}
