using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArena.DesktopClient.ExternalModels.Profile
{
    public class ProfileUpdateDto
    {
        public string OldPassword { get; set; }
        public string Password { get; set; }
        public string EmailToken { get; set; }
        public string Email { get; set; }
    }
}
