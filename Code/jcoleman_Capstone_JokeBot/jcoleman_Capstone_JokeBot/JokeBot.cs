using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace jcoleman_Capstone_JokeBot
{
    class JokeBot
    {
        public JokeBot() { }

        public string lastUserInput { get; set; }
        public string lastBotInput { get; set; }
        public int ConfuseCount { get; set; }

        public void Reply(string userInput)
        {
           bool isSentence = Converter(userInput);
           Responses response = new Responses();

           if (isSentence)
           {
               string whatToSay = "";
               whatToSay = response.Intros;

               if (!isEmpty(whatToSay))
               {
                   Console.WriteLine(whatToSay);
               }

               whatToSay = response.Knocks;

               if (!isEmpty(whatToSay))
               {
                   Console.WriteLine(whatToSay);
               }

               whatToSay = response.Questions;

               
           }

           else
           {
               Console.WriteLine(BotReply);

               lastBotInput = BotReply;
           }
        }

        public bool isEmpty(string whatToSay)
        {
            return whatToSay.Equals("");
        }
    }
}
