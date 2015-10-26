using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace msdWPF.Model
{
    public class MSDModel
    {
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

        public List<SchoolSemester> FindAllSchoolSemester()
        {
            String query = "SELECT * FROM school_semester;";
            List<SchoolSemester> semesters = new List<SchoolSemester>();
            try
            {
                SQLiteCommand command = new SQLiteCommand(query, conn);
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    SchoolSemester semester = new SchoolSemester();
                    semester.Id = System.DBNull.Value != reader["id"] ? Convert.ToInt32(reader["id"]) : 0;
                    semester.Name = System.DBNull.Value != reader["name"] ? (String)reader["name"] : null;
                    semester.StartDate = System.DBNull.Value != reader["start_date"] ? (DateTime?)reader["start_date"] : (DateTime?)null;

                    semesters.Add(semester);
                }

            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            return semesters;
        } 
    }
}
