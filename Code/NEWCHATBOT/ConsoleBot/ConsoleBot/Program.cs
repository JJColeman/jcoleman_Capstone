using System;
using AIMLbot;
using System.Net;
using ConsoleBot.DictionaryService;
using System.Web;

namespace ConsoleBot
{
    class Program
    {
        static void Main(string[] args)
        {
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
                    PunEngine punengine = new PunEngine(input);
                    punengine.setUpSynonm(input);
                    Homonyms homonyms = new Homonyms();
                    Homonym homonym = homonyms.findWordInList(input);

                    Console.WriteLine("Homonyms for : " + homonym.word);
                    for (int i = 0; i < homonym.homonyms.Length; i++)
                    {
                        if (!homonym.homonyms[i].Equals(input))
                        {
                            Console.WriteLine(homonym.homonyms[i]);
                        }
                    }

                    Console.WriteLine("Definition is: ");
                    using(WebClient client = new WebClient())
                    {
                        string line = client.DownloadString("http://api.wordnik.com/v4/word.json/"+input+"/definitions?limit=200&includeRelated=true&useCanonical=false&includeTags=false&api_key=a2a73e7b926c924fad7001ca3111acd55af2ffabf50eb4ae5");
                        string[] lines1 = System.Text.RegularExpressions.Regex.Split(line,"\"text\":\"");
                        string[] lines2 = System.Text.RegularExpressions.Regex.Split(lines1[1],"\",\"sequence\"[\\W\\w]+");
                        Console.WriteLine(lines2[0]);
                    }

                    

                    //Request r = new Request(input, myUser, myBot);
                    //Result res = myBot.Chat(r);
                    //Console.WriteLine("Bot: " + res.Output);
                }
            }
        }
    }
}
