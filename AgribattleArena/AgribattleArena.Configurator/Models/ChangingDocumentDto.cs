using System;
using System.Collections.Generic;
using System.Text;

namespace AgribattleArena.Configurator.Models
{
    class ChangingDocumentDto
    {
        public IEnumerable<StoreActorDto> StoreActors { get; set; }
        public IEnumerable<RevelationLevelDto> RevelationLevels { get; set; }
    }
}
