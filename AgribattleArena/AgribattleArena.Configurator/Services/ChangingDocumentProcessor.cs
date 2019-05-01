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

        public async Task Process(ChangingDocumentDto document)
        {
            foreach(var storeActor in document.StoreActors)
            {
                bool result;
                switch(storeActor.DocumentAction)
                {
                    case Models.Action.Create:
                        result = await _store.AddNewActor(storeActor);
                        break;
                    default:
                        result = false;
                        break;
                }
                if(result)
                {
                    Console.WriteLine("{0} processed.", storeActor.Name);
                }
                else
                {
                    Console.WriteLine("{0} not processed due to error.", storeActor.Name);
                }
            }
        }
    }
}
