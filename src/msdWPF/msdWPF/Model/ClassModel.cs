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

        internal List<SchoolClass> FindAllSchoolClass ()
        {
            List<SchoolClass> classes = new List<SchoolClass>();
            String query = "select sc.*, ss.name as semester_name, at.name as class_type_name " +
                " from school_class as sc " + 
                " join school_semester as ss on sc.semester_id = ss.id " +
                " join application_type as at on sc.class_type_id = at.id  " +
                " WHERE is_active = 1 " +
                " order by start_date desc, semester_id desc, sc.name;";
            try
            {
                SQLiteCommand command = new SQLiteCommand(query, conn);
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    SchoolClass sc = new SchoolClass();
                    sc.Id = System.DBNull.Value != reader["id"] ? Convert.ToInt32(reader["id"]) : 0;
                    sc.Name = System.DBNull.Value != reader["name"] ? (String)reader["name"] : null;
                    sc.StartDate = System.DBNull.Value != reader["start_date"] ? (DateTime?)reader["start_date"] : (DateTime?)null;
                    sc.EndDate = System.DBNull.Value != reader["end_date"] ? (DateTime?)reader["end_date"] : (DateTime?)null;
                    sc.Status = System.DBNull.Value != reader["status"] ? (String)reader["status"] : null;
                    sc.SemesterName = System.DBNull.Value != reader["semester_name"] ? (String)reader["semester_name"] : null;
                    sc.ClassTypeName = System.DBNull.Value != reader["class_type_name"] ? (String)reader["class_type_name"] : null;
                    sc.ClassTypeId = System.DBNull.Value != reader["class_type_id"] ? Convert.ToInt32(reader["class_type_id"]) : 0;
                    sc.SemesterId = System.DBNull.Value != reader["semester_id"] ? Convert.ToInt32(reader["semester_id"]) : 0;
                    sc.IsActive = System.DBNull.Value != reader["is_active"] ? Convert.ToInt32(reader["is_active"]) == 1 : false;

                    classes.Add(sc);
                }
                
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            return classes;
        } 
    }
}
