using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ConsoleBot
{
    class Homonyms
    {
        public List<string[]> setsOfHomonyms { get; set; }

        public Homonyms()
        {
            setsOfHomonyms = new List<string[]>();
            setUpHomoyms();
        }

        public void setUpHomoyms()
        {
            using (StreamReader sr = new StreamReader(@"C:\Users\jcoleman\Desktop\Homonyms.txt"))
            {
                while (sr.Peek() >= 0)
                {
                    string readLine = sr.ReadLine();
                    string[] currentHomonyms = readLine.Split(',');
                    setsOfHomonyms.Add(currentHomonyms);
                }
            }           
        }

        public Homonym findWordInList(string word)
        {
            Homonym newHomonym = new Homonym();
            foreach (string[] set in setsOfHomonyms)
            {
                for (int i = 0; i < set.Length; i++)
                {
                    if (set[i].Equals(word))
                    {
                        newHomonym.word = word;
                        newHomonym.homonyms = set;
                    }
                }
            }

            if (newHomonym.word == null)
            {
                Console.WriteLine("Can't find homonyms");
            }

            return newHomonym;
        }
    }
}
