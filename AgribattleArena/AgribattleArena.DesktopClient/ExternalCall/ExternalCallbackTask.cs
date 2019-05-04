using AgribattleArena.DesktopClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgribattleArena.DesktopClient.ExternalCall
{
    public delegate void ProcessCallback(Game1Shell game, ExternalResultDto result);

    public class ExternalCallbackTask
    {
        public ProcessCallback CallbackMethod { get; }
        public ExternalResultDto Result { get; }

        public ExternalCallbackTask(ProcessCallback callbackMethod, ExternalResultDto result)
        {
            CallbackMethod = callbackMethod;
            Result = result;
        }
    }
}
