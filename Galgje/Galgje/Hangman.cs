using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Galgje
{
    class Hangman
    {
        public Player player = new Player();
        public FileReader fileReader = new FileReader();
        Random randomNumber = new Random();

        public char[] correctLetters { get; private set; }
        public char[] displayedLetters { get; private set; }
       
        public List<char> guessedLetters { get; private set; } = new List<char>();
        
        public string word { get; private set; }
        public string result { get; private set; } = "";
        


       


        //Start
        public void Start()
        {
            GenerateWord();
            ClearDisplayLetters();
        }





        // Hier haal je het woord op uit de database
        public void GenerateWord()
        {
            int MaxValue = fileReader.words.Count + 1;
            int number = randomNumber.Next(0, MaxValue);
            string word = fileReader.words[number];
            SplitWord(word);
        }


        //Split het woord in een char[]
        private void SplitWord(string word)
        {
            correctLetters = word.ToCharArray();
            displayedLetters = new char[correctLetters.Length];
        }






        // Veranderd alle char[] characters naar streepjes
        private void ClearDisplayLetters()
        {
            for (int i = 0; i < displayedLetters.Length; i++)
            {
                displayedLetters[i] = '-';
            }
        }


        // Haalt alles leeg 
        public void Clear()
        {
            Array.Clear(correctLetters, 0, correctLetters.Length);
            ClearDisplayLetters();
            guessedLetters.Clear();
            word = "";
            result = "";
        }






        // Controleer of de letter goed is
        public void CheckLetter(char letter)
        {
            bool ContainsLetter = false;

            for (int i = 0; i < correctLetters.Length; i++)
            {
                if (letter == correctLetters[i])
                {
                    displayedLetters[i] = letter;
                    ContainsLetter = true;
                }
            }

            if (!ContainsLetter && !guessedLetters.Contains(letter))
            {
                guessedLetters.Add(letter);
                player.AddTurn();
            }
        }






        // Controleert of alle letters goed zijn
        public bool WinCheck()
        {
            int x = 0;
            for (int i = 0; i < correctLetters.Length; i++)
            {
                if (correctLetters[i] == displayedLetters[i])
                {
                    x++;
                }

                if (x == correctLetters.Length)
                {
                    result = "Je hebt gewonnen!";
                    return true;
                }
            }
            return false;
        }






        // Controleert of je hangman dood is
        public bool EndGameCheck()
        {
            if (player.turn == 11)
            {
                result = "Je hebt verloren!";
                return true;
            }
            return false;
        }
    }
}
