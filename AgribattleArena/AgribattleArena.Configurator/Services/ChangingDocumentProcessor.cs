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

        public async Task Process(ChangingDocumentDto document)
        {
            await new EntityProcessor<StoreActorDto>(EntityProcessorDelegates.StoreActorCharacteristics)
                .Process(_store, document.StoreActors);
            await new EntityProcessor<RevelationLevelDto>(EntityProcessorDelegates.RevelationLevelCharacteristics)
                .Process(_profiles, document.RevelationLevels);
            if(document.BackendNatives != null)
            {
                await new EntityProcessor<BackendActorDto>(EntityProcessorDelegates.BackendActorCharacteristics)
                    .Process(_natives, document.BackendNatives.Actors);
                await new EntityProcessor<BackendDecorationDto>(EntityProcessorDelegates.BackendDecorationCharacteristics)
                    .Process(_natives, document.BackendNatives.Decorations);
                await new EntityProcessor<BackendBuffDto>(EntityProcessorDelegates.BackendBuffCharacteristics)
                    .Process(_natives, document.BackendNatives.Buffs);
                await new EntityProcessor<BackendRoleModelDto>(EntityProcessorDelegates.BackendRoleModelCharacteristics)
                    .Process(_natives, document.BackendNatives.RoleModels);
                await new EntityProcessor<BackendSkillDto>(EntityProcessorDelegates.BackendSkillCharacteristics)
                    .Process(_natives, document.BackendNatives.Skills);
                await new EntityProcessor<BackendSpecEffectDto>(EntityProcessorDelegates.BackendSpecEffectCharacteristics)
                    .Process(_natives, document.BackendNatives.SpecEffects);
                await new EntityProcessor<BackendTileDto>(EntityProcessorDelegates.BackendTileCharacteristics)
                    .Process(_natives, document.BackendNatives.Tiles);
            }
        }
    }
}
