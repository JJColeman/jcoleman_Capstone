using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleBot
{
    class Menu
    {
        private string FilePath { get; set; }
        private string[] FileText { get; set; }
        public Menu() { }

        public void startMenu()
        {
            Console.WriteLine("Welcome to the Pun Engine");
            bool usingStartMenu = true;
            while (usingStartMenu)
            {
                Console.WriteLine("Choose the following chooses by enter the number");
                Console.WriteLine("1: Load a text file.");
                Console.WriteLine("2: Start a new text file");
                Console.WriteLine("3: Exit");
                string userinput = Console.ReadLine();

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
                    Console.WriteLine("Wrong input, please enter the number next to the choices");
                    Console.WriteLine();
                }
            }
        }

        public void mainMenu()
        {
            Console.WriteLine();
            Console.WriteLine("Choose the following choices by entering the number next to the choice");
            bool usingMainMenu = true;

            while (usingMainMenu)
            {
                Console.WriteLine("Displaying current text");
                FileText = System.IO.File.ReadAllLines(FilePath);

                for (int i = 0; i < FileText.Length; i++)
                {
                    Console.WriteLine(FileText[i]);
                }
                
                Console.WriteLine();
                Console.WriteLine("1: Start a new sentence");
                Console.WriteLine("2: Choose a sentence to anaylze");
                Console.WriteLine("3: Exit");
                string userinput = Console.ReadLine();

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
            string newSentence = "";

            Console.WriteLine("Enter a sentence to add to your file");
            Console.WriteLine("Once enter, you will be moved to the AnalyzeMenu");
            string userInputSentence = Console.ReadLine();

            return "";
        }

        public String chooseSentenceMenu()
        {
            return "";
        }

        public void analysingCurrentSentenceMenu(string currentSentence)
        {
            saveFile(currentSentence);
        }

        public  

        public void startNewFile()
        {
            Console.WriteLine("The new file will be located on your desktop");
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
                Console.WriteLine("Enter the path of text file");

                string userinput = Console.ReadLine();

                bool fileExits = System.IO.File.Exists(@"" + userinput);

                if (fileExits)
                {
                    FilePath = userinput;
                    hasLoadFile = false;
                }

                else
                {
                    Console.WriteLine("Wrong input or Wrong path");
                    Console.WriteLine("Please follow the insturctions that are given");
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

            text += "\r\n" + currentSentence;

            System.IO.File.WriteAllText(FilePath, text);
        }
    }
}
