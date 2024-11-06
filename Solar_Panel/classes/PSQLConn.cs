using System;
using Npgsql;

namespace connect
{

    public class PSQLCon
    {
        private String test;
        String connectionString;

        public String Test { get; set; }
        public String ConnectionString {get;set;}
        public PSQLCon()
        {
            Init_Connection();
        }

        private void Init_Connection()
        {
            ConnectionString="Host=localhost;Port=5432;Database=solar;Username=solar;Password=solar;";

            using var connection = new NpgsqlConnection(ConnectionString);

            try
            {
                connection.Open();
                Test = "Connected to PostgreSQL!";
                Console.WriteLine("Connected to PostgreSQL!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}" );
                Test = $"Error: {ex.Message}";
            }
            finally
            {
                connection.Close();
            }
        }
    }
}