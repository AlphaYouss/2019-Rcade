using System;

namespace Rcade
{
    class GB_Dice
    {
        public int throwCount { get; private set; }
        public int throwAmount { get; private set; }
        public int pipCount { get; private set; }
        public int XD6(int X)
        {
            for (int i = 1; i <= X; i++)
            {
                Random rnd = new Random();

                throwAmount = rnd.Next(1, 7);
                throwCount = throwCount + throwAmount;
                pipCount = throwCount;
            }
            return throwCount;
        }

        public int XD2(int X)
        {
            for (int i = 1; i <= X; i++)
            {
                Random rnd = new Random();

                throwAmount = rnd.Next(1, 3);
                throwCount = throwCount + throwAmount;
            }
            return throwCount;
        }

        public int XD3(int X)
        {
            for (int i = 1; i <= X; i++)
            {
                Random rnd = new Random();

                throwAmount = rnd.Next(1, 4);
                throwCount = throwCount + throwAmount;
            }
            return throwCount;
        }

        public int XD4(int X)
        {
            for (int i = 1; i <= X; i++)
            {
                Random rnd = new Random();

                throwAmount = rnd.Next(1, 5);
                throwCount = throwCount + throwAmount;
            }
            return throwCount;
        }

        public int XD8(int X)
        {
            for (int i = 1; i <= X; i++)
            {
                Random rnd = new Random();

                throwAmount = rnd.Next(1, 8);
                throwCount = throwCount + throwAmount;
            }
            return throwCount;
        }

        public int XD10(int X)
        {
            for (int i = 1; i <= X; i++)
            {
                Random rnd = new Random();

                throwAmount = rnd.Next(1, 11);
                throwCount = throwCount + throwAmount;
            }
            return throwCount;
        }

        public int XD12(int X)
        {
            for (int i = 1; i <= X; i++)
            {
                Random rnd = new Random();

                throwAmount = rnd.Next(1, 13);
                throwCount = throwCount + throwAmount;
            }
            return throwCount;
        }

        public int XD16(int X)
        {
            for (int i = 1; i <= X; i++)
            {
                Random rnd = new Random();

                throwAmount = rnd.Next(1, 17);
                throwCount = throwCount + throwAmount;
            }
            return throwCount;
        }

        public int XD20(int X)
        {
            for (int i = 1; i <= X; i++)
            {
                Random rnd = new Random();

                throwAmount = rnd.Next(1, 21);
                throwCount = throwCount + throwAmount;
            }
            return throwCount;
        }

        public int XD30(int X)
        {
            for (int i = 1; i <= X; i++)
            {
                Random rnd = new Random();

                throwAmount = rnd.Next(1, 31);
                throwCount = throwCount + throwAmount;
            }
            return throwCount;
        }

        public int XD100(int X)
        {
            for (int i = 1; i <= X; i++)
            {
                Random rnd = new Random();

                throwAmount = rnd.Next(1, 101);
                throwCount = throwCount + throwAmount;
            }
            return throwCount;
        }

        public void ResetThrowCount()
        {
            throwCount = 0;
            pipCount = 0;
        }

        public void ChangeThrowCount(int number)
        {
            throwCount = throwCount + number;
        }
    }
}
