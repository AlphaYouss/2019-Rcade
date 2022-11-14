using System;
using System.Data.SqlClient;

namespace Rcade
{
    class Databasehandler_ttt : Databasehandler
    {
        public Databasehandler_ttt(bool testTable)
        {
            isTestVersion = testTable;
        }

        public void GetUser(int id)
        {
            SqlCommand cmd = new SqlCommand();

            if (isTestVersion == true)
            {
                cmd = new SqlCommand("SELECT * FROM Test_BKE WHERE user_ID = @ID", GetCon());
            }
            else
            {
                cmd = new SqlCommand("SELECT * FROM BKE WHERE user_ID = @ID", GetCon());
            }

            cmd.Parameters.AddWithValue("ID", id);

            OpenConnectionToDB();

            SqlDataAdapter adapt = new SqlDataAdapter(cmd);

            adapt.Fill(table);
            CloseConnectionToDB();
        }

        public void SetUser(int id, int won, int lost, int draw, DateTime lastPlayed)
        {
            SqlCommand cmd = new SqlCommand();

            if (isTestVersion == true)
            {
                cmd = new SqlCommand("UPDATE Test_BKE SET gewonnen_potjes = @Won, verloren_potjes = @Lost, gelijkspel_potjes = @Draw, laatst_gespeeld = @LastPlayed Where user_ID = @ID", GetCon());
            }
            else
            {
                cmd = new SqlCommand("UPDATE BKE SET gewonnen_potjes = @Won, verloren_potjes = @Lost, gelijkspel_potjes = @Draw, laatst_gespeeld = @LastPlayed Where user_ID = @ID", GetCon());
            }

            cmd.Parameters.AddWithValue("ID", id);
            cmd.Parameters.AddWithValue("Won", won);
            cmd.Parameters.AddWithValue("Lost", lost);
            cmd.Parameters.AddWithValue("Draw", draw);
            cmd.Parameters.AddWithValue("LastPlayed", lastPlayed);

            OpenConnectionToDB();
            cmd.ExecuteNonQuery();
            CloseConnectionToDB();
        }
    }
}