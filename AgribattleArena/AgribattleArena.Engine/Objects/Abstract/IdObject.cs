using System;
using System.Collections.Generic;
using System.Text;

namespace AgribattleArena.Engine.Objects.Abstract
{
    public abstract class IdObject
    {
        readonly int id;
        readonly ISceneParentRef parent;

        public ISceneParentRef Parent { get { return parent; } }
        public int Id { get { return id; } }

        public IdObject (ISceneParentRef parent)
        {
            this.parent = parent;
            this.id = parent.GetNextId();
        }
    }
}
