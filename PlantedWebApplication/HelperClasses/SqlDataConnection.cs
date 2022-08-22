using System;
using System.Data;
using System.Data.SqlClient;

namespace PlantedWebApplication.HelperClasses
{
    public class SqlDataConnection
    {
        private readonly string _database;
        private readonly string _ipAddressStore;
        private readonly string _userName;
        private readonly string _userPassword;
        private SqlDataAdapter _da = new SqlDataAdapter();
        private DataTable _dt = new DataTable();
        private SqlCommand _sqlcmd = new SqlCommand();
        private SqlConnection _sqlcon = new SqlConnection();

        public SqlDataConnection(string ipAddressOfStoreOrServerName, string userName, string userPassword,
            string database)
        {
            _ipAddressStore = ipAddressOfStoreOrServerName;
            _database = database;
            _userName = userName;
            _userPassword = userPassword;

            BuildConnectionString();
        }

        public SqlDataConnection(string connectionString)
        {
            strconnection = connectionString;
        }

        public string strconnection { get; set; }

        private void BuildConnectionString()
        {
            var myConnString = new SqlConnectionStringBuilder
            {
                UserID = _userName,
                Password = _userPassword,
                MultipleActiveResultSets = true,
                DataSource = _ipAddressStore.Trim(),
                InitialCatalog = _database.Trim(),
                ConnectTimeout = 10
            };

            strconnection = myConnString.ConnectionString;
        }

        private void Connect()
        {
            _sqlcon = new SqlConnection(strconnection);
            _sqlcon.Open();
        }

        private void Disconnect()
        {
            if (_sqlcon.State == ConnectionState.Open)
            {
                _sqlcon.Close();
                _sqlcon.Dispose();
            }
        }

        public DataTable ReadData(string query)
        {
            try
            {
                Connect();
                _sqlcmd = new SqlCommand(query, _sqlcon);
                _da = new SqlDataAdapter(_sqlcmd);
                _dt = new DataTable();
                _da.Fill(_dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Disconnect();
            }

            return _dt;
        }

        public void QryCommand(string query)
        {
            try
            {
                Connect();
                _sqlcmd = new SqlCommand(query, _sqlcon);
                _sqlcmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Disconnect();
            }
        }

        public int QryScalar(string query)
        {
            int count;
            try
            {
                Connect();
                _sqlcmd = new SqlCommand(query, _sqlcon);
                count = (int)_sqlcmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Disconnect();
            }

            return count;
        }

        public bool IsServerConnected(string connectionString)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    return true;
                }
                catch (SqlException)
                {
                    return false;
                }
            }
        }
    }
}