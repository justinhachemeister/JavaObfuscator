using System;

namespace JavaObfuscator.Core.Logger
{
    public abstract class ILoggerBase : ILogger
    {
        public abstract Action<string> General { get; }
        public abstract Action<string> Failed { get; }
        public abstract Action<string> Successed { get; }
        public abstract Action<string> Info { get; }
        public abstract Action<string> Warning { get; }
    }
}
