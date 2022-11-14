using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ganzenbord
{
    class Dice
    {
        public int ThrowCount { get; private set; }
        public int Throw { get; private set; }
        public int PipCount { get; private set; }
        public int XD6(int X)
        {
            for (int i = 1; i <= X; i++)
            {
            Random rnd = new Random();
            Throw = rnd.Next(1, 7);
                ThrowCount = ThrowCount + Throw;
                PipCount = ThrowCount;
            }
            return ThrowCount;
        }

        public int XD2(int X)
        {
            for (int i = 1; i <= X; i++)
            {
                Random rnd = new Random();
                Throw = rnd.Next(1, 3);
                ThrowCount = ThrowCount + Throw;
            }
            return ThrowCount;
        }

        public int XD3(int X)
        {
            for (int i = 1; i <= X; i++)
            {
                Random rnd = new Random();
                Throw = rnd.Next(1, 4);
                ThrowCount = ThrowCount + Throw;
            }
            return ThrowCount;
        }

        public int XD4(int X)
        {
            for (int i = 1; i <= X; i++)
            {
                Random rnd = new Random();
                Throw = rnd.Next(1, 5);
                ThrowCount = ThrowCount + Throw;
            }
            return ThrowCount;
        }

        public int XD8(int X)
        {
            for (int i = 1; i <= X; i++)
            {
                Random rnd = new Random();
                Throw = rnd.Next(1, 8);
                ThrowCount = ThrowCount + Throw;
            }
            return ThrowCount;
        }

        public int XD10(int X)
        {
            for (int i = 1; i <= X; i++)
            {
                Random rnd = new Random();
                Throw = rnd.Next(1, 11);
                ThrowCount = ThrowCount + Throw;
            }
            return ThrowCount;
        }

        public int XD12(int X)
        {
            for (int i = 1; i <= X; i++)
            {
                Random rnd = new Random();
                Throw = rnd.Next(1, 13);
                ThrowCount = ThrowCount + Throw;
            }
            return ThrowCount;
        }

        public int XD16(int X)
        {
            for (int i = 1; i <= X; i++)
            {
                Random rnd = new Random();
                Throw = rnd.Next(1, 17);
                ThrowCount = ThrowCount + Throw;
            }
            return ThrowCount;
        }

        public int XD20(int X)
        {
            for (int i = 1; i <= X; i++)
            {
                Random rnd = new Random();
                Throw = rnd.Next(1, 21);
                ThrowCount = ThrowCount + Throw;
            }
            return ThrowCount;
        }

        public  int XD30(int X)
        {
            for (int i = 1; i <= X; i++)
            {
                Random rnd = new Random();
                Throw = rnd.Next(1, 31);
                ThrowCount = ThrowCount + Throw;
            }
            return ThrowCount;
        }

        public int XD100(int X)
        {
            for (int i = 1; i <= X; i++)
            {
                Random rnd = new Random();
                Throw = rnd.Next(1, 101);
                ThrowCount = ThrowCount + Throw;
            }
            return ThrowCount;
        }

        public void ResetThrowCount()
        {
            ThrowCount = 0;
            PipCount = 0;
        }

        public void ChangeThrowCount(int number)
        {
            ThrowCount = ThrowCount + number;
        }

        
    }
}
