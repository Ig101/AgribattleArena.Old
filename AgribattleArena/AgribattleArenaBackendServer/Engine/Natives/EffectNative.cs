﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Engine.Natives
{
    public class EffectNative : TaggingNative
    {
        SceneObjectAction action;
        float defaultMod;
        float defaultDuration;
        float defaultZ;

        public float DefaultZ { get { return defaultZ; } }
        public float DefaultDuration { get { return defaultDuration; } }
        public float DefaultMod { get { return defaultMod; } }
        public SceneObjectAction Action { get { return action; } }

        public EffectNative(string id, string[] tags, SceneObjectAction action, float defaultDuration, float defaultMod, float defaultZ)
            : base(id, tags)
        {
            this.defaultZ = defaultZ;
            this.defaultDuration = defaultDuration;
            this.defaultMod = defaultMod;
            this.action = action;
        }
    }
}
