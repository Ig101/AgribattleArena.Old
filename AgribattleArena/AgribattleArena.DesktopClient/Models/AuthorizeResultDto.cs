using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgribattleArena.DesktopClient.Models
{
    public class AuthorizeResultDto: ExternalResultDto
    {
        public string Cookie { get; set; }
    }
}
