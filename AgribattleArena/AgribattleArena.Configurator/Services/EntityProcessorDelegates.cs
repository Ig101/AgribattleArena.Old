using AgribattleArena.Configurator.Models;
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
    }
}
