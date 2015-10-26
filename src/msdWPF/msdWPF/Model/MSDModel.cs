using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace msdWPF.Model
{
    public class MSDModel
    {
        private MySqlConnection conn
        {
            get
            {
                if (null == _conn)
                {
                    _conn = new MSDModelUtil().GetMSDDatabaseConnection();
                }
                return _conn;
            }
        }
        private MySqlConnection _conn;

        public List<String> FindAllMSDSemester()
        {
            return null;
        } 
    }
}
