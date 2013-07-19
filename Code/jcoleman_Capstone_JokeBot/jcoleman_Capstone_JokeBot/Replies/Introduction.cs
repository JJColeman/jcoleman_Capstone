using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace jcoleman_Capstone_JokeBot.Replies
{
    class Introduction
    {
        public string Hi(string userInput)
        {
            string hi = "";
            if(userInput.Equals("hi") || 
                userInput.Equals("hello") || 
                userInput.Equals("Hi") || 
                userInput.Equals("Hello"))
            {
                hi = "Hi, how are you doing";
            }

            else if (userInput.Equals("Sup") ||
                userInput.Equals("sup") ||
                userInput.Equals("what sup"))
            {
                hi = "YO YO YO";
            }

            return hi;
        }

        public string Feeling(string userInput)
        {
            return "";
        }
    }
}
