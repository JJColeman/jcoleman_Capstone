using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using jcoleman_Capstone_JokeBot.Replies;

namespace jcoleman_Capstone_JokeBot
{
    class Responses
    {
        public Responses() { }

        public string Knocks(string userInput)
        {
            return "";
        }

        public string Intros(string userInput)
        {
            string reply = "";
            Introduction intro = new Introduction();

            reply = intro.Hi(userInput);

            return reply;
        }

        public string Questions(string userInput)
        {
            return "";
        }
    }
}
