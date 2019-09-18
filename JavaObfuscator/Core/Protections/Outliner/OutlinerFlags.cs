using System;

namespace JavaObfuscator.Core.Protections.Outliner
{
    [Flags]
    public enum OutlinerFlags
    {
        Strings,
        Ints,
        Floats,
        Doubles
    }
}
