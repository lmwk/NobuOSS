using System;

namespace NobuOSS
{
    class Program
    {
        static void Main(string[] args)
        {
            var bot = new bot();
            bot.RunAsync().GetAwaiter().GetResult();
        }
    }
}
