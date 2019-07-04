using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AgribattleArena.Configurator.Services
{
    interface IRepository<T>
    {
        Task<Response> Add(T entity);
        Task<Response> Update(T entity);
        Task<Response> Delete(T entity);
    }
}
