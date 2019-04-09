using System;
using System.Threading;

namespace SteamCli
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            new Thread(() => Steam.Run()).Start();
            while ( !Steam.IsReady )
            {
                Thread.Sleep(100);
            }
            ConsoleManager.Run();
        }
    }
}