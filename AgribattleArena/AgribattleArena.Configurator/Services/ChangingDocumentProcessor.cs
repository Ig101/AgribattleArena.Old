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

        void ResponseToConsole(Response response, string name, string category, Models.Action action)
        {
            switch(response)
            {
                case Response.Success:
                    Console.WriteLine("{2}. Object {1} {0} changed.", name, category, action);
                    break;
                case Response.NoChanges:
                    Console.WriteLine("{2}. Object {1} {0} not changed.", name, category, action);
                    break;
                case Response.Error:
                    Console.WriteLine("{2}. Object {1} {0} not changed due to an error.", name, category, action);
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
                        result = await _store.AddActor(storeActor);
                        break;
                    case Models.Action.Update:
                        result = await _store.ChangeActor(storeActor);
                        break;
                    case Models.Action.Delete:
                        result = await _store.RemoveActor(storeActor.Name);
                        break;
                    default:
                        result = Response.NoChanges;
                        break;
                }
                ResponseToConsole(result, storeActor.Name, "StoreActor", storeActor.DocumentAction);
            }
        }
    }
}
