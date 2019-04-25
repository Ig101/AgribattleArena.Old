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

        /// <summary>
        /// For Attacker tags
        /// </summary>
        /// <param name="selfTag"></param>
        /// <param name="targetTag"></param>
        /// <param name="mod"></param>
        public TagSynergy(string selfTag, string targetTag, float mod)
        {
            this.selfTag = string.Intern(selfTag);
            this.targetTag = string.Intern(targetTag);
            this.mod = mod;
        }

        /// <summary>
        /// For Defence tags
        /// </summary>
        /// <param name="targetTag"></param>
        /// <param name="mod"></param>
        public TagSynergy(string targetTag, float mod)
        {
            this.selfTag = null;
            this.targetTag = string.Intern(targetTag);
            this.mod = mod;
        }
    }
}
