using System;

namespace JavaObfuscator.Core.Protections.ProxyCalls
{
    [Flags]
    public enum CallFlags
    {
        InvokeStatic,
        InvokeVirtual
    }
}
