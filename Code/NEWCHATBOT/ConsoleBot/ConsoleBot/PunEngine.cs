using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace ConsoleBot
{
    class PunEngine
    {
        private string word {get;set;}
        private string synonms{get;set;}
        private List<string> grabbedSynonms { get; set; }

        public PunEngine(string word)
        {
            this.word = word;
            grabbedSynonms = new List<string>();
            setUpSynonm(this.word);
            Console.WriteLine(synonms);
            seperateSynonms(this.synonms);
        }

        public void setUpSynonm(string word)
        {
            using (WebClient client = new WebClient())
            {
                string foundedSynonms = client.DownloadString("http://words.bighugelabs.com/api/2/69523ee24f4ae7f7b0d01314944579de/" + word + "/");  
                this.synonms = foundedSynonms;
            }
        }

        public void seperateSynonms(string synonms)
        {
            string[] seperatedString = synonms.Split('|');
            
            int wordCount = 1;
            for (int i = 0; i < seperatedString.Length; i++)
            {
                if (wordCount == 0)
                {
                    wordCount = 1;

                    if (seperatedString[i].Equals("syn"))
                    {
                        int l = seperatedString[i + 1].IndexOf("\n");
                        grabbedSynonms.Add(seperatedString[i + 1].Substring(0,seperatedString[i + 1].IndexOf("\n")));
                    }
                }

                else
                {
                    wordCount--;
                }
            }
        }
    }
}
