using AgribattleArena.DesktopClient.ExternalModels.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgribattleArena.DesktopClient.Models
{
    public class GetProfileResultDto: ExternalResultDto
    {
        public ProfileDto Profile { get; set; }
    }
}
