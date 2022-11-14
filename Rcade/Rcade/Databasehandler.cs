using System;
using System.Data;
using System.Data.SqlClient;

namespace Rcade
{
    class Databasehandler
    {
        public SqlConnectionStringBuilder builder { get; private set; }
        public SqlConnection con { get; private set; }
        public DataTable table { get; private set; }
        public bool isTestVersion { get; set; }

        public Databasehandler()
        {
            builder = new SqlConnectionStringBuilder();

            builder.DataSource = "rcade.database.windows.net";
            builder.UserID = "rcade";
            builder.Password = "Kanker!25";
            builder.InitialCatalog = "Rcade";

            con = new SqlConnection(builder.ConnectionString);
            table = new DataTable();
        }

        public bool TestConnection()
        {
            bool open = false;

            try
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
            }
            catch (Exception)
            {
                return false;
            }

            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    open = true;
                }

                con.Close();
            }

            if (!open)
            {
                return false;
            }
            return open;
        }

        public void OpenConnectionToDB()
        {
            con.Open();
        }

        public void CloseConnectionToDB()
        {
            con.Close();
        }

        public SqlConnection GetCon()
        {
            return con;
        }
    }
}