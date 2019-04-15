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

namespace AgribattleArena.Engine
{ 
    public class Scene: ISceneParentRef, ForExternalUse.IScene
    {
        public event EventHandler<SyncEventArgs> ReturnAction;

        int id;
        List<Player> players;

        readonly IVarManager varManager;
        INativeManager nativeManager;
        Random gameRandom;

        Tile[,] tiles;
        List<TileObject> actors;
        List<SpecEffect> specEffects;
        TileObject tempTileObject;

        List<TileObject> deletedActors;
        List<SpecEffect> deletedEffects;
        int idsCounter;
        int randomCounter;

        public IVarManager VarManager { get { return varManager; } }
        public INativeManager NativeManager { get { return nativeManager; } }

        public TileObject TempTileObject { get { return tempTileObject; } }
        public int Id { get { return id; } }
        public int RandomCounter { get { return randomCounter; } }
        public IEnumerable<Player> Players { get { return players; } }

        /*public Scene(int id, List<int> playerIds, List<> playerActors, ForExternalUse.ISceneGenerator generator, ForExternalUse.INativeManager nativeManager, int seed)
        {
            this.id = id;
            this.gameRandom = new Random(seed);
            this.nativeManager = (INativeManager)nativeManager;
            ISceneGenerator tempGenerator = (ISceneGenerator)generator;

            players = new List<Player>();
            foreach(int playerId in playerIds)
            {
                players.Add(new Player(this, playerId));
            }

            idsCounter = 0;
            actors = new List<TileObject>();
            specEffects = new List<SpecEffect>();
            deletedActors = new List<TileObject>();
            deletedEffects = new List<SpecEffect>();
            GenerationSet set = generator.GenerateNewScene(playerActors, players, unchecked(seed * id));
            tiles = new Tile[set.TileSet.GetLength(0), set.TileSet.GetLength(1)];
            for (int x = 0; x < tiles.GetLength(0); x++)
            {
                for (int y = 0; y < tiles.GetLength(1); y++)
                {
                    CreateTile(set.TileSet[x, y].Native, x, y, set.TileSet[x, y].Height);
                }
            }

            foreach (GenerationPartyActorWithTile actor in set.PartyActors)
            {
                RoleModelNativeDto roleModel = new RoleModelNativeDto()
                {
                    ActionPointsIncome = actor.RoleModel.ActionPointsIncome,
                    Armor = actor.RoleModel.Armor.Select(tag => new TagSynergy(tag.SelfTag, tag.TargetTag, tag.Mod)).ToList(),
                    AttackingSkill = nativeManager.GetSkillNative(actor.RoleModel.AttackSkill),
                    Constitution = actor.RoleModel.Constitution,
                    Skills = actor.RoleModel.Skills.Select(skill => nativeManager.GetSkillNative(skill)).ToList(),
                    Id = actor.RoleModel.Id,
                    Speed = actor.RoleModel.Speed,
                    Strength = actor.RoleModel.Strength,
                    Tags = actor.RoleModel.Tags,
                    Willpower = actor.RoleModel.Willpower
                };
                CreateActor(actor.Owner, actor.Native, roleModel, tiles[actor.TileX, actor.TileY], null);
            }
            foreach (GenerationObject actor in set.Actors)
            {
                CreateActor(actor.Owner, actor.Native, actor.RoleNative, tiles[actor.TileX, actor.TileY], null);
            }
            foreach (GenerationObject decoration in set.Decorations)
            {
                CreateDecoration(decoration.Owner, decoration.Native, tiles[decoration.TileX, decoration.TileY], null, null, null, null);
            }
            EndTurn();
        }*/

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

        public Tile GetTile(float x, float y)
        {
            return tiles[(int)(x / VarManager.TileSize), (int)(y / VarManager.TileSize)];
        }

        #region Creation
        public Actor CreateActor(Player owner, string native, string roleNative, Tile target, float? z)
        {
            return CreateActor(owner, native, nativeManager.GetRoleModelNative(roleNative), target, z);
        }

        public Actor CreateActor(Player owner, string native, RoleModelNative roleModel, Tile target, float? z)
        {
            Actor actor = new Actor(this, owner, target, z, nativeManager.GetActorNative(native), roleModel);
            actors.Add(actor);
            return actor;
        }

        public ActiveDecoration CreateDecoration(Player owner, string native, Tile target, float? z, int? health, TagSynergy[] armor, float? mod)
        {
            ActiveDecoration actor = new ActiveDecoration(this, owner, target, z, health, armor, nativeManager.GetDecorationNative(native), mod);
            actors.Add(actor);
            return actor;
        }

        public SpecEffect CreateEffect(Player owner, string native, float x, float y, float? z, float? duration, float? mod)
        {
            SpecEffect effect = new SpecEffect(this, owner, x, y, z, nativeManager.GetEffectNative(native), duration, mod);
            specEffects.Add(effect);
            return effect;
        }

        public Tile CreateTile(string native, int x, int y, int? height)
        {
            Tile tile = new Tile(this, x, y, nativeManager.GetTileNative(native), height);
            tiles[x, y] = tile;
            return tile;
        }
        #endregion

        #region Sync
     /*   public SynchronizationObject GetSynchronizationData(bool nullify)
        {
            SynchronizationObject sync = new SynchronizationObject(actors, deletedActors, deletedEffects, tiles, randomCounter);
            if (nullify)
            {
                sync.ChangedActors.ForEach(x => x.Affected = false);
                sync.ChangedTiles.ForEach(x => x.Affected = false);
                this.deletedActors.Clear();
                this.deletedEffects.Clear();
            }
            return sync;
        }

        public SynchronizationObject GetSynchronizationData()
        {
            return GetSynchronizationData(false);
        }

        public FullSynchronizationObject GetFullSynchronizationData()
        {
            return new FullSynchronizationObject(actors, specEffects, tiles, randomCounter);
        }*/
        #endregion

        #region Updates
        public void EndTurn()
        {
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
            if (newObject != null)
            {
                this.tempTileObject = newObject;
                Update(minInitiativePosition);
                //ReturnAction(this, Engine.Helpers.Action.EndTurn, null, null, null, null, GetSynchronizationData(true));
                this.tempTileObject.StartTurn();
            }
        }

        void Update(float time)
        {
            foreach (TileObject obj in actors)
            {
                obj.Update(time);
            }
            foreach (SpecEffect eff in specEffects)
            {
                eff.Update(time);
            }
            AfterActionUpdate();
        }

        void AfterActionUpdate()
        {
            for (int i = 0; i < actors.Count; i++)
            {
                if (!actors[i].IsAlive)
                {
                    deletedActors.Add(actors[i]);
                    actors.RemoveAt(i);
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
            //TODO WinCondition
        }
        #endregion

        #region Actions
        public bool DecorationCast(ActiveDecoration actor)
        {
            if (tempTileObject == actor)
            {
                actor.Cast();
                AfterActionUpdate();
                //this.ReturnAction(this, Engine.Helpers.Action.Move, actor.Id, null, null, null, GetSynchronizationData(true));
                return true;
            }
            return false;
        }

        public bool ActorMove(Actor actor, Tile target)
        {
            if (tempTileObject == actor)
            {
                bool result = actor.Move(target);
                if (result)
                {
                    AfterActionUpdate();
                    //this.ReturnAction(this, Engine.Helpers.Action.Move, actor.Id, null, target.X, target.Y, GetSynchronizationData(true));
                }
                return result;
            }
            return false;
        }

        public bool ActorCast(Actor actor, int id, Tile target)
        {
            if (tempTileObject == actor)
            {
                bool result = actor.Cast(id, target);
                if (result)
                {
                    AfterActionUpdate();
                    //this.ReturnAction(this, Engine.Helpers.Action.Cast, actor.Id, id, target.X, target.Y, GetSynchronizationData(true));
                }
                return result;
            }
            return false;
        }

        public bool ActorAttack(Actor actor, Tile target)
        {
            if (tempTileObject == actor)
            {
                bool result = actor.Attack(target);
                if (result)
                {
                    AfterActionUpdate();
                    //this.ReturnAction(this, Engine.Helpers.Action.Attack, actor.Id, null, target.X, target.Y, GetSynchronizationData(true));
                }
                return result;
            }
            return false;
        }

        public bool ActorWait(Actor actor)
        {
            if (tempTileObject == actor)
            {
                bool result = actor.Wait();
                if (result)
                {
                    AfterActionUpdate();
                    //this.ReturnAction(this, Engine.Helpers.Action.Wait, actor.Id, null, null, null, GetSynchronizationData(true));
                }
                return result;
            }
            return false;
        }
        #endregion
    }
}
