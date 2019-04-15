using AgribattleArena.Engine.Helpers;
using AgribattleArena.Engine.Natives;
using AgribattleArena.Engine.Objects.Abstract;
using AgribattleArena.Engine.Objects.Immaterial;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgribattleArena.Engine.Objects
{
    public class ActiveDecoration: TileObject
    {
        float mod;

        public new ActiveDecorationNative Native { get { return (ActiveDecorationNative)base.Native; } }
        public float Mod { get { return mod; } set { mod = value; } }

        public ActiveDecoration(ISceneParentRef parent, IPlayerParentRef owner, ITileParentRef tempTile, float? z, int? maxHealth, TagSynergy[] armor, ActiveDecorationNative native, float? mod)
            : base(parent, owner, tempTile, z ?? native.DefaultZ, new DamageModel(maxHealth ?? native.DefaultHealth, armor ?? native.DefaultArmor), native)
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
            Native.Action?.Invoke(Parent, this);
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
