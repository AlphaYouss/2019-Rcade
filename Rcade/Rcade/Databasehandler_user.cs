using System;
using System.Data.SqlClient;

namespace Rcade
{
    class Databasehandler_user : Databasehandler
    {
        public Databasehandler_user(bool testTable)
        {
            isTestVersion = testTable;
        }

        public bool CheckUser(string username)
        {
            SqlCommand cmd = new SqlCommand();

            if (isTestVersion == true)
            {
                cmd = new SqlCommand("SELECT COUNT(*) FROM Test_User_gegevens WHERE Username = @Username", GetCon());
            }
            else
            {
                cmd = new SqlCommand("SELECT COUNT(*) FROM User_gegevens WHERE Username = @Username", GetCon());
            }

            cmd.Parameters.AddWithValue("Username", username);

            OpenConnectionToDB();

            bool exists = (int)cmd.ExecuteScalar() > 0;

            CloseConnectionToDB();
            return exists;
        }

        public int GetUserID(string username)
        {
            SqlCommand cmd = new SqlCommand();
            int userID = 0;

            if (isTestVersion == true)
            {
                cmd = new SqlCommand("SELECT ID FROM Test_User_gegevens WHERE Username = @Username", GetCon());
            }
            else
            {
                cmd = new SqlCommand("SELECT ID FROM User_gegevens WHERE Username = @Username", GetCon());
            }

            cmd.Parameters.AddWithValue("Username", username);

            OpenConnectionToDB();

            userID = Convert.ToInt16(cmd.ExecuteScalar());

            CloseConnectionToDB();
            return userID;
        }

        public string GetUserHash(int id)
        {
            SqlCommand cmd = new SqlCommand();
            string salt = "";

            if (isTestVersion == true)
            {
                cmd = new SqlCommand("SELECT Wachtwoord_hash FROM Test_User_gegevens WHERE ID = @ID", GetCon());
            }
            else
            {
                cmd = new SqlCommand("SELECT Wachtwoord_hash FROM User_gegevens WHERE ID = @ID", GetCon());
            }

            cmd.Parameters.AddWithValue("ID", id);

            OpenConnectionToDB();

            salt = Convert.ToString(cmd.ExecuteScalar());
            CloseConnectionToDB();

            return salt;
        }
    }
}