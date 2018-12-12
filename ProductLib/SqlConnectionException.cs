using System;

namespace ProductLib
{
    public class SqlConnectionException : Exception
    {
        public SqlConnectionException() : base("Kan ikke forbinde til Databasen...")
        {
        }

        public SqlConnectionException(string message) : base(message)
        {
        }
    }
}
