using System.Collections.Generic;

namespace Rcade
{
    class Errorhandler
    {
        List<string> webbrowserError = new List<string>();
        List<string> connectionError = new List<string>();
        List<string> accountError = new List<string>();

        public Errorhandler()
        {
            webbrowserError.Add("Visit rcade.azurewebsites.net/ to create an account.");

            connectionError.Add("Connection to the database failed, contact us!");

            accountError.Add("Account doesn't exist!");
            accountError.Add("Username and/or password do not match!");
        }

        public string GetWebbrowserError(int error)
        {
            return webbrowserError[error];
        }

        public string GetConnectionError(int error)
        {
            return connectionError[error];
        }

        public string GetAccountError(int error)
        {
            return accountError[error];
        }
    }
}