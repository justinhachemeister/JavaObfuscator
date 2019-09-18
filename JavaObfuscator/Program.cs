using JavaObfuscator.Core;
using System;

namespace JavaObfuscator
{
    class Program
    {
        static void Main(string[] args)
        {
            Context context = new Context(args[0]);
            context.Engine.Executor();
            context.Save();
            Console.ReadKey();
        }
    }
}
