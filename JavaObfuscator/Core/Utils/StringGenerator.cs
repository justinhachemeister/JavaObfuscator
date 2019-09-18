using System;
using System.Linq;

namespace JavaObfuscator.Core.Utils
{
    public class StringGenerator
    {
        public string UsedString { get; set; }
        public int Seed { get; set; }

        private Random Random;

        public StringGenerator()
            : this ("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz", Guid.NewGuid().GetHashCode())
        {
        }
        public StringGenerator(string UsedString)
            : this (UsedString, Guid.NewGuid().GetHashCode())
        {
        }

        public StringGenerator(string UsedString, int Seed)
        {
            this.UsedString = UsedString;
            this.Seed = Seed;

            this.Random = new Random(this.Seed);
        }


        public string Generate()
        {
            return Generate(10);
        }

        public string Generate(int length)
        {
            return new string(Enumerable.Repeat(UsedString, length).Select(s => s[Random.Next(s.Length)]).ToArray());
        }
    }
}
