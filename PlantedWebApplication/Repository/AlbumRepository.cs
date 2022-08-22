using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using PlantedWebApplication.Factories;
using PlantedWebApplication.Models;

namespace PlantedWebApplication.Repository
{
    public class AlbumRepository
    {
        internal List<AlbumPrice> GetAlbumPrices(int discogsId)
        {
            using (var connection =
                   new SqlConnection(DatabaseConnectionFactory.GetConnectionToAzureDatabase().strconnection))
            {
                try
                {
                    var query = $@"select [PriceDate] as PriceDate,[Price] as Price from [dbo].[RecordPrices] where DiscogsId = {discogsId}";
                    return connection.Query<AlbumPrice>(query) as List<AlbumPrice>;
                }
                catch (Exception ex)
                {
                    throw new Exception("No Albums could be found in the database : " + ex.Message);
                }
            }
        }
        
        internal string GetAlbumName(int discogsId)
        {
            using (var connection = new SqlConnection(DatabaseConnectionFactory.GetConnectionToAzureDatabase().strconnection))
            {
                try
                {
                    var query = $@"SELECT [Artist] + ' - ' + [AlbumName] FROM [dbo].[FavoriteRecords] WHERE DiscogsId = {discogsId}";
                    return connection.Query<string>(query).FirstOrDefault().ToString();
                }
                catch (Exception ex)
                {
                    throw new Exception("No Album name could be found in the database : " + ex.Message);
                }
            }
        }
        internal List<Album> GetAlbums()
        {
            using (var connection =
                   new SqlConnection(DatabaseConnectionFactory.GetConnectionToAzureDatabase().strconnection))
            {
                try
                {
                    var query = $@"select DiscogsId, AlbumName from [dbo].[FavoriteRecords]";
                    return connection.Query<Album>(query) as List<Album>;
                }
                catch (Exception ex)
                {
                    throw new Exception("No Albums could be found in the database : " + ex.Message);
                }
            }
        }
    }
}