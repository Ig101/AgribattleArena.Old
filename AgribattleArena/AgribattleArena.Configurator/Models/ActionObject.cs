using System;
using System.Collections.Generic;
using System.Text;

namespace AgribattleArena.Configurator.Models
{
    public enum Action { Create, Update, Delete };

    class ActionObject
    {
        public Action DocumentAction { get; set; }
    }
}
