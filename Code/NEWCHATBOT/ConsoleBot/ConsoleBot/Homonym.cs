using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace ConsoleBot
{
    class Homonym
    {
        public string[] homonyms{get; set;}
        public string word { get; set; }

        public Homonym() { }

        public string chooseRandom()
        {
            string selectedWord = "";
            Random random = new Random();
            
            while(selectedWord.Equals(""))
            {
                int numberOfPossibles = random.Next(homonyms.Length);

                if(word.Equals(homonyms[numberOfPossibles]))
                {

                }

                else
                {
                    selectedWord = homonyms[numberOfPossibles];
                }
            }

            return selectedWord;
        }

        public List<string> selectedHomonyms()
        {
            List<string> selectedHomonyms = homonyms.ToList<string>();

            foreach (string homonym in selectedHomonyms)
            {
                findPossiblePun(selectedHomonyms, homonym, 0,0);
            }

            return selectedHomonyms;
        }

        public void findPossiblePun(List<string> selectedHomonyms, string currentHomonym, int currentLeft, int currentRight)
        {
            char[] vowels = new char[] { 'a', 'e', 'i', 'o', 'u'};
            char[] lettersOfCurrentHomonym = currentHomonym.ToCharArray();
            bool notDone = true;
            int rightMostVowel = 0;

            for (int i = 0; i < lettersOfCurrentHomonym.Length; i++)
            {
                if (isVowel(lettersOfCurrentHomonym[i]))
                {
                    rightMostVowel = i;
                }
            }

            while (notDone)
            {
                if (isVowel(lettersOfCurrentHomonym[currentLeft]))
                {
                    for (int i = 0; i < vowels.Length; i++)
                    {
                        string manupulatedHomonymCurrentLeft = currentHomonym;
                        char[] lettersOfManupulatedHomonymCurrentLeft = manupulatedHomonymCurrentLeft.ToCharArray();
                        lettersOfManupulatedHomonymCurrentLeft[currentLeft] = vowels[i];

                        for (int a = currentLeft+1; a < vowels.Length; a++)
                        {
                            if (isVowel(lettersOfManupulatedHomonymCurrentLeft[a]))
                            {
                                currentRight = a;
                                if (currentRight == rightMostVowel)
                                {
                                    string manupulatedHomonymCurrentRight = lettersOfManupulatedHomonymCurrentLeft.ToString();
                                    char[] lettersOfManupulatedHomonymCurrentRight = manupulatedHomonymCurrentRight.ToCharArray();
                                    lettersOfManupulatedHomonymCurrentRight[currentRight] = vowels[a];

                                    if(isChangeable(lettersOfManupulatedHomonymCurrentRight.ToString()))
                                    {
                                        selectedHomonyms.Add(lettersOfManupulatedHomonymCurrentRight.ToString());
                                    }
                                }

                                else
                                {
                                    findPossiblePun(selectedHomonyms,currentHomonym,currentRight,0);
                                }
                            }
                        }
                    }
                }

                else
                {
                    currentLeft++;
                    if((currentLeft+1) == currentHomonym.Length)
                    {
                        notDone = false;
                    }
                }
            }
        }

        public bool isVowel(char currentLetter)
        {
            bool vowel = false;
            char[] vowels = new char[] { 'a', 'e', 'i', 'o', 'u', 'A', 'E', 'I', 'O', 'U' };

            for (int i = 0; i < vowels.Length; i++)
            {
                if (currentLetter.Equals(vowels[i]))
                {
                    vowel = true;
                }
            }

            return vowel;
        }

        public bool isChangeable(string currentManipulatedHomonym)
        {
            bool isChange;
            using (WebClient client = new WebClient())
            {
                string line = client.DownloadString("http://api.wordnik.com/v4/word.json/" + currentManipulatedHomonym + "/definitions?limit=200&includeRelated=true&useCanonical=false&includeTags=false&api_key=a2a73e7b926c924fad7001ca3111acd55af2ffabf50eb4ae5");
                if (!line.Equals("[]"))
                {
                    isChange = false;
                }

                else
                {
                    isChange = true;
                }
            }

            return isChange;
        }
     }
}
