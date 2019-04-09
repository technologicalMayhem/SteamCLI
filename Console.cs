using System;
using System.Collections.Generic;
using System.Linq;
using SteamKit2;

namespace SteamCli
{
    public static class ConsoleManager
    {
        static int curFriendNum;
        static string curMessage;

        public static void Run()
        {
            curMessage = "";
            curFriendNum = 0;
            Render();
            while (true)
            {
                var key = Console.ReadKey();
                if (key.Key == ConsoleKey.Enter)
                {
                    Steam.Send(Steam.friendsList[curFriendNum].steamid, curMessage);
                    curMessage = "";
                }
                else if (key.Key == ConsoleKey.Backspace)
                {
                    curMessage = curMessage.Substring(0, curMessage.Length - 1);
                }
                else if (key.Key == ConsoleKey.UpArrow)
                {
                    if (curFriendNum >= 1)
                    {
                        curFriendNum--;
                    }
                }
                else if (key.Key == ConsoleKey.DownArrow)
                {
                    if (curFriendNum < Steam.friendsList.Count - 2)
                    {
                        curFriendNum++;
                    }
                }
                else
                {
                    curMessage += key.KeyChar;
                }
                Render();
            }
        }

        public static void Render()
        {
            Console.Clear();
            var lines = Console.WindowHeight - 3;
            List<string> messages = new List<string>();
            if (true)
            {
                messages = Steam.chatMessages.Skip(Math.Max(0, Steam.chatMessages.Count() - lines)).ToList();
            }
            Console.SetCursorPosition(0,0);
            foreach (var msg in messages)
            {
                Console.Write(msg.Substring(0, Math.Min(Console.WindowWidth, msg.Length)));
                Console.SetCursorPosition(0, Console.CursorTop + 1);
            }
            Console.SetCursorPosition(0, Console.WindowHeight - 2);
            if (Steam.friendsList.Count >= 1)
            {
                var user = Steam.friendsList[curFriendNum];
                switch (user.personaState)
                {
                    case EPersonaState.Offline:
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    break;

                    case EPersonaState.Online:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;

                    case EPersonaState.Away:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;

                    case EPersonaState.Busy:
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    break;

                    default:
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    break;
                }
                Console.Write(user.username.Substring(0, Math.Min(Console.WindowWidth, user.username.Length)));
                Console.ResetColor();
            }
            else
            {
                Console.Write("No friends found :(");
            }
            Console.SetCursorPosition(0, Console.WindowHeight - 1);
            Console.Write( "> " + curMessage.Substring(Math.Max(0, curMessage.Length - Console.WindowWidth - 2)));
        }
    }
}