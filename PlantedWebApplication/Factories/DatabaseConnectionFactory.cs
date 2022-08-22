using System;
using System.Data.SqlClient;
using PlantedWebApplication.HelperClasses;

namespace PlantedWebApplication.Factories
{
    public static class DatabaseConnectionFactory
    {
        public static SqlDataConnection GetConnectionToAzureDatabase()
        {
            try
            {
                var connString = System.Configuration.ConfigurationManager.ConnectionStrings["Planted"].ConnectionString;
                var connection = new SqlConnection(connString);
                return new SqlDataConnection(connection.ConnectionString);
            }
            catch (Exception e)
            {
                throw new Exception($"An error occured while trying to create a Sql Data Connection : {e.Message}");
            }
        }
    }
}