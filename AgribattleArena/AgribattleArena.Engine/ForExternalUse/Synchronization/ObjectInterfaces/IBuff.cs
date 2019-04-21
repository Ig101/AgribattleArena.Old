using System;
using System.Collections.Generic;
using System.Text;

namespace AgribattleArena.Engine.ForExternalUse.Synchronization.ObjectInterfaces
{
    public interface IBuff
    {
        int Id { get; }
        string NativeId { get; }
        string SecretNativeId { get; }
        float Mod { get; }
        float? Duration { get; }
    }
}
