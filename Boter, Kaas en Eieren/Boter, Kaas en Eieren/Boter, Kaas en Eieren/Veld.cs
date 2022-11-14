using System.Collections.Generic;

namespace Boter__Kaas_en_Eieren
{

    public class Veld
    {
        public List<string> Velden { get; private set; } = new List<string>();

        public void VeldGenereren()
        {
            for (int i = 0; i < 10; i++)
            {
                Velden.Add(i.ToString());
            }
        }

        public void ClearVeld()
        {
            Velden.Clear();
            VeldGenereren();
        }
    }  
}
