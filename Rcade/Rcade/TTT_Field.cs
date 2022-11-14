using System.Collections.Generic;

namespace Rcade
{
    class TTT_Field
    {
        public List<string> box { get; private set; } = new List<string>();

        public void GenerateField()
        {
            for (int i = 0; i < 10; i++)
            {
                box.Add(i.ToString());
            }
        }

        public void ClearField()
        {
            box.Clear();
            GenerateField();
        }
    }
}