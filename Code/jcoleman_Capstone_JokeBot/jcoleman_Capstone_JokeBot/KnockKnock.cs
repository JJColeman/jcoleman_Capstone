using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using jcoleman_Capstone_JokeBot.RecognizedWords;

namespace jcoleman_Capstone_JokeBot
{
    class KnockKnock
    {
        public void KnockKnockListening(string userInput)
        {
            string Reply = "";
            if (userInput.Equals("knockknock") || userInput.Equals("KnockKnock"))
            {
                Console.WriteLine("Who's There");
                string userName = Console.ReadLine();
                Console.WriteLine(userName + " who?");
                string userPunchLine = Console.ReadLine();
                Console.WriteLine("Nice joke");
                Console.WriteLine("Want to hear my knockknock joke?");
                string userConfirm = Console.ReadLine();
                bool isConfirm = Enum.IsDefined(typeof(Confirm), userConfirm);
                if (isConfirm)
                {
                    KnockKnockTell();
                }
            }
        }

        public void KnockKnockTell()
        {
            Console.WriteLine("knock knock");
            string userWho = Console.ReadLine();
            if (userWho.Equals("whos there") || userWho.Equals("Who there"))
            {
                Random r = new Random();
                int l = r.Next(3);
                string[] replys = new string[] { "rick", "lol", "look" };
                Console.WriteLine(replys[l]);
                string userReply = Console.ReadLine();
                if (userReply.Equals("rick who") || userReply.Equals("lol who") || userReply.Equals("look who"))
                {
                    Random q = new Random();
                    int d = q.Next(3);
                    string[] replyss = new string[] { "THE BIG MAN", "daddy girl", "master mind behind of waffles" };
                    Console.WriteLine(replys[l] + " " + replyss[d]);
                }
            }
        }
    }
}
