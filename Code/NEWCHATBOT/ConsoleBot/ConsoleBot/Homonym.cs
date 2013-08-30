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
            bool notDone1 = true;
            bool notDone2 = true;
            int rightMostVowel = 0;

            for (int i = 0; i < lettersOfCurrentHomonym.Length; i++)
            {
                if (isVowel(lettersOfCurrentHomonym[i]))
                {
                    rightMostVowel = i;
                }
            }

            while (notDone1)
            {
                if (isVowel(lettersOfCurrentHomonym[currentLeft]))
                {
                    for (int i = 0; i < vowels.Length; i++)
                    {
                        string manupulatedHomonymCurrentLeft = currentHomonym;
                        char[] lettersOfManupulatedHomonymCurrentLeft = manupulatedHomonymCurrentLeft.ToCharArray();
                        lettersOfManupulatedHomonymCurrentLeft[currentLeft] = vowels[i];
                        currentRight = currentLeft + 1;

                        if (currentLeft == rightMostVowel)
                        {
                            bool abled1 = isChangeable(toChartoString(lettersOfManupulatedHomonymCurrentLeft));

                            if (abled1)
                            {
                                selectedHomonyms.Add(toChartoString(lettersOfManupulatedHomonymCurrentLeft));
                            }
                        }

                        else
                        {
                            while (notDone2)
                            {
                                if (isVowel(lettersOfManupulatedHomonymCurrentLeft[currentRight]))
                                {
                                    for (int l = 0; l < vowels.Length; l++)
                                    {
                                        string manupulatedHomonymCurrentRight = toChartoString(lettersOfManupulatedHomonymCurrentLeft);
                                        char[] lettersOfManupulatedHomonymCurrentRight = manupulatedHomonymCurrentRight.ToCharArray();
                                        lettersOfManupulatedHomonymCurrentRight[currentRight] = vowels[l];

                                        if (currentRight == rightMostVowel)
                                        {
                                            bool abled2 = isChangeable(toChartoString(lettersOfManupulatedHomonymCurrentRight));

                                            if (abled2)
                                            {
                                                selectedHomonyms.Add(toChartoString(lettersOfManupulatedHomonymCurrentRight));
                                            }
                                        }

                                        else
                                        {
                                            findPossiblePun(selectedHomonyms, currentHomonym, currentRight, 0);
                                        }
                                    }
                                }

                                else
                                {
                                    currentRight++;
                                    if ((currentRight + 1) == currentHomonym.Length)
                                    {
                                        notDone2 = false;
                                    }
                                }
                            }
                        }
                    }
                    currentLeft++;
                }

                else
                {
                    currentLeft++;
                    if((currentLeft+1) == currentHomonym.Length)
                    {
                        notDone1 = false;
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

        public string toChartoString(char[] turnIntoString)
        {
            string newString = "";

            for(int i = 0; i < turnIntoString.Length;i++)
            {
                newString += turnIntoString[i];
            }

            return newString;
        }

        public bool isChangeable(string currentManipulatedHomonym)
        {
            bool isChange;
            using (WebClient client = new WebClient())
            {
                string line = client.DownloadString("http://api.wordnik.com/v4/word.json/" + currentManipulatedHomonym + "/definitions?limit=200&includeRelated=true&useCanonical=false&includeTags=false&api_key=a2a73e7b926c924fad7001ca3111acd55af2ffabf50eb4ae5");
                if (line.Equals("[]"))
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
