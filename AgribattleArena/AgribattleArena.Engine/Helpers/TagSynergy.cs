using System;
using System.Collections.Generic;
using System.Text;

namespace AgribattleArena.Engine.Helpers
{
    public struct TagSynergy
    {
        string selfTag;
        string targetTag;
        float mod;

        public string SelfTag { get { return selfTag; } }
        public string TargetTag { get { return targetTag; } }
        public float Mod { get { return mod; } }

        public TagSynergy(string selfTag, string targetTag, float mod)
        {
            this.selfTag = string.Intern(selfTag);
            this.targetTag = string.Intern(targetTag);
            this.mod = mod;
        }
    }
}
