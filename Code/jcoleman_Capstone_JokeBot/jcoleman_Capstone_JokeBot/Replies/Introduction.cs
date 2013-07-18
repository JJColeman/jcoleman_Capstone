using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace jcoleman_Capstone_JokeBot.Trees
{
    class Introduction
    {
        public string Hi(string userInput)
        {
            if(userInput.Equals("hi") || 
                userInput.Equals("hello") || 
                userInput.Equals("Hi") || 
                userInput.Equals("Hello"))
            {
                return "Hi, how are you doing";
            }

            else if(userInput.Equals("Sup") || 
                userInput.Equals("sup") ||
                userInput.Equals("what sup"))
            {
                return "YO YO YO" ;
            }
        }

        public string Feeling(string userInput)
        {
            
        }
    }
}
