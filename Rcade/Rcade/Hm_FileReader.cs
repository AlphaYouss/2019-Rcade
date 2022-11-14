using System.Collections.Generic;
using System.IO;

namespace Rcade
{
    class Hm_FileReader
    {
        public List<string> words { get; private set; } = new List<string>();

        public Hm_FileReader()
        {
            LoadTextFile();
        }

        public void LoadTextFile()
        {
            string Location = @"Assets\Files\words.txt";

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