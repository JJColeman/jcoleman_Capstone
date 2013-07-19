using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using jcoleman_Capstone_JokeBot.Replies;

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
            Converter converter = new Converter();
            string[] splitUserInput = userInput.Split(' ');
            bool isSentence = converter.checkForSentence(splitUserInput);
            Responses response = new Responses();
            string whatToSay = "";

            if (isSentence)
            {
               whatToSay = response.Intros(userInput);

               if (!isEmpty(whatToSay))
               {
                   Console.WriteLine(whatToSay);
               }

               whatToSay = response.Knocks(userInput);

               if (!isEmpty(whatToSay))
               {
                   Console.WriteLine(whatToSay);
               }

               whatToSay = response.Questions(userInput);

               if (!isEmpty(whatToSay))
               {
                   Console.WriteLine("I understood what you said, just I don't know how to reply to that...weird...JAMAL FIX ME!");
                   lastBotInput = "I understood what you said, just I don't know how to reply to that...weird...JAMAL FIX ME!";
               }       
           }

           else
           {
               Console.WriteLine("I don't understand? ARE U SPEAKING ENGLISH");

               lastBotInput = "I don't understand? ARE U SPEAKING ENGLISH";
           }
        }

        public bool isEmpty(string whatToSay)
        {
            return whatToSay.Equals("");
        }
    }
}
