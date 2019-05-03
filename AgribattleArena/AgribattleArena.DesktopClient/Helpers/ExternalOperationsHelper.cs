using AgribattleArena.BackendServer.Models.Authorization;
using AgribattleArena.BackendServer.Models.Profile;
using Ignitus;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AgribattleArena.DesktopClient.Helpers
{
    class ExternalOperationsHelper
    {
        static string backendAddress = "https://localhost:444";
        static JsonSerializer serializer = new JsonSerializer();

        public static ProfileDto GetProfile(Game1Shell game)
        {
            HttpResponseMessage result;
            string errorCode;
            try
            {
                result = game.Client.GetAsync(backendAddress + "/api/profile/actors").Result;
            }
            catch (AggregateException e)
            {
                StringBuilder builder = new StringBuilder();
                Exception tempException = e;
                while (tempException != null)
                {
                    builder.Append(tempException.Message + '\n');
                    tempException = tempException.InnerException;
                }
                errorCode = builder.Remove(builder.Length - 1, 1).ToString();
                LabelElement errorDescr = (LabelElement)(((Mode)game.Modes["authorize_error"]).Elements[4]);
                errorDescr.Text = errorCode;
                game.GoToMode("authorize_error");
                return null;
            }
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<ProfileDto>(result.Content.ReadAsStringAsync().Result);
            }
            else
            {
                errorCode = result.Content.ReadAsStringAsync().Result;
                LabelElement errorDescr = (LabelElement)(((Mode)game.Modes["authorize_error"]).Elements[4]);
                errorDescr.Text = errorCode;
                game.GoToMode("authorize_error");
                return null;
            }
        }

        public static bool Authorize(Game1Shell game, string login, string password, out string errorCode)
        {
            AuthorizeDto auth = new AuthorizeDto()
            {
                Login = login,
                Password = password
            };
            var content = new StringContent(JsonConvert.SerializeObject(auth), Encoding.UTF8, "application/json");
            HttpResponseMessage result;
            try
            {
                result = game.Client.PostAsync(backendAddress + "/api/auth/login", content).Result;
            }
            catch (AggregateException e)
            {
                StringBuilder builder = new StringBuilder();
                Exception tempException = e;
                while (tempException != null)
                {
                    builder.Append(tempException.Message + '\n');
                    tempException = tempException.InnerException;
                }
                errorCode = builder.Remove(builder.Length - 1, 1).ToString();
                return false;
            }
            if (result.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                var cookie = result.Headers.FirstOrDefault(x => x.Key == "Set-Cookie").Value;
                game.LoginCookie = String.Concat(cookie);
                game.Client.DefaultRequestHeaders.Add("Cookie", cookie);
            }
            if (game.LoginCookie != null)
            {
                game.SaveProfile(login, password);
                errorCode = null;
                return true;
            }
            errorCode = "Unauthorized";
            return false;
        }

        public static bool Register(Game1Shell game, string login, string email, string password, out string errorCode)
        {
            RegisterDto register = new RegisterDto()
            {
                Login = login,
                Password = password,
                Email = email
            };
            var content = new StringContent(JsonConvert.SerializeObject(register), Encoding.UTF8, "application/json");
            HttpResponseMessage result;
            try
            {
                result = game.Client.PostAsync(backendAddress + "/api/auth/register", content).Result;
            }
            catch (AggregateException e)
            {
                StringBuilder builder = new StringBuilder();
                Exception tempException = e;
                while (tempException != null)
                {
                    builder.Append(tempException.Message + '\n');
                    tempException = tempException.InnerException;
                }
                errorCode = builder.Remove(builder.Length - 1, 1).ToString();
                return false;
            }
            if (result.StatusCode == System.Net.HttpStatusCode.Created)
            {
                errorCode = null;
                return true;
            }
            errorCode = result.Content.ReadAsStringAsync().Result;
            return false;
        }
    }
}
