using AgribattleArena.Configurator.Models;
using AgribattleArena.Configurator.Models.Natives;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AgribattleArena.Configurator.Services
{
    class ChangingDocumentProcessor
    {
        ProfilesRepository _profiles;
        NativesRepository _natives;
        StoreRepository _store;

        public ChangingDocumentProcessor(ProfilesRepository profiles, NativesRepository natives, StoreRepository store)
        {
            _profiles = profiles;
            _natives = natives;
            _store = store;
        }

        public List<Task> Process(ChangingDocumentDto document)
        {
            var tasks = new List<Task>();
            tasks.Add(new EntityProcessor<StoreActorDto>(EntityProcessorDelegates.StoreActorCharacteristics)
                .Process(_store, document.StoreActors));
            tasks.Add(new EntityProcessor<RevelationLevelDto>(EntityProcessorDelegates.RevelationLevelCharacteristics)
                .Process(_profiles, document.RevelationLevels));
            if(document.BackendNatives != null)
            {
                tasks.Add(new EntityProcessor<BackendActorDto>(EntityProcessorDelegates.BackendActorCharacteristics)
                    .Process(_natives, document.BackendNatives.Actors));
                tasks.Add(new EntityProcessor<BackendDecorationDto>(EntityProcessorDelegates.BackendDecorationCharacteristics)
                    .Process(_natives, document.BackendNatives.Decorations));
                tasks.Add(new EntityProcessor<BackendBuffDto>(EntityProcessorDelegates.BackendBuffCharacteristics)
                    .Process(_natives, document.BackendNatives.Buffs));
                tasks.Add(new EntityProcessor<BackendRoleModelDto>(EntityProcessorDelegates.BackendRoleModelCharacteristics)
                    .Process(_natives, document.BackendNatives.RoleModels));
                tasks.Add(new EntityProcessor<BackendSkillDto>(EntityProcessorDelegates.BackendSkillCharacteristics)
                    .Process(_natives, document.BackendNatives.Skills));
                tasks.Add(new EntityProcessor<BackendSpecEffectDto>(EntityProcessorDelegates.BackendSpecEffectCharacteristics)
                    .Process(_natives, document.BackendNatives.SpecEffects));
                tasks.Add(new EntityProcessor<BackendTileDto>(EntityProcessorDelegates.BackendTileCharacteristics)
                    .Process(_natives, document.BackendNatives.Tiles));
            }
            return tasks;
        }
    }
}
