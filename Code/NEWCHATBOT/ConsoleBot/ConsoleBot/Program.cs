using System;
using AIMLbot;
using System.Net;

namespace ConsoleBot
{
    class Program
    {
        static void Main(string[] args)
        {

            using (WebClient client = new WebClient())
            {
                string htmlCode = client.DownloadString("http://words.bighugelabs.com/api/2/69523ee24f4ae7f7b0d01314944579de/fish/");
                Console.WriteLine(htmlCode);
            }

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
