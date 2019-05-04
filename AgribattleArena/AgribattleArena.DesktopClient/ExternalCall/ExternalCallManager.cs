using AgribattleArena.DesktopClient.ExternalModels.Authorization;
using AgribattleArena.DesktopClient.Models;
using Ignitus;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AgribattleArena.DesktopClient.ExternalCall
{

    public class ExternalCallManager
    {
        HttpClient client = new HttpClient();
        JsonSerializer serializer = new JsonSerializer();
        Thread thread;

        string callAddress;

        Queue<ExternalCallbackTask> callbackQueue;
        Queue<ExternalTask> taskQueue;

        public ExternalCallManager (Queue<ExternalCallbackTask> callbackQueue, string callAddress)
        {
            taskQueue = new Queue<ExternalTask>();
            this.callbackQueue = callbackQueue;
            this.callAddress = callAddress;
            thread = new Thread(new ThreadStart(TaskComputer));
            thread.Start();
        }

        public void InsertTask(ProcessTask method, object inObject, ProcessCallback callbackMethod)
        {
            taskQueue.Enqueue(new ExternalTask(method, inObject, callbackMethod));
        }

        void TaskComputer()
        {
            while (true)
            {
                if (taskQueue.Count > 0)
                {
                    var task = taskQueue.Dequeue();
                    var callbackResult = task.TaskMethod(task.InObject);
                    if (task.CallbackMethod != null)
                    {
                        var callbackTask = new ExternalCallbackTask(task.CallbackMethod, callbackResult);
                        callbackQueue.Enqueue(callbackTask);
                    }
                }
            }
        }

        public ExternalResultDto Authorize (object inObject)
        {
            Thread.Sleep(2000);
            return new ExternalResultDto()
            {
                Error = "Not implemented"
            };
            //return new ExternalCallbackTask(;// new AuthorizeResultDto() { Error = "Not Implemented" };
            /* ((LabelElement)(((Mode)game.Modes["authorize_status"]).Elements[3])).Text = "";
             ((LabelElement)(((Mode)game.Modes["register_status"]).Elements[3])).Text = "";
             AuthorizeDto auth = new AuthorizeDto()
             {
                 Login = login,
                 Password = password
             };
             var content = new StringContent(JsonConvert.SerializeObject(auth), Encoding.UTF8, "application/json");
             HttpResponseMessage callResult;
             try
             {
                 callResult = await client.PostAsync(callAddress + "/api/auth/login", content);
             }
             catch (AggregateException e)
             {
             }
             var result = new AuthorizeResultDto() { Error = "Not Implemented" };
             ((LabelElement)(((Mode)game.Modes["authorize_status"]).Elements[3])).Text = result.Error;
             ((LabelElement)(((Mode)game.Modes["register_status"]).Elements[3])).Text = result.Error;
             return null;*/
        }



        public ExternalResultDto Register(object inObject)
        {
            return new ExternalResultDto()
            {
                Error = "Not implemented"
            };
        }

        public ExternalResultDto GetProfileInfo (object inObject)
        {
            return new ExternalResultDto()
            {
                Error = "Not implemented"
            };
        }
    }
}
