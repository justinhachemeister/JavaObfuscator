using System;

namespace JavaObfuscator.Core.Logger
{
    public interface ILogger
    {
        Action<string> General { get; }
        Action<string> Failed { get; }
        Action<string> Successed { get; }
        Action<string> Info { get; }
        Action<string> Warning { get; }
    }
}
