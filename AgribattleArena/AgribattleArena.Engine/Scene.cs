using AgribattleArena.Engine.ForExternalUse.Synchronization.ObjectInterfaces;
using AgribattleArena.Engine.Helpers;
using AgribattleArena.Engine.NativeManagers;
using AgribattleArena.Engine.Natives;
using AgribattleArena.Engine.Objects;
using AgribattleArena.Engine.Objects.Abstract;
using AgribattleArena.Engine.SceneGenerators;
using AgribattleArena.Engine.Synchronizers;
using AgribattleArena.Engine.VarManagers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AgribattleArena.Engine
{ 
    public class Scene: ISceneParentRef, ISceneForSceneGenerator, ForExternalUse.IScene
    {
        public delegate bool DefeatConditionMethod(ISceneParentRef scene, IPlayerParentRef player);
        public delegate bool WinConditionMethod(ISceneParentRef scene);

        public event EventHandler<ForExternalUse.Synchronization.ISyncEventArgs> ReturnAction;

        bool isActive;

        long id;
        List<Player> players;
        int version;
        float remainedTurnTime;
        TileObject tempTileObject;

        WinConditionMethod winCondition;
        DefeatConditionMethod defeatCondition;

        readonly IVarManager varManager;
        INativeManager nativeManager;
        Random gameRandom;

        Tile[][] tiles;
        List<Actor> actors;
        List<ActiveDecoration> decorations;
        List<SpecEffect> specEffects;

        List<Actor> deletedActors;
        List<ActiveDecoration> deletedDecorations;
        List<SpecEffect> deletedEffects;
        int idsCounter;
        int randomCounter;

        public bool IsActive { get { return isActive; } }

        public DefeatConditionMethod DefeatCondition { get { return defeatCondition; } }
        public WinConditionMethod WinCondition { get { return winCondition; } }
        public Random GameRandom { get { return gameRandom; } }
        public Tile[][] Tiles { get { return tiles; } }
        public List<Actor> Actors { get { return actors; } }
        public List<ActiveDecoration> Decorations { get { return decorations; } }
        public List<SpecEffect> SpecEffects { get { return specEffects; } }
        public List<Actor> DeletedActors { get { return deletedActors; } }
        public List<ActiveDecoration> DeletedDecorations { get { return deletedDecorations; } }
        public List<SpecEffect> DeletedEffects { get { return deletedEffects; } }
        public int IdsCounter { get { return idsCounter; } }

        public IVarManager VarManager { get { return varManager; } }
        public INativeManager NativeManager { get { return nativeManager; } }

        public int Version { get { return version; } }
        public IEnumerable<long> PlayerIds { get { return players.Select(x => x.Id); } }
        public float RemainedTurnTime { get { return remainedTurnTime; } }
        public TileObject TempTileObject { get { return tempTileObject; } }
        public long Id { get { return id; } }
        public int RandomCounter { get { return randomCounter; } }
        public IEnumerable<Player> Players { get { return players; } }

        public Scene(long id, IEnumerable<ForExternalUse.Generation.ObjectInterfaces.IPlayer> players, ForExternalUse.Generation.ISceneGenerator generator, 
            ForExternalUse.INativeManager nativeManager, ForExternalUse.IVarManager varManager, int seed)
        {
            this.isActive = true;
            this.id = id;
            this.gameRandom = new Random(seed);
            this.nativeManager = (INativeManager)nativeManager;
            this.varManager = (IVarManager)varManager;
            ISceneGenerator tempGenerator = (ISceneGenerator)generator;
            this.winCondition = tempGenerator.WinCondition;
            this.defeatCondition = tempGenerator.DefeatCondition;
            this.idsCounter = 0;
            this.players = new List<Player>();
            this.actors = new List<Actor>();
            this.decorations = new List<ActiveDecoration>();
            this.specEffects = new List<SpecEffect>();
            this.deletedActors = new List<Actor>();
            this.deletedDecorations = new List<ActiveDecoration>();
            this.deletedEffects = new List<SpecEffect>();
            tempGenerator.GenerateNewScene(this, players, unchecked(seed * (int)id));
        }

        public float GetNextRandom()
        {
            randomCounter++;
            return (float)gameRandom.NextDouble();
        }

        public int GetNextId()
        {
            int tempId = idsCounter;
            idsCounter++;
            return tempId;
        }

        public List<Actor> GetPlayerActors(Player player)
        {
            return actors.FindAll(x => x.Owner == player);
        }

        #region Creation
        public Tile[][] SetupEmptyTileSet (int width, int height)
        {
            Tile[][] tiles= new Tile[width][];
            for(int i = 0; i<width;i++)
            {
                tiles[i] = new Tile[height];
            }
            this.tiles = tiles;
            return tiles;
        }

        public Player CreatePlayer (long id, int? team)
        {
            Player player = new Player(this, id, team);
            players.Add(player);
            return player;
        }

        public Actor CreateActor(Player owner, string nativeName, string roleNativeName, Tile target, float? z)
        {
            return CreateActor(owner, null, nativeName, nativeManager.GetRoleModelNative(roleNativeName), target, z);
        }

        public Actor CreateActor(Player owner, long? externalId, string nativeName, RoleModelNative roleModel, Tile target, float? z)
        {
            if (target.TempObject != null) return null;
            Actor actor = new Actor(this, owner, externalId, target, z, nativeManager.GetActorNative(nativeName), roleModel);
            actors.Add(actor);
            return actor;
        }

        public ActiveDecoration CreateDecoration(Player owner, string nativeName, Tile target, float? z, int? health, TagSynergy[] armor, float? mod)
        {
            if (target.TempObject != null) return null;
            ActiveDecoration decoration = new ActiveDecoration(this, owner, target, z, health, armor, nativeManager.GetDecorationNative(nativeName), mod);
            decorations.Add(decoration);
            return decoration;
        }

        public SpecEffect CreateEffect(Player owner, string nativeName, Tile target, float? z, float? duration, float? mod)
        {
            SpecEffect effect = new SpecEffect(this, owner, target.X, target.Y, z, nativeManager.GetEffectNative(nativeName), duration, mod);
            specEffects.Add(effect);
            return effect;
        }

        public Tile ChangeTile(string nativeName, int x, int y, int? height, Player owner)
        {
            if (tiles[x][y] == null) return null;
            Tile tile = tiles[x][y];
            tile.Owner = owner;
            tile.Native = nativeManager.GetTileNative(nativeName);
            if (height != null) tile.Height = height.Value;
            if(tile.Native.Unbearable && tile.TempObject!=null)
            {
                tile.TempObject.Kill();
            }
            if(tile.TempObject!=null)
            {
                tiles[x][y].Native.OnStepAction(this, tiles[x][y]);
            }
            return tile;
        }

        public Tile CreateTile(string nativeName, int x, int y, int? height)
        {
            if (tiles[x][y] != null) return null;
            Tile tile = new Tile(this, x, y, nativeManager.GetTileNative(nativeName), height);
            tiles[x][y] = tile;
            return tile;
        }
        #endregion

        #region Sync
        ForExternalUse.Synchronization.ISynchronizer GetSynchronizationDataPlayersOnly()
        {
            return new Synchronizer(tempTileObject, players, new List<Actor>(), new List<ActiveDecoration>(), new List<SpecEffect>(), new List<Actor>(),
                new List<ActiveDecoration>(), new List<SpecEffect>(), new Point(tiles.Length, tiles[0].Length), new List<Tile>(), randomCounter);
        }
        
        ForExternalUse.Synchronization.ISynchronizer GetSynchronizationData(bool nullify)
        {
            List<Actor> changedActors = actors.FindAll(x => x.Affected);
            List<ActiveDecoration> changedDecorations = decorations.FindAll(x => x.Affected);
            List<SpecEffect> changedEffects = specEffects.FindAll(x => x.Affected);
            List<Tile> changedTiles = new List<Tile>();
            for (int x = 0; x < tiles.Length; x++)
            {
                for (int y = 0; y < tiles[x].Length; y++)
                {
                    if (tiles[x][y].Affected) changedTiles.Add(tiles[x][y]);
                }
            }
            Synchronizer sync = new Synchronizer(tempTileObject, players, changedActors, changedDecorations, changedEffects, deletedActors, deletedDecorations, 
                deletedEffects, new Point(tiles.Length, tiles[0].Length), changedTiles, randomCounter);
            if (nullify)
            {
                changedActors.ForEach(x => x.Affected = false);
                changedDecorations.ToList().ForEach(x => x.Affected = false);
                changedEffects.ToList().ForEach(x => x.Affected = false);
                changedTiles.ToList().ForEach(x => x.Affected = false);
                this.deletedDecorations.Clear();
                this.deletedActors.Clear();
                this.deletedEffects.Clear();
            }
            return sync;
        }

        ForExternalUse.Synchronization.ISynchronizer GetSynchronizationData()
        {
            return GetSynchronizationData(false);
        }

        ForExternalUse.Synchronization.ISynchronizer GetFullSynchronizationData(bool nullify)
        {
            if (nullify)
            {
                actors.ForEach(x => x.Affected = false);
                decorations.ToList().ForEach(x => x.Affected = false);
                specEffects.ToList().ForEach(x => x.Affected = false);
                for (int x = 0; x < tiles.Length; x++)
                {
                    for (int y = 0; y < tiles[x].Length; y++)
                    {
                        tiles[x][y].Affected = false;
                    }
                }
                this.deletedDecorations.Clear();
                this.deletedActors.Clear();
                this.deletedEffects.Clear();
            }
            return new SynchronizerFull(tempTileObject, players, actors, decorations, specEffects, tiles, randomCounter);

        }

        public ForExternalUse.Synchronization.ISynchronizer GetFullSynchronizationData()
        {
            return GetFullSynchronizationData(false);
        }
        #endregion

        #region Updates
        public void StartGame()
        {
            this.ReturnAction(this, new SyncEventArgs(this, version++, Helpers.Action.StartGame, GetFullSynchronizationData(true), 
                null,null,null,null));
            EndTurn();
        }
        public void EndTurn()
        {
            bool turnStarted;
            do
            {
                tempTileObject?.EndTurn();
                float minInitiativePosition = float.MaxValue;
                TileObject newObject = null;
                foreach (TileObject obj in actors)
                {
                    if (obj.IsAlive && obj.InitiativePosition < minInitiativePosition)
                    {
                        minInitiativePosition = obj.InitiativePosition;
                        newObject = obj;
                    }
                }
                foreach (TileObject obj in decorations)
                {
                    if (obj.IsAlive && obj.InitiativePosition < minInitiativePosition)
                    {
                        minInitiativePosition = obj.InitiativePosition;
                        newObject = obj;
                    }
                }
                if (newObject != null)
                {
                    this.remainedTurnTime = newObject.Owner == null || newObject.Owner.TurnsSkipped <= 0 ? varManager.TurnTimeLimit : varManager.TurnTimeLimitAfterSkip;
                    TileObject previousTempTileObject = this.tempTileObject;
                    this.tempTileObject = newObject;
                    Update(minInitiativePosition);
                    if (AfterUpdateSynchronization(Helpers.Action.EndTurn, previousTempTileObject, null, null, null))
                    {
                        turnStarted = this.tempTileObject.StartTurn();
                    }
                    else
                    {
                        turnStarted = true;
                        isActive = false;
                    }
                }
                else
                {
                    turnStarted = true;
                    isActive = false;
                    AfterUpdateSynchronization(Helpers.Action.NoActorsDraw, null, null, null, null);
                }
            } while (!turnStarted);
        }

        void Update(float time)
        {
            foreach (TileObject obj in actors)
            {
                obj.Update(time);
            }
            foreach (TileObject obj in decorations)
            {
                obj.Update(time);
            }
            foreach (SpecEffect eff in specEffects)
            {
                eff.Update(time);
            }
        }

        void AfterActionUpdate()
        {
            foreach (Player player in players)
            {
                if (player.Status == PlayerStatus.Playing &&
                    (player.TurnsSkipped >= varManager.SkippedTurnsLimit || defeatCondition(this, player)))
                {
                    player.Defeat();
                }
            }
            for (int i = 0; i < actors.Count; i++)
            {
                if (!actors[i].IsAlive)
                {
                    deletedActors.Add(actors[i]);
                    actors.RemoveAt(i);
                    i--;
                }
            }
            for (int i = 0; i < decorations.Count; i++)
            {
                if (!decorations[i].IsAlive)
                {
                    deletedDecorations.Add(decorations[i]);
                    decorations.RemoveAt(i);
                    i--;
                }
            }
            for (int i = 0; i < specEffects.Count; i++)
            {
                if (!specEffects[i].IsAlive)
                {
                    deletedEffects.Add(specEffects[i]);
                    specEffects.RemoveAt(i);
                    i--;
                }
            }
        }

        //TODO Lock
        public void UpdateTime(float time)
        {
            this.remainedTurnTime -= time;
            if(remainedTurnTime<=0)
            {
                IPlayerParentRef player = tempTileObject.Owner;
                TileObject previousTempTileObject = tempTileObject;
                if (player!=null)
                {
                    player.SkipTurn();
                    AfterUpdateSynchronization(Helpers.Action.SkipTurn, tempTileObject, null, null, null);
                }
                tempTileObject.EndTurn();
            }
        }

        public bool AfterUpdateSynchronization (Helpers.Action action, TileObject actor, int? actionId, int? targetX, int? targetY)
        {
            AfterActionUpdate();
            this.ReturnAction?.Invoke(this, new SyncEventArgs(this, version++, action, GetSynchronizationData(true), actor?.Id, actionId, targetX, targetY));
            if (winCondition(this))
            {
                foreach(Player player in players)
                {
                    if (player.Status == PlayerStatus.Playing) player.Victory();
                }
                this.ReturnAction?.Invoke(this, new SyncEventArgs(this, version++, Helpers.Action.EndGame, GetSynchronizationDataPlayersOnly(), null, null, null, null));
                return false;
            }
            return true;
        }
        #endregion

        #region Actions
        void ApplyActionAfterSkipping()
        {
            remainedTurnTime += varManager.TurnTimeLimit - varManager.TurnTimeLimitAfterSkip;
        }

        public bool DecorationCast(ActiveDecoration actor)
        {
            if (tempTileObject == actor)
            {
                if(actor.Owner?.ActThisTurn() ?? false)
                {
                    ApplyActionAfterSkipping();
                }
                actor.Cast();
                if(AfterUpdateSynchronization(Helpers.Action.Decoration, actor, null, null, null))
                    EndTurn();
                return true;
            }
            return false;
        }

        public bool ActorMove(int actorId, int targetX, int targetY)
        {
            if (tempTileObject.Id == actorId)
            {
                Actor actor = (Actor)tempTileObject;
                if (actor.Owner?.ActThisTurn() ?? false)
                {
                    ApplyActionAfterSkipping();
                }
                bool result = actor.Move(tiles[targetX][targetY]);
                if (result)
                {
                    if(AfterUpdateSynchronization(Helpers.Action.Move, actor, null, targetX, targetY) && !actor.CheckActionAvailability())
                        EndTurn();
                }
                return result;
            }
            return false;
        }

        public bool ActorCast(int actorId, int skillId, int targetX, int targetY)
        {
            if (tempTileObject.Id == actorId)
            {
                Actor actor = (Actor)tempTileObject;
                if (actor.Owner?.ActThisTurn() ?? false)
                {
                    ApplyActionAfterSkipping();
                }
                bool result = actor.Cast(skillId, tiles[targetX][targetY]);
                if (result)
                {
                    if (AfterUpdateSynchronization(Helpers.Action.Cast, actor, null, targetX, targetY) && !actor.CheckActionAvailability())
                        EndTurn();
                }
                return result;
            }
            return false;
        }

        public bool ActorAttack(int actorId, int targetX, int targetY)
        {
            if (tempTileObject.Id == actorId)
            {
                Actor actor = (Actor)tempTileObject;
                if (actor.Owner?.ActThisTurn() ?? false)
                {
                    ApplyActionAfterSkipping();
                }
                bool result = actor.Attack(tiles[targetX][targetY]);
                if (result)
                {
                    if (AfterUpdateSynchronization(Helpers.Action.Attack, actor, null, targetX, targetY) && !actor.CheckActionAvailability())
                        EndTurn();
                }
                return result;
            }
            return false;
        }

        public bool ActorWait(int actorId)
        {
            if (tempTileObject.Id == actorId)
            {
                Actor actor = (Actor)tempTileObject;
                if (actor.Owner?.ActThisTurn() ?? false)
                {
                    ApplyActionAfterSkipping();
                }
                bool result = actor.Wait();
                if (result)
                {
                    if (AfterUpdateSynchronization(Helpers.Action.Wait, actor, null, null, null))
                        EndTurn();
                }
                return result;
            }
            return false;
        }
        #endregion
    }
}
