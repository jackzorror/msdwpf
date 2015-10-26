using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace msdWPF.Model
{
    public class ClassModel
    {
        private MSDModel _MSDModel = new MSDModel();

        private SQLiteConnection conn
        {
            get
            {
                if (null == _conn)
                {
                    _conn = new MSDModelUtil().GetSQLiteDBConnection();
                }
                return _conn;
            }
        }
        private SQLiteConnection _conn;

        internal List<SchoolSemester> FindAllSemesterList()
        {
            return _MSDModel.FindAllSchoolSemester();
        }
    }
}
