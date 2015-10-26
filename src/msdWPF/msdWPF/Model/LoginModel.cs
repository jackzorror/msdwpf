using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Annotations;
using MySql.Data.MySqlClient;

namespace msdWPF.Model
{
    public class LoginModel
    {
        private MSDModelUtil _util;

        public LoginModel()
        {
            _util = new MSDModelUtil();
        }
        internal bool Login(string Username, string Password)
        {
            if (String.IsNullOrEmpty(Username) || (String.IsNullOrEmpty(Password))) 
                return false;

            
            if (FindUserByUsernamePassword(Username, EncodingPassword(Password)))
            {
                return true;
            }

            return false;
        }

        private bool FindUserByUsernamePassword(string Username, string Password)
        {
            SQLiteConnection _conn = _util.GetSQLiteDBConnection();

            String sql = string.Format("SELECT COUNT(*) FROM user WHERE username = '{0}' AND password ='{1}' AND status = 1", Username, Password);
            try
            {
                SQLiteCommand mycommand = new SQLiteCommand(_conn);
                mycommand.CommandText = sql;
                Object count = mycommand.ExecuteScalar();
                if (Convert.ToInt32(count) == 1)
                    return true;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            return false;
            /*
            MySqlConnection conn = _util.GetMSDDatabaseConnection();
            SQLiteConnection _conn = _util.GetSQLiteDBConnection();
            String query = "Select * from msd.msd_user where username = '" + Username + "' and password = '" + Password + "';";
            try
            {
                MySqlCommand command = new MySqlCommand(query, conn);
                MySqlDataReader reader = command.ExecuteReader();
                DataTable table = new DataTable();
                table.Load(reader);
                if (table.Rows.Count == 1)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                ;
            }
            return false;
            */
        }

        private string EncodingPassword(string password)
        {
            var hasher = new SHA512Managed();
            var unhashed = System.Text.Encoding.Unicode.GetBytes(password);
            var hashed = hasher.ComputeHash(unhashed);
            var hashedPassword = Convert.ToBase64String(hashed);

            return hashedPassword;
        }
    }
}
