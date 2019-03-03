using AgribattleArenaBackendServer.Engine.ActorModel;
using AgribattleArenaBackendServer.Engine.Natives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Engine
{
    public class ActiveDecoration : TileObject
    {
        float mod;

        public new DecorationNative Native { get { return (DecorationNative)base.Native; } }
        public float Mod { get { return mod; } set { mod = value; } }

        public ActiveDecoration(Scene parent,  Tile tempTile, float z, int maxHealth, TagSynergy[] armor, DecorationNative native, float?mod) 
            : base(parent, tempTile, z, new DamageModel(maxHealth, armor), native)
        {
            this.mod = mod ?? native.DefaultMod;
        }

        public override void Update(float time)
        {
            this.InitiativePosition -= time;
        }

        public void Cast()
        {
            if (Native.Action != null)
            {
                Native.Action(Parent, this, mod, 1);
                Parent.ReturnActionImplementation(Action.Decoration, Parent.Actors.FindIndex(x => x == this));
            }
        }

        public override void EndTurn()
        {
            this.InitiativePosition += 1;
            Parent.InitiativeScale.EndTurn();
        }

        public override void StartTurn()
        {
            Cast();
            EndTurn();
        }
    }
}
