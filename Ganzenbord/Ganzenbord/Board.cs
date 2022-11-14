using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ganzenbord
{
    class Board
    {
        public Dictionary<int, string> fields { get; private set; }
        public List<int> DoubleThrowFields { get; private set; }
        public SpecialFields specialfields { get; private set; }
        public Board(Dice dice)
        
        {
            specialfields = new SpecialFields(dice);
            fields = new Dictionary<int, string>();
            DoubleThrowFields = new List<int>() { 5, 9, 14, 18, 23, 27, 32, 36, 41, 45, 50, 54, 59 };
        }

       public void GenerateBoard()
        {
            for (int i = 0; i <= 63; i++)
            { 
                if (DoubleThrowFields.Contains(i))
                {
                    fields.Add(i, "dubbeleworp");
                }

                if (fields.ContainsKey(i))
                {

                }
                else
                {
                    switch (i)
                    {
                        default:
                            fields.Add(i, null);
                            break;
                        case 6:
                            fields.Add(i, "brug");
                            break;
                        case 19:
                            fields.Add(i, "herberg");
                            break;
                        case 31:
                            fields.Add(i, "put");
                            break;
                        case 42:
                            fields.Add(i, "doolhof");
                            break;
                        case 52:
                            fields.Add(i, "gevangenis");
                            break;
                        case 58:
                            fields.Add(i, "dood");
                            break;
                        case 63:
                            fields.Add(i, "einde");
                            break;
                    }
                }
            }
        }
    }
}
