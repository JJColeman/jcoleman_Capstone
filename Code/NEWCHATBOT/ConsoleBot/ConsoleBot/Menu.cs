using System;
using System.Collections.Generic;
using System.Linq;
using java.io;
using opennlp.tools.sentdetect;
using opennlp.tools.tokenize;
using System.Text;
using System.IO;
using System.Net;

namespace ConsoleBot
{
    class Menu
    {
        private string FilePath { get; set; }
        private string[] FileText { get; set; }
        public Menu() { }

        public void startMenu()
        {
            System.Console.WriteLine("Welcome to the Pun Helper");
            bool usingStartMenu = true;
            while (usingStartMenu)
            {
                System.Console.WriteLine("Choose the following chooses by enter the number");
                System.Console.WriteLine("1: Load a text file.");
                System.Console.WriteLine("2: Start a new text file");
                System.Console.WriteLine("3: Exit");
                string userinput = System.Console.ReadLine();

                if (userinput.Equals("1"))
                {
                    usingStartMenu = false;
                    loadFile();
                    mainMenu();
                }

                else if (userinput.Equals("2"))
                {
                    usingStartMenu = false;
                    startNewFile();
                    mainMenu();
                }

                else if (userinput.Equals("3"))
                {
                    usingStartMenu = false;
                }

                else
                {
                    System.Console.WriteLine("Wrong input, please enter the number next to the choices");
                    System.Console.WriteLine();
                }
            }
        }

        public void mainMenu()
        {
            System.Console.WriteLine();
            System.Console.WriteLine("Choose the following choices by entering the number next to the choice");
            bool usingMainMenu = true;

            while (usingMainMenu)
            {
                System.Console.WriteLine("Displaying current text");
                FileText = System.IO.File.ReadAllLines(FilePath);

                for (int i = 0; i < FileText.Length; i++)
                {
                    System.Console.WriteLine(FileText[i]);
                }

                System.Console.WriteLine();
                System.Console.WriteLine("1: Start a new sentence");
                System.Console.WriteLine("2: Choose a sentence to anaylze");
                System.Console.WriteLine("3: Exit");
                string userinput = System.Console.ReadLine();

                if(userinput.Equals("1"))
                {
                    analysingCurrentSentenceMenu(newSentence());
                }

                else if (userinput.Equals("2"))
                {
                    analysingCurrentSentenceMenu(chooseSentenceMenu());
                }

                else if (userinput.Equals("3"))
                {
                    usingMainMenu = false;
                }
            }
        }

        public String newSentence()
        {
            System.Console.WriteLine("Enter a sentence to add to your file");
            System.Console.WriteLine("Once enter, you will be moved to the AnalyzeMenu");
            string userInputSentence = System.Console.ReadLine();

            return userInputSentence;
        }

        public String chooseSentenceMenu()
        {
            int userInputNumber = 0;
            string userChoosenSentence = "";
            System.Console.WriteLine("Choose a sentence to use from your current text");
            System.Console.WriteLine("Must be a space between each sentence");
            try
            {
                java.io.File file = new java.io.File("C:\\en-sent.bin");
                java.io.InputStream modelIn = new FileInputStream("C:\\Users\\jcoleman\\Documents\\Capstone\\jcoleman_Capstone\\Code\\NEWCHATBOT\\ConsoleBot\\ConsoleBot\\en-sent.bin");
                SentenceModel model = new SentenceModel(modelIn);
                SentenceDetectorME sentenceDetector = new SentenceDetectorME(model);
                
                string text = "";
                FileText = System.IO.File.ReadAllLines(FilePath);

                for (int i = 0; i < FileText.Length; i++)
                {
                    text += FileText[i];
                }

                string[] sentences = sentenceDetector.sentDetect(text);

                for(int s = 0;s < sentences.Length;s++)
                {
                    System.Console.WriteLine((s+1) +" : " +sentences[s]);
                }

                string userInput = System.Console.ReadLine();
                userInputNumber = int.Parse(userInput);

                userChoosenSentence = sentences[userInputNumber - 1];
                modelIn.close();
            }
            catch(Exception e)
            {
                System.Console.WriteLine(e.Message);
            }

            return userChoosenSentence;
        }

        public void analysingCurrentSentenceMenu(string currentSentence)
        {
            System.Console.WriteLine();
            System.Console.WriteLine("Enter the number for the following choices");
            System.Console.WriteLine();
            bool isChoosingAnalyze = true;

            while (isChoosingAnalyze)
            {
                System.Console.WriteLine(currentSentence);
                System.Console.WriteLine();
                System.Console.WriteLine("1: Overwrite the sentence");
                System.Console.WriteLine("2: Give suggested pun sentences for current currentsentence");
                System.Console.WriteLine("3: Give definition/possible puns");
                System.Console.WriteLine("4: Save the current sentence to text then go back");
                System.Console.WriteLine("5: Go Back");

                string userInput = System.Console.ReadLine();

                if (userInput.Equals("1"))
                {
                    currentSentence = overWriteSentence();
                }

                else if (userInput.Equals("2"))
                {
                    currentSentence = grabPossiblePunSentences(currentSentence);
                }

                else if (userInput.Equals("3"))
                {
                   giveDefinitionAndHomonym(currentSentence);
                }

                else if (userInput.Equals("4"))
                {
                    saveFile(currentSentence);
                    isChoosingAnalyze = false;
                }

                else if (userInput.Equals("5"))
                {
                    isChoosingAnalyze = false;
                }

                else
                {
                    System.Console.WriteLine("Wrong number input, please enter the number next to the choice you choose");
                }
            }

            System.Console.WriteLine();
        }

        public void giveDefinitionAndHomonym(string currentSentence)
        {
            try
            {
                java.io.InputStream modelIn = new java.io.FileInputStream(@"C:\en-token.bin");
                TokenizerModel model = new TokenizerModel(modelIn);

                Tokenizer tokenizer = new TokenizerME(model);

                string[] words = tokenizer.tokenize(currentSentence);

                Homonyms homonyms = new Homonyms();

                for (int i = 0; i < words.Length; i++)
                {
                    System.Console.WriteLine();
                    Homonym homonym = homonyms.findWordInList(words[i]);

                    if (homonym.homonyms == null)
                    {
                        System.Console.WriteLine("No homonyms found for: " + words[i]);
                    }

                    else
                    {
                        List<string> selectedHomonyms = homonym.selectedHomonyms();

                        System.Console.WriteLine("Homonyms are: " + words[i]);
                        foreach (string selectedWord in selectedHomonyms)
                        {
                            System.Console.Write(selectedWord + ",");
                        }
                    }

                    System.Console.WriteLine();
                    System.Console.WriteLine("Definition for: " + words[i]);
                    using (WebClient client = new WebClient())
                    {
                        string line = client.DownloadString("http://api.wordnik.com/v4/word.json/" + words[i] + "/definitions?limit=200&includeRelated=true&useCanonical=false&includeTags=false&api_key=a2a73e7b926c924fad7001ca3111acd55af2ffabf50eb4ae5");
                        if (!line.Equals("[]"))
                        {
                            string[] lines1 = System.Text.RegularExpressions.Regex.Split(line, "\"text\":\"");
                            string[] lines2 = System.Text.RegularExpressions.Regex.Split(lines1[1], "\",\"sequence\"[\\W\\w]+");
                            System.Console.WriteLine(lines2[0]);
                        }
                        else
                        {
                            System.Console.WriteLine("Definition cannot be found, word is mispelled or doesn't exist within our current data");
                        }
                    }

                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
            }
        }

        public string grabPossiblePunSentences(string currentSentence)
        {
            try
            {
                java.io.InputStream modelIn = new java.io.FileInputStream(@"C:\en-token.bin");
                TokenizerModel model = new TokenizerModel(modelIn);

                Tokenizer tokenizer = new TokenizerME(model);

                string[] words = tokenizer.tokenize(currentSentence);
                List<string> possibleSentences = new List<string>();

                Homonyms homonyms = new Homonyms();

                for (int i = 0; i < words.Length; i++)
                {
                    System.Console.WriteLine();
                    Homonym homonym = homonyms.findWordInList(words[i]);

                    if (homonym.homonyms == null)
                    {
                        
                    }

                    else
                    {
                        string possibleSentence = "";
                        for (int r = 0; r < words.Length; r++)
                        {
                            if (words[i].Equals(words[r]))
                            {
                                Random random = new Random();
                                int randomNumber = random.Next(homonym.homonyms.Length);
                                possibleSentence += " " + homonym.homonyms[randomNumber];
                            }

                            else
                            {
                                possibleSentence += " " + words[r];
                            }
                        }
                        possibleSentences.Add(possibleSentence);
                    }
                }
                currentSentence = choosePossiblePunSentence(currentSentence, possibleSentences);
            }

            catch (Exception e)
            {

            }

            return currentSentence;
        }

        public string choosePossiblePunSentence(string currentSentence, List<string> possiblePunSentences)
        {
            string newPunSentence = "";
            System.Console.WriteLine("Choosing one of these following sentences will overwrite your current sentence");
            string[] possiblePunSentencesArray = possiblePunSentences.ToArray();
            bool isChoosingPossiblePunSentence = true;

            try
            {
                while (isChoosingPossiblePunSentence)
                {
                    for (int i = 0; i < possiblePunSentencesArray.Length; i++)
                    {
                        System.Console.WriteLine((i+1) + ": " + possiblePunSentencesArray[i]);
                    }

                    System.Console.WriteLine((possiblePunSentencesArray.Length + 1) + ": Go back"); 
                    string userInput = System.Console.ReadLine();
                    
                    if(int.Parse(userInput) < possiblePunSentencesArray.Length + 1)
                    {
                        newPunSentence = possiblePunSentencesArray[int.Parse(userInput) - 1];
                        isChoosingPossiblePunSentence = false;
                    }   
            
                    else if (int.Parse(userInput) == possiblePunSentencesArray.Length + 1)
                    {
                        isChoosingPossiblePunSentence = false;
                        newPunSentence = currentSentence;
                    }

                    else
                    {
                        System.Console.WriteLine("Wrong input, please enter a number to select a choice");
                    }
                }
             }

            catch(Exception e)
            {
                System.Console.WriteLine("Wrong format, please enter a number to select a choice");
            }

            return newPunSentence;
        }

        public string overWriteSentence()
        {
            System.Console.WriteLine("Please enter the your sentence");
            string userInput = System.Console.ReadLine();

            return userInput;
        }

        public void startNewFile()
        {
            System.Console.WriteLine("The new file will be located on your desktop");
            Guid g = Guid.NewGuid();
            string randomGenerateWord = Convert.ToBase64String(g.ToByteArray());
            randomGenerateWord = randomGenerateWord.Replace("=","");
            randomGenerateWord = randomGenerateWord.Replace("+","");
            randomGenerateWord = randomGenerateWord.Replace("/","");

            System.IO.StreamWriter writer = new System.IO.StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\" + randomGenerateWord +".txt");
            writer.Flush();
            writer.Close();
            FilePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\" + randomGenerateWord +".txt";

            FileText = System.IO.File.ReadAllLines(FilePath);
        }

        public void loadFile()
        {
            bool hasLoadFile = true;
            while (hasLoadFile)
            {
                System.Console.WriteLine("Enter the path of text file");

                string userinput = System.Console.ReadLine();

                bool fileExits = System.IO.File.Exists(@"" + userinput);

                if (fileExits)
                {
                    FilePath = userinput;
                    hasLoadFile = false;
                }

                else
                {
                    System.Console.WriteLine("Wrong input or Wrong path");
                    System.Console.WriteLine("Please follow the insturctions that are given");
                }
            }
        }

        public void saveFile(string currentSentence)
        {
            string text = "";
            FileText = System.IO.File.ReadAllLines(FilePath);

            for (int i = 0; i < FileText.Length; i++)
            {
                text += FileText[i];
            }

            text += "\r\n" + " " +currentSentence;

            System.IO.File.WriteAllText(FilePath, text);
        }
    }
}
