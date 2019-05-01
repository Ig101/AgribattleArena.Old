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

        void ResponseToConsole(Response response, string name, string category)
        {
            switch(response)
            {
                case Response.Success:
                    Console.WriteLine("Object {1} {0} changed.", name, category);
                    break;
                case Response.NoChanges:
                    Console.WriteLine("Object {1} {0} not changed.", name, category);
                    break;
                case Response.Error:
                    Console.WriteLine("Object {1} {0} not changed due to error.", name, category);
                    break;
            }
        }

        public async Task Process(ChangingDocumentDto document)
        {
            foreach(var storeActor in document.StoreActors)
            {
                Response result;
                switch(storeActor.DocumentAction)
                {
                    case Models.Action.Create:
                        result = await _store.AddNewActor(storeActor);
                        break;
                    default:
                        result = Response.NoChanges;
                        break;
                }
                ResponseToConsole(result, storeActor.Name, "StoreActor");
            }
        }
    }
}
