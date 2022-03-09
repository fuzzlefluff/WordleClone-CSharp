using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace WordleClone
{
    public class WordleClone
    {
        int GuessCount = 5;
        int wordIndex;
        static private List<string> WordListFull;
        private string selectedWord;
        MainForm formref;
       
        public WordleClone(MainForm form, bool easyWordList) 
        {
            formref = form;
            WordListFull = loadWordList();
            if (easyWordList) { WordListFull = loadWordListEasy(); }
            wordIndex = new Random().Next(0, WordListFull.Count);
            selectedWord = WordListFull[wordIndex];
            selectedWord = selectedWord.ToUpper();
            Console.WriteLine("Selected Word: " + selectedWord);
        }
       public WordleClone(MainForm form, int wordIndexInp, bool easyWordList) 
        {
            formref = form;
            WordListFull = loadWordList();
            if (easyWordList) { WordListFull = loadWordListEasy(); }
            wordIndex = wordIndexInp;
            selectedWord = WordListFull[wordIndex];
            selectedWord = selectedWord.ToUpper();
            Console.WriteLine("Selected Word: " + selectedWord);
        }

        //unused from when we ran things through the console.
        public void runGame()
        {
            Console.Write("Please try and guess the 5 letter long word: ");
            string input = takeInput(WordListFull);
            if (input == selectedWord)
            {
                Console.WriteLine();
                Console.WriteLine("----------------------");
                Console.WriteLine("You Win!");
                Console.WriteLine("----------------------");
                Console.WriteLine();
                Environment.Exit(0);
            }
            else if (input != selectedWord && GuessCount == 1)
            {
                Console.WriteLine();
                Console.WriteLine("----------------------");
                Console.WriteLine("You Loose!");
                Console.WriteLine("----------------------");
                Console.WriteLine();
                Environment.Exit(0);
            }
            else
            {
                foreach (var v in returnMatchingChars(selectedWord, input))
                {
                    Console.WriteLine("-The Matching Letters-");
                    Console.WriteLine("Index: " + v.Item1.ToString() + " Letter: " + v.Item2);
                    Console.WriteLine("----------------------");
                }
                if (GuessCount > 0)
                {
                    Console.WriteLine();
                    Console.Write(GuessCount - 1 + " Attempts Left");
                }
                GuessCount--;

            }

            Console.WriteLine();
        }

        public string giveCheatString() 
        {
            return selectedWord;
        }
        public void setGuessCount(int i) 
        {
            GuessCount = i;
        }
        public int giveGuessCount() 
        {
            return GuessCount;
        }

        public string testString(string input) 
        {
            string r = "error";
            if(input == selectedWord) { r = "You Win!"; return r; }
            else if(GuessCount > 0)
            { 
                GuessCount--; r = "Thats not it! Attempts Left:" + (GuessCount+1); return r;

            }
            if(GuessCount == 0) { r = "You loose! Sorry!"; }
            return r;
        }

        public bool isWord(string i, bool isEasy) 
        {
            bool r = true;
            i = formatString(i);
            if (isEasy) { i = i.ToLower(); }
            if (!WordListFull.Contains(i)) { r = false; }

            return r;
        }

        string takeInput(List<string> WordListFull)
        {
            string input = "";
            Boolean acceptableInp = false;
            while (!acceptableInp)
            {
                input = Console.ReadLine();
                input = "";
                input = formatString(input);
                if (input.Length != 5) { Console.Write("Please enter a word only 5 letters long: "); }
                else if (!WordListFull.Contains(input)) { Console.Write("Not a word on list! Try again: "); }
                else { acceptableInp = true; }
            }
            Console.WriteLine("You wrote: " + input);
            return input;
        }

        //Returns a list with the index and chars that match, and index of -1 means that the character is in the wrong place
        public static List<Tuple<int, char>> returnMatchingChars(string selected, string guess)
        {
            List<Tuple<int, char>> r = new();
            StringBuilder sb = new StringBuilder(selected);

            for (int i = 0; i < guess.Count(); i++)
            {
                if (selected[i] == guess[i])
                {
                    r.Add(new Tuple<int, char>(i, guess[i]));
                    sb[i] = '*';
                    selected = sb.ToString();
                }
                else if (selected.Contains(guess[i]))
                {
                    r.Add(new Tuple<int, char>(-1, guess[i]));
                    sb[selected.IndexOf(guess[i])] = '*';
                    selected = sb.ToString();
                }
            }
            return r;
        }

        //load the list we created with our createList() function
        static List<string> loadWordList()
        {
            List<string> WordList = new List<string>();
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            var resourceName = "WordleClone.masterwordlist.csv";
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (var reader = new StreamReader(stream))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    WordList.Add(line);
                }
            }
            Console.WriteLine("Loaded " + WordList.Count + " Words from master list!");
            return WordList;
        }
        static public List<string> loadWordListEasy()
        {
            List<string> WordList = new List<string>();
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            var resourceName = "WordleClone.easywordlist.csv";
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (var reader = new StreamReader(stream))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    WordList.Add(line);
                }
            }
            Console.WriteLine("Loaded " + WordList.Count + " Words from master list!");
            return WordList;
        }

        //cut out the white space and symbols, and get only the letters
        public static string formatString(string input)
        {
            input = input.ToUpper();
            input = String.Concat(input.Where(c => Char.IsLetter(c)));
            return input;
        }

        //update our word list and save it
        public static void updateWordList() 
        {
            WordListFull = loadWordList();
            List<string> WordListUpdated = new List<String>();
            List<string> WordListIter = WordListFull.Distinct().ToList();
            foreach (string w in WordListIter) 
            {
              if(isAlphaOnly(w)) 
                {
                    WordListUpdated.Add(w);
                }
            }
            WordListFull = WordListUpdated;

            TextWriter tw = new StreamWriter("masterwordlist.csv");
            foreach(String s in WordListFull) 
            {
                tw.WriteLine(s);
            }
            tw.Close();
        }
        public static bool isAlphaOnly(string i) 
        {
            bool r = false;
            Regex regx = new Regex(@"^[a-zA-z]+$");
            if (regx.IsMatch(i))
            {
                r = true;
            }
            return r;
        }

    }
}
