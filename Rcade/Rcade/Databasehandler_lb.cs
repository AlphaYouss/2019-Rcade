using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Rcade
{
    class Databasehandler_lb : Databasehandler
    {
        public int choice { get; private set; } = 1;
        public List<string> usernames { get; private set; } = new List<string>();
        public string table1Name { get; private set; } = "";
        public List<string> table1 { get; private set; } = new List<string>();
        public string table2Name { get; private set; } = "";
        public List<string> table2 { get; private set; } = new List<string>();
        public string table3Name { get; private set; } = "";
        public List<string> table3 { get; private set; } = new List<string>();
        public string table4Name { get; private set; } = "";
        public List<string> table4 { get; private set; } = new List<string>();
        public string table5Name { get; private set; } = "";
        public List<string> table5 { get; private set; } = new List<string>();

        public Databasehandler_lb(bool testTable)
        {
            isTestVersion = testTable;
        }

        public void ChoiceMaker()
        {
            Clear();
            switch (choice)
            {
                case 1:
                    SelectFromBJ();
                    break;
                case 2:
                    SelectFromRL();
                    break;
                case 3:
                    SelectFromHM();
                    break;
                case 4:
                    SelectFromTTT();
                    break;
            }
        }

        public void Clear()
        {
            table.Clear();
            usernames.Clear();

            table1.Clear();
            table2.Clear();
            table3.Clear();
            table4.Clear();
            table5.Clear();

            table1Name = "";
            table2Name = "";
            table3Name = "";
            table4Name = "";
            table5Name = "";
        }

        public void ChangeChoice(int myChoice)
        {
            choice = myChoice;
        }

        private void SelectFromBJ()
        {
            SqlCommand cmd = new SqlCommand();

            if (isTestVersion == true)
            {
                cmd = new SqlCommand("SELECT TOP (10) Test_User_gegevens.Username, Test_Blackjack.saldo, Test_Blackjack.laatst_gespeeld FROM Test_User_gegevens INNER JOIN Test_Blackjack ON Test_User_gegevens.ID = Test_Blackjack.user_ID ORDER BY saldo DESC", GetCon());
            }
            else
            {
                cmd = new SqlCommand("SELECT TOP (10) User_gegevens.Username, Blackjack.saldo, Blackjack.laatst_gespeeld FROM User_gegevens INNER JOIN Blackjack ON User_gegevens.ID = Blackjack.user_ID ORDER BY saldo DESC", GetCon());
            }

            OpenConnectionToDB();

            SqlDataAdapter adapt = new SqlDataAdapter(cmd);
            adapt.Fill(table);

            CloseConnectionToDB();

            DateTime dt;

            table1Name = "Username";
            table2Name = "Balance";
            table3Name = "Last seen";

            foreach (DataRow row in table.Rows)
            {
                table1.Add(Convert.ToString(row["Username"]));
                table2.Add(Convert.ToString(row["saldo"]));

                dt = Convert.ToDateTime(row["laatst_gespeeld"]);

                table3.Add(dt.Date.ToString("MM/dd/yyyy"));
            }
        }

        private void SelectFromTTT()
        {
            SqlCommand cmd = new SqlCommand();

            if (isTestVersion == true)
            {
                cmd = new SqlCommand("SELECT TOP(10) Test_User_gegevens.Username, Test_BKE.gewonnen_potjes, Test_BKE.verloren_potjes, Test_BKE.gelijkspel_potjes, Test_BKE.laatst_gespeeld FROM Test_User_gegevens INNER JOIN Test_BKE ON Test_User_gegevens.ID = Test_BKE.user_ID ORDER BY gewonnen_potjes DESC", GetCon());
            }
            else
            {
                cmd = new SqlCommand("SELECT TOP(10) User_gegevens.Username, BKE.gewonnen_potjes, BKE.verloren_potjes, BKE.gelijkspel_potjes, BKE.laatst_gespeeld FROM User_gegevens INNER JOIN BKE ON User_gegevens.ID = BKE.user_ID ORDER BY gewonnen_potjes DESC", GetCon());
            }

            OpenConnectionToDB();

            SqlDataAdapter adapt = new SqlDataAdapter(cmd);
            adapt.Fill(table);

            CloseConnectionToDB();

            DateTime dt;

            table1Name = "Username";
            table2Name = "Games won";
            table3Name = "Games lost";
            table4Name = "Games tied";
            table5Name = "Last seen";

            foreach (DataRow row in table.Rows)
            {
                table1.Add(Convert.ToString(row["Username"]));
                table2.Add(Convert.ToString(row["gewonnen_potjes"]));
                table3.Add(Convert.ToString(row["verloren_potjes"]));
                table4.Add(Convert.ToString(row["gelijkspel_potjes"]));

                dt = Convert.ToDateTime(row["laatst_gespeeld"]);

                table5.Add(dt.Date.ToString("MM/dd/yyyy"));
            }
        }

        private void SelectFromHM()
        {
            SqlCommand cmd = new SqlCommand();

            if (isTestVersion == true)
            {
                cmd = new SqlCommand("SELECT TOP(10) Test_User_gegevens.Username, Test_Galgje.totaal_punten, Test_Galgje.totaal_fouten, Test_Galgje.laatst_gespeeld FROM Test_User_gegevens INNER JOIN Test_Galgje  ON Test_User_gegevens.ID = Test_Galgje.user_ID ORDER BY totaal_punten DESC", GetCon());
            }
            else
            {
                cmd = new SqlCommand("SELECT TOP(10) User_gegevens.Username, Galgje.totaal_punten, Galgje.totaal_fouten, Galgje.laatst_gespeeld FROM User_gegevens INNER JOIN Galgje ON User_gegevens.ID = Galgje.user_ID ORDER BY totaal_punten DESC", GetCon());
            }


            OpenConnectionToDB();

            SqlDataAdapter adapt = new SqlDataAdapter(cmd);
            adapt.Fill(table);

            CloseConnectionToDB();

            DateTime dt;

            table1Name = "Username";
            table2Name = "Points";
            table3Name = "Mistakes";
            table4Name = "Last seen";

            foreach (DataRow row in table.Rows)
            {
                table1.Add(Convert.ToString(row["Username"]));
                table2.Add(Convert.ToString(row["totaal_punten"]));
                table3.Add(Convert.ToString(row["totaal_fouten"]));

                dt = Convert.ToDateTime(row["laatst_gespeeld"]);

                table4.Add(dt.Date.ToString("MM/dd/yyyy"));
            }
        }

        private void SelectFromRL()
        {
            SqlCommand cmd = new SqlCommand();

            if (isTestVersion == true)
            {
                cmd = new SqlCommand("SELECT TOP(10) Test_User_gegevens.Username, Test_Roulette.saldo, Test_Roulette.laatst_gespeeld FROM Test_User_gegevens INNER JOIN Test_Roulette ON Test_User_gegevens.ID = Test_Roulette.user_ID ORDER BY saldo DESC", GetCon());
            }
            else
            {
                cmd = new SqlCommand("SELECT TOP(10) User_gegevens.Username, Roulette.saldo, Roulette.laatst_gespeeld FROM User_gegevens INNER JOIN Roulette ON User_gegevens.ID = Roulette.user_ID ORDER BY saldo DESC", GetCon());
            }

            OpenConnectionToDB();

            SqlDataAdapter adapt = new SqlDataAdapter(cmd);
            adapt.Fill(table);

            CloseConnectionToDB();

            DateTime dt;

            table1Name = "Username";
            table2Name = "Balance";
            table3Name = "Last seen";

            foreach (DataRow row in table.Rows)
            {
                table1.Add(Convert.ToString(row["Username"]));
                table2.Add(Convert.ToString(row["saldo"]));

                dt = Convert.ToDateTime(row["laatst_gespeeld"]);

                table3.Add(dt.Date.ToString("MM/dd/yyyy"));
            }
        }
    }
}