using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AgribattleArena.DesktopClient.ExternalCall
{
    public sealed class CustomTaskScheduler : TaskScheduler, IDisposable
    {
        public CustomTaskScheduler()
        {

        }
        protected override IEnumerable<Task> GetScheduledTasks()
        {
            return null;
        }
        protected override void QueueTask(Task task)
        {
            if (task != null)
                TryExecuteTask(task);
        }
        protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
        {
            return false;
        }
        private void Dispose(bool disposing)
        {
            if (!disposing) return;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
