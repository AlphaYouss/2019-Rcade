using System;
using System.Collections.Generic;

namespace Rcade
{
    class Hm
    {
        public Hm_Player player { get; private set; } = new Hm_Player();
        public Hm_FileReader fileReader { get; private set; } = new Hm_FileReader();
        public char[] correctLetters { get; private set; }
        public char[] displayedLetters { get; private set; }
        public List<char> guessedLetters { get; private set; } = new List<char>();
        public Random randomNumber { get; private set; } = new Random();
        public string word { get; private set; }
        public string result { get; private set; } = "";

        public void Start()
        {
            GenerateWord();
            ClearDisplayLetters();
        }

        public void GenerateWord()
        {
            int maxValue = fileReader.words.Count + 1;
            int number = randomNumber.Next(0, maxValue);
            string word = fileReader.words[number];

            SplitWord(word);
        }

        private void SplitWord(string word)
        {
            correctLetters = word.ToCharArray();
            displayedLetters = new char[correctLetters.Length];
        }

        private void ClearDisplayLetters()
        {
            for (int i = 0; i < displayedLetters.Length; i++)
            {
                displayedLetters[i] = '-';
            }
        }

        public void Clear()
        {
            Array.Clear(correctLetters, 0, correctLetters.Length);

            ClearDisplayLetters();
            guessedLetters.Clear();

            word = "";
            result = "";
        }

        public void CheckLetter(char letter)
        {
            bool containsLetter = false;

            for (int i = 0; i < correctLetters.Length; i++)
            {
                if (letter == correctLetters[i])
                {
                    displayedLetters[i] = letter;
                    containsLetter = true;
                }
            }

            if (!containsLetter && !guessedLetters.Contains(letter))
            {
                guessedLetters.Add(letter);
                player.AddTurn();
            }
        }

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
                    result = "You won!";
                    return true;
                }
            }
            return false;
        }

        public bool EndGameCheck()
        {
            if (player.turn == 11)
            {
                result = "You lost!";
                return true;
            }
            return false;
        }
    }
}