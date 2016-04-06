using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics.Eventing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace msdWPF.Model
{
    public class MSDModelUtil
    {
        private MySqlConnection _connection = null;
        private SQLiteConnection _sqLiteConnection = null;

        public MySqlConnection GetMSDDatabaseConnection()
        {
            if (null == _connection)
            {
                MySqlConnectionStringBuilder connString = new MySqlConnectionStringBuilder();
                connString.Server = "localhost";
                connString.UserID = "msd";
                connString.Port = 3306;
                connString.Database = "msd";
                connString.Password = "Password1";
                _connection = new MySqlConnection(connString.ToString());
                try
                {
                    _connection.Open();
                }
                catch (Exception ex)
                {
                    _connection = null;
                }
            }

            return _connection;
        }

        public SQLiteConnection GetSQLiteDBConnection ()
        {
            if (null == _sqLiteConnection)
            {
                string inputFile = "C:\\msd\\msdwpf\\db\\msd_sqlite.s3db";
                string dbConnection = String.Format("Data Source={0}", inputFile);
                _sqLiteConnection = new SQLiteConnection(dbConnection);
                try
                {
                    _sqLiteConnection.Open();
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                    _sqLiteConnection = null;
                }
            }

            return _sqLiteConnection;
        }
    }
}
