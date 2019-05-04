using AgribattleArena.DesktopClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgribattleArena.DesktopClient.ExternalCall
{
    public delegate ExternalResultDto ProcessTask(object inObject);

    public class ExternalTask
    {
        public ProcessTask TaskMethod { get; }
        public ProcessCallback CallbackMethod { get; }
        public object InObject { get; }

        public ExternalTask(ProcessTask taskMethod, object inObject, ProcessCallback callbackMethod)
        {
            CallbackMethod = callbackMethod;
            TaskMethod = taskMethod;
            InObject = inObject;
        }
    }
}
