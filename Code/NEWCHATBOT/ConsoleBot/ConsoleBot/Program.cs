using System;
using AIMLbot;
using System.Net;

namespace ConsoleBot
{
    class Program
    {
        static void Main(string[] args)
        {

            PunEngine punEngine = new PunEngine("hello");
            Bot myBot = new Bot();
            myBot.loadSettings();
            User myUser = new User("consoleUser", myBot);
            myBot.isAcceptingUserInput = false;
            myBot.loadAIMLFromFiles();
            myBot.isAcceptingUserInput = true;
            while (true)
            {
                Console.Write("You: ");
                string input = Console.ReadLine();
                if (input.ToLower() == "quit")
                {
                    break;
                }
                else
                {
                    Request r = new Request(input, myUser, myBot);
                    Result res = myBot.Chat(r);
                    Console.WriteLine("Bot: " + res.Output);
                }
            }
        }
    }
}
