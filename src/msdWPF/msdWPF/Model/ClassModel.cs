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

        internal List<ApplicationType> FindClassTypeList()
        {
            List<ApplicationType> atypes = _MSDModel.FindAllApplicationTypes();
            List<ApplicationType> ctypes = atypes.FindAll(x => x.Type.Equals("CLASS_TYPE"));
            return ctypes;
        } 
    }
}
