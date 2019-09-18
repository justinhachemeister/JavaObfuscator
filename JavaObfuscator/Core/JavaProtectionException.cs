using System;

namespace JavaObfuscator.Core
{
    public class JavaProtectionException : Exception
    {
        public JavaProtectionException()
            : base ()
        { }
        public JavaProtectionException(string message)
            : base(message)
        { }
    }
}
