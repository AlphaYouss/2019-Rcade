namespace Rcade
{
    class TTT
    {
        public TTT_Field field { get; private set; }

        public TTT(TTT_Field field)
        {
            this.field = field;
        }

        public bool CheckWin()
        {
            if (
                // Horizontal.  
                   field.box[1] == field.box[2] && field.box[2] == field.box[3]
                || field.box[4] == field.box[5] && field.box[5] == field.box[6]
                || field.box[7] == field.box[8] && field.box[8] == field.box[9]

                // Vertical.
                || field.box[1] == field.box[4] && field.box[4] == field.box[7]
                || field.box[2] == field.box[5] && field.box[5] == field.box[8]
                || field.box[3] == field.box[6] && field.box[6] == field.box[9]

                // Diagonal.
                || field.box[1] == field.box[5] && field.box[5] == field.box[9]
                || field.box[3] == field.box[5] && field.box[5] == field.box[7]
                )
            {
                return true;
            }
            return false;
        }

        public int CheckField()
        {
            int remainingBoxes = 0;

            for (int i = 1; i < field.box.Count; i++)
            {
                if (field.box[i] != "O" && field.box[i] != "X")
                {
                    remainingBoxes++;
                }
            }
            return remainingBoxes;
        }
    }
}