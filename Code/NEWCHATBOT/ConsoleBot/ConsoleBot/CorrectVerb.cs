using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleBot
{
    class CorrectVerb
    {
        public int countedLetters { get; set; }
        public string word { get; set; }

        public CorrectVerb(String word)
        {
            this.word = word;
            countedLetters = 0;
        }
    }
}
