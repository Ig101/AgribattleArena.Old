using AgribattleArena.Configurator.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AgribattleArena.Configurator.Services
{
    class EntityProcessor<T> where T : ActionObject
    {
        public delegate (string name, string category) GetObjectCharacteristics(T entity);

        GetObjectCharacteristics getCharacteristics;

        public EntityProcessor(GetObjectCharacteristics getCharacteristics)
        {
            this.getCharacteristics = getCharacteristics;
        }

        void ResponseToConsole(Response response, string name, string category, Models.Action action)
        {
            switch (response)
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

        public async Task Process(IRepository<T> repository, IEnumerable<T> array)
        {
            if (array != null)
            {
                foreach (var entity in array)
                {
                    Response result;
                    switch (entity.DocumentAction)
                    {
                        case Models.Action.Create:
                            result = await repository.Add(entity);
                            break;
                        case Models.Action.Update:
                            result = await repository.Update(entity);
                            break;
                        case Models.Action.Delete:
                            result = await repository.Delete(entity);
                            break;
                        default:
                            result = Response.NoChanges;
                            break;
                    }
                    var characteristics = getCharacteristics(entity);
                    ResponseToConsole(result, characteristics.name, characteristics.category, entity.DocumentAction);
                }
            }
        }
    }
}
