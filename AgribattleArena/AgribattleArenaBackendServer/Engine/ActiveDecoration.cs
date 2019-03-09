using AgribattleArenaBackendServer.Engine.ActorModel;
using AgribattleArenaBackendServer.Engine.Helpers;
using AgribattleArenaBackendServer.Models.Natives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Engine
{
    public class ActiveDecoration : TileObject
    {
        float mod;

        public new DecorationNativeDto Native { get { return (DecorationNativeDto)base.Native; } }
        public float Mod { get { return mod; } set { mod = value; } }

        public ActiveDecoration(Scene parent, int? ownerId, Tile tempTile, float? z, int? maxHealth, TagSynergy[] armor, DecorationNativeDto native, float?mod) 
            : base(parent, ownerId, tempTile, z ?? native.DefaultZ, new DamageModel(maxHealth ?? native.DefaultHealth, armor ?? native.DefaultArmor.ToArray()), native)
        {
            this.mod = mod ?? native.DefaultMod;
            this.InitiativePosition += 1;
        }

        public override void Update(float time)
        {
            this.InitiativePosition -= time;
        }

        public void Cast()
        {
            //Native.Action?.Invoke(Parent, this, mod, 1);
            if (Native.Action != null)
            {
                Jint.Engine actionEngine = new Jint.Engine();
                actionEngine
                    .SetValue("scene", Parent)
                    .SetValue("act", this)
                    .SetValue("mod", mod)
                    .Execute(Native.Action.Script);
            }
        }

        public override void EndTurn()
        {
            this.InitiativePosition += 1;
            Parent.EndTurn();
        }

        public override void StartTurn()
        {
            Parent.DecorationCast(this);
            EndTurn();
        }
    }
}
