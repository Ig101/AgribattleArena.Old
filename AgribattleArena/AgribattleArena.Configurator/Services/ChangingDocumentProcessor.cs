using AgribattleArena.Configurator.Models;
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
            return new List<Task>
            {
                new EntityProcessor<StoreActorDto>(EntityProcessorDelegates.StoreActorCharacteristics)
                    .Process(_store, document.StoreActors),
                new EntityProcessor<RevelationLevelDto>(EntityProcessorDelegates.RevelationLevelCharacteristics)
                    .Process(_profiles, document.RevelationLevels)
            };
        }
    }
}
