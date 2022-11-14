using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Galgje
{
    class FileReader
    {
        public List<string> words { get; private set; } = new List<string>();

        




        // Constructor
        public FileReader()
        {
            LoadTextFile();
        }


       
        // Hierin lees ik het textbestand en zet ik het om in een list
        public void LoadTextFile()
        {
            string Location = @"Assets\HangmanWords.txt";

            using (StreamReader streamReader = new StreamReader(Location))
            {
                while (streamReader.Peek() > 0)
                {
                    string line = streamReader.ReadLine();
                    words.Add(line);
                }
            }
        }
    }
}
