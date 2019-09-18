using System;

namespace JavaObfuscator.Core.Logger
{
    public class ConsoleLogger : ILoggerBase
    {
        public override Action<string> General
        {
            get
            {
                return msg =>
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write("[:] ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(msg);
                };
            }
        }

        public override Action<string> Failed
        {
            get
            {
                return msg =>
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write("[-] ");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(msg);
                };
            }
        }

        public override Action<string> Successed
        {
            get
            {
                return msg =>
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.Write("[+] ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(msg);
                };
            }
        }

        public override Action<string> Info
        {
            get
            {
                return msg =>
                {
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.Write("[!] ");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine(msg);
                };
            }
        }

        public override Action<string> Warning
        {
            get
            {
                return msg =>
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write("[~] ");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(msg);
                };
            }
        }
    }
}
