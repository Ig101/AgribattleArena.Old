using AgribattleArena.Configurator.Models;
using AgribattleArena.Configurator.Models.Natives;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgribattleArena.Configurator.Services
{
    static class EntityProcessorDelegates
    {
        public static (string name, string category) StoreActorCharacteristics(StoreActorDto actor)
        {
            return (actor.Name, "StoreActor");
        }

        public static (string name, string category) RevelationLevelCharacteristics(RevelationLevelDto level)
        {
            return (level.Level.ToString(), "RevelationLevel");
        }

        public static (string name, string category) BackendActorCharacteristics(BackendActorDto entity)
        {
            return (entity.Name.ToString(), "BackendActor");
        }

        public static (string name, string category) BackendBuffCharacteristics(BackendBuffDto entity)
        {
            return (entity.Name.ToString(), "BackendBuff");
        }

        public static (string name, string category) BackendDecorationCharacteristics(BackendDecorationDto entity)
        {
            return (entity.Name.ToString(), "BackendDecoration");
        }

        public static (string name, string category) BackendTileCharacteristics(BackendTileDto entity)
        {
            return (entity.Name.ToString(), "BackendTile");
        }

        public static (string name, string category) BackendRoleModelCharacteristics(BackendRoleModelDto entity)
        {
            return (entity.Name.ToString(), "BackendRoleModel");
        }

        public static (string name, string category) BackendSkillCharacteristics(BackendSkillDto entity)
        {
            return (entity.Name.ToString(), "BackendSkill");
        }

        public static (string name, string category) BackendSpecEffectCharacteristics(BackendSpecEffectDto entity)
        {
            return (entity.Name.ToString(), "BackendSpecEffect");
        }
    }
}
