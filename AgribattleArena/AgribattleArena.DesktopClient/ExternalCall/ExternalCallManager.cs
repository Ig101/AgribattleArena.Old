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
        #region ClassInfo
        HttpClient client;
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
            thread.IsBackground = true;
            thread.Start();
            client = new HttpClient();
        }

        public void InsertTask(ProcessTask method, ExternalTaskDto inObject, ProcessCallback callbackMethod)
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
                    if (task.CallbackMethod != null && callbackResult!=null)
                    {
                        var callbackTask = new ExternalCallbackTask(task.CallbackMethod, callbackResult);
                        callbackQueue.Enqueue(callbackTask);
                    }
                }
            }
        }

        public void SetAuthorizeCookie(string cookie)
        {
            client.DefaultRequestHeaders.Add("Cookie", cookie);
        }
        #endregion

        public ExternalResultDto Authorize (ExternalTaskDto inObject)
        {
            AuthorizeTaskDto authObject = (AuthorizeTaskDto)inObject;
            AuthorizeDto auth = new AuthorizeDto()
            {
                Login = authObject.Login,
                Password = authObject.Password
            };
            var content = new StringContent(JsonConvert.SerializeObject(auth), Encoding.UTF8, "application/json");
            HttpResponseMessage callResult=null;
            try
            {
                callResult = client.PostAsync(callAddress + "/api/auth/login", content).Result;
            }
            catch
            {
                return new ExternalResultDto()
                {
                    Version = inObject.Version,
                    Error = "external_server_error"
                };
            }
            if (callResult.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                var cookie = callResult.Headers.FirstOrDefault(x => x.Key == "Set-Cookie").Value;
                return new AuthorizeResultDto()
                {
                    Cookie = String.Concat(cookie),
                    Version = inObject.Version
                };
            }
            return new ExternalResultDto()
            {
                Version = inObject.Version,
                Error = "wrong_login"
            };
        }



        public ExternalResultDto Register(ExternalTaskDto inObject)
        {
            RegisterTaskDto regObject = (RegisterTaskDto)inObject;
            if (regObject.Password != regObject.ConfirmPassword)
            {
                return new ExternalResultDto()
                {
                    Error = "password_not_confirmed",
                    Version = inObject.Version
                };
            }
            RegisterDto register = new RegisterDto()
            {
                Login = regObject.Login,
                Password = regObject.Password,
                Email = regObject.Email
            };
            var content = new StringContent(JsonConvert.SerializeObject(register), Encoding.UTF8, "application/json");
            HttpResponseMessage callResult;
            try
            {
                callResult = client.PostAsync(callAddress + "/api/auth/register", content).Result;
            }
            catch
            {
                return new ExternalResultDto()
                {
                    Version = inObject.Version,
                    Error = "external_server_error"
                };
            }
            if (callResult.StatusCode == System.Net.HttpStatusCode.Created)
            {
                return new RegisterResultDto()
                {
                    Version = inObject.Version,
                    Login = regObject.Login,
                    Password = regObject.Password
                };
            }
            string jsonError = callResult.Content.ReadAsStringAsync().Result;
            var dict = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(jsonError);
            var error = dict.Values.First().First();
            string errorToSend = null;
            if(error.Contains("already taken"))
            {
                if(error.Contains("User name"))
                {
                    errorToSend = "login_already_taken";
                }
                if(error.Contains("Email"))
                {
                    errorToSend = "email_already_taken";
                }
            }
            if(error.Contains("letters or digits") && error.Contains("User name"))
            {
                errorToSend = "incorrect login";
            }
            if (error.Contains("Passwords must have at least one digit")) errorToSend = "password_one_digit";
            if (error.Contains("Passwords must have at least one uppercase")) errorToSend = "password_one_uppercase";
            if (error.Contains("Passwords must have at least one lowercase")) errorToSend = "password_one_lowercase";
            if (errorToSend == null)
            {
                switch (error)
                {
                    case "The Email field is not a valid e-mail address.":
                        errorToSend = "not_valid_email";
                        break;
                    case "Passwords must be at least 6 characters.":
                        errorToSend = "password_too_short";
                        break;
                    case "Passwords must have at least one non alphanumeric character.":
                        errorToSend = "password_one_non_alphanumeric";
                        break;
                    default:
                        errorToSend = "wrong_input";
                        break;
                }
            }
            return new ExternalResultDto(){
                Error = errorToSend,
                Version = inObject.Version
            };
        }

        public ExternalResultDto GetProfileInfo (ExternalTaskDto inObject)
        {
            return new ExternalResultDto()
            {
                Version = inObject.Version,
                Error = "Not implemented"
            };
        }
    }
}
