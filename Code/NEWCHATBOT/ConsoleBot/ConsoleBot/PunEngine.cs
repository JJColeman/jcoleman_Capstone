using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using opennlp.tools.parser;
using opennlp.tools.cmdline.parser;
using java.io;
using System.Reflection;
using System.IO;
using ConsoleBot.DictionaryService;
using System.Xml;
using System.Net;

namespace ConsoleBot
{
    class PunEngine
    {
        private string sentence { get; set; }
        private string word {get;set;}
        public string synonms{get;set;}
        private List<string> grabbedSynonms { get; set; }
        private Parse[] parse { get; set;}
        private Parser parser {get; set;}
        private ParserModel parserModel { get; set; }

        public PunEngine(string sentence)
        {
            this.sentence = sentence;
         //   setUpParser();
        }

        public void setUpParser()
        {
            string modelPath = "C:\\Users\\jcoleman\\Documents\\Capstone\\jcoleman_Capstone\\Code\\NEWCHATBOT\\ConsoleBot\\ConsoleBot\\en-parser-chunking.bin";
            java.io.FileInputStream modelInpstream = new java.io.FileInputStream(modelPath);
            parserModel = new ParserModel(modelInpstream);
            parser = ParserFactory.create(parserModel);
            parse = ParserTool.parseLine(sentence, parser, 1);
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
                System.Console.WriteLine(this.synonms);
            }
        }

        public void seperateSynonms(string synonms)
        {
            //NEEDS TO BE FIX
            System.Console.WriteLine(synonms);
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

            foreach (string l in grabbedSynonms)
            {
                System.Console.WriteLine(l);
            }
        }
    }
}
