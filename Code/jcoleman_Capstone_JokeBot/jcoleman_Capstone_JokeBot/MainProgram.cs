using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenNLP.Tools.Parser;
using OpenNLP.Tools.Util;

namespace jcoleman_Capstone_JokeBot
{
    class MainProgram
    {
        static void Main(string[] args)
        {
            JokeBot bot = new JokeBot();

            Console.WriteLine("You are now talking to Jokebot");
            Console.WriteLine("If done chatting with Jokebot, you may type in 'exit");
            bool dontExit = true;

            Span span = new Span(0,10);
            Parse parse = new Parse("Hello, my name is Jamal", span, "noun", 0.0);
            Console.WriteLine(parse.Show());
            Console.ReadLine();

           // while (dontExit)
           // {
            //    string userInput = Console.ReadLine();
            //    if(userInput.Equals("exit"))
            //    {
            //        dontExit = false;
            //    }
            //    bot.Reply(userInput);
            }
        }
    }
