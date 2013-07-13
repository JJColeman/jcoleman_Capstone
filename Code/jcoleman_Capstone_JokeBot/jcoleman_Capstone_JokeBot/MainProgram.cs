using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

            while (dontExit)
            {
                string userInput = Console.ReadLine();
                Console.WriteLine(userInput);
            }
        }
    }
}
