using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using opennlp.tools.parser;
using opennlp.tools.cmdline.parser;
using java.io;
using System.Reflection;

namespace ConsoleBot
{
    class PunEngine
    {
        private string sentence { get; set; }
        private string word {get;set;}
        private string synonms{get;set;}
        private List<string> grabbedSynonms { get; set; }
        private Parse[] parse { get; set;}
        private Parser parser {get; set;}
        private ParserModel parserModel { get; set; }

        public PunEngine(string sentence)
        {
            this.sentence = sentence;
            setUpParser();
        }

        public void setUpParser()
        {
            var asm = Assembly.GetExecutingAssembly();
            var stream = asm.GetManifestResourceStream("en-parser-chunking.bin");
            var inp = new ikvm.io.InputStreamWrapper(stream);
            InputStream modelParser = inp; 
            parserModel = new ParserModel(modelParser);
            parser = ParserFactory.create(parserModel);
        }

        public void grabPun()
        {
            parse = ParserTool.parseLine(sentence,parser,1);
            string grabbedNoun = grabNoun(parse);
            string grabbedVerb= grabVerb(parse);

            var medivalNounValues = Enum.GetValues(typeof(Medival_Noun));

            bool foundmedivalNoun = false;

            foreach(Medival_Noun mNoun in medivalNounValues)
            {
                if(grabbedNoun.Equals(mNoun))
                {
                    foundmedivalNoun = true;
                }
            }

            if (foundmedivalNoun)
            {
                System.Console.WriteLine(findCloseVerbs(grabbedVerb, sentence));
            }

            else
            {
                System.Console.WriteLine("NO PUN FOUND!");
            }            
        }

        public String findCloseVerbs(String grabbedVerb, String sentence)
        {
            string punSentence = "";
            string BestPunWord = "";
            List<CorrectVerb> correctVerbs = new List<CorrectVerb>();
            var medivalVarbValues = Enum.GetValues(typeof(Medival_Verbs));

            foreach (Medival_Verbs mVerbs in medivalVarbValues)
            {
                CorrectVerb currentVerb = new CorrectVerb(mVerbs.ToString());
                compareVerbForPun(currentVerb, grabbedVerb);

                if (currentVerb.countedLetters > 0)
                {
                    correctVerbs.Add(currentVerb);
                }
            }

            BestPunWord = findBestPunWord(correctVerbs);

            string[] words = sentence.Split(' ');
            foreach(string word in words)
            {
                if(word.Equals(grabbedVerb))
                {
                    punSentence += BestPunWord + " "; 
                }

                else
                {
                    punSentence += word + " ";
                }
            }

            return punSentence;
        }

        public String findBestPunWord(List<CorrectVerb> correctVerbs)
        {
            CorrectVerb bestPun = new CorrectVerb("");
            foreach (CorrectVerb cv in correctVerbs)
            {
                if (bestPun.word.Equals(""))
                {
                    bestPun = cv;
                }

                else
                {
                    if (cv.countedLetters > bestPun.countedLetters)
                    {
                        bestPun = cv;
                    }
                }
            }

            return bestPun.word;
        }

        public void compareVerbForPun(CorrectVerb possiblePunVerb, String grabbedVerb)
        {
            char[] grabbedVerbChar = grabbedVerb.ToCharArray();
            char[] possiblePunVerbChar = possiblePunVerb.word.ToCharArray();

            if (grabbedVerbChar.Length > possiblePunVerbChar.Length)
            {                
                for (int i = 0; i < possiblePunVerbChar.Length - 1; i++)
                {
                    if (grabbedVerbChar[i].Equals(possiblePunVerbChar[i]))
                    {
                        possiblePunVerb.countedLetters++;
                    }
                }
            }

            else
            {
                for (int i = 0; i < grabbedVerbChar.Length - 1; i++)
                {
                    if (grabbedVerbChar[i].Equals(possiblePunVerbChar[i]))
                    {
                        possiblePunVerb.countedLetters++;
                    }
                }
            }
        }

        public String grabNoun(Parse[] children)
        {
            String grabbedNoun = "";
            bool isNotFound = true;
            int nextChild = 0;

            while(isNotFound)
            {
                if(children[nextChild].getType().Equals("NP"))
                {
                    String[] NounSymbols = new String[]{"NN","NNS","NNP","NNPS"};

                    Parse[] parseGrandChildren = children[nextChild].getChildren();

                    for(int i = 0; i < parseGrandChildren.Length;i++)
                    {
                        foreach(String symbols in NounSymbols)
                        {
                            if(parseGrandChildren[i].getType().Equals(symbols))
                            {
                                grabbedNoun = parseGrandChildren[i].toString();
                                isNotFound = false;
                            }
                        }
                    }
                }

                else if((nextChild + 1) == children.Length)
                {
                    isNotFound = false;
                    for(int i = 0; i < children.Length;i++)
                    {
                        grabbedNoun = grabNoun(children[i].getChildren());
                        if(!grabbedNoun.Equals(""))
                        {
                            i = children.Length;
                        }
                    }
                }

                else
                {
                    nextChild++;
                }
            }

            return grabbedNoun;
        }

        public String grabVerb(Parse[] children)
        {
            String grabbedVerb = "";
            bool isNotFound = true;
            int nextChild = 0;

            while(isNotFound)
            {
                if(children[nextChild].getType().Equals("VP"))
                {
                    String[] VerbSymbols = new String[]{"VB","VBD","VBG","VBN", "VBP", "VBZ"};

                    Parse[] parseGrandChildren = children[nextChild].getChildren();

                    for(int i = 0; i < parseGrandChildren.Length;i++)
                    {
                        foreach(String symbols in VerbSymbols)
                        {
                            if(parseGrandChildren[i].getType().Equals(symbols))
                            {
                                grabbedVerb = parseGrandChildren[i].toString();
                                isNotFound = false;
                            }
                        }
                    }
                }

                else if((nextChild + 1)  == children.Length)
                {
                    isNotFound = false;
                    for(int i = 0; i < children.Length;i++)
                    {
                        grabbedVerb = grabVerb(children[i].getChildren());
                        if(!grabbedVerb.Equals(""))
                        {
                            i = children.Length;
                        }
                    }
                }

                else
                {
                    nextChild++;
                }
            }

            return grabbedVerb;
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
