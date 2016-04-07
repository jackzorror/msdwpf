using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

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

        internal List<SchoolClassSummary> FindAllSchoolClassSummary()
        {
            List<SchoolClassSummary> summaries = null;
            List<SchoolClass> classes = FindAllSchoolClass();

            if (null != classes && classes.Count > 0)
            {
                summaries = new List<SchoolClassSummary>();
                foreach (SchoolClass schoolClass in classes)
                {
                    SchoolClassSummary summary = new SchoolClassSummary();
                    summary.Id = schoolClass.Id;
                    summary.Name = schoolClass.Name;
                    summary.SemesterId = schoolClass.SemesterId;
                    summary.ClassTypeId = schoolClass.ClassTypeId;

                    summaries.Add(summary);
                }

            }
            return summaries;
        }

        internal List<SchoolClass> FindAllSchoolClassesBySemesterId(int semesterid)
        {
            List<SchoolClass> classes = null;
            String query = "select sc.*, ss.name as semester_name, at.name as class_type_name " +
                " from school_class as sc " +
                " join school_semester as ss on sc.semester_id = ss.id " +
                " join application_type as at on sc.class_type_id = at.id  " +
                " WHERE sc.is_active = 1 " +
                " AND sc.semester_id = " + semesterid +
                " order by start_date desc, semester_id desc, sc.name;";
            try
            {
                SQLiteCommand command = new SQLiteCommand(query, conn);
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    if (null == classes) classes = new List<SchoolClass>();

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

        internal List<SchoolClassSummary> FindAllSchoolClassSummariesBySemesterId(int semesterid)
        {
            List<SchoolClassSummary> summaries = null;
            List<SchoolClass> classes = FindAllSchoolClassesBySemesterId(semesterid);
            if (null != classes && classes.Count > 0)
            {
                summaries = new List<SchoolClassSummary>();
                foreach (SchoolClass schoolClass in classes)
                {
                    if (schoolClass.SemesterId.Equals(semesterid))
                    {
                        SchoolClassSummary summary = new SchoolClassSummary();
                        summary.Id = schoolClass.Id;
                        summary.Name = schoolClass.Name;
                        summary.SemesterId = schoolClass.SemesterId;
                        summary.ClassTypeId = schoolClass.ClassTypeId;

                        summaries.Add(summary);
                    }
                }
            }
            return summaries;
        } 

        internal SchoolClass FindSchoolClassById(int cid)
        {
            SchoolClass sc = new SchoolClass();
            String query = "select sc.*, ss.name as semester_name, at.name as class_type_name " +
                " from school_class as sc " +
                " join school_semester as ss on sc.semester_id = ss.id " +
                " join application_type as at on sc.class_type_id = at.id  " +
                " WHERE sc.id = " + cid + " AND is_active = 1 " + 
                " order by start_date desc, semester_id desc, sc.name;";
            try
            {
                SQLiteCommand command = new SQLiteCommand(query, conn);
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
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
                }

            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            return sc;
        }

        internal void SaveSchoolClass(SchoolClass sclass)
        {
            String query = " UPDATE school_class  SET " +
                           " name = '" + sclass.Name + "'" +
                           ", start_date = " + (sclass.StartDate != null ? "'" + ((DateTime)sclass.StartDate).ToString("yyyy-MM-dd HH:mm:ss") + "'" : "null") +
                           ", end_date = " + (sclass.EndDate != null ? "'" + ((DateTime)sclass.EndDate).ToString("yyyy-MM-dd HH:mm:ss") + "'" : "null") +
                           " WHERE id = " + sclass.Id;
            try
            {
                SQLiteCommand command = new SQLiteCommand(query, conn);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        internal void AddSchoolClass(SchoolClass sclass)
        {
            String query = " INSERT INTO school_class(name, start_date, end_date, class_type_id, semester_id) VALUES(" +
                           " '" + sclass.Name + "'" +
                           ", " + (sclass.StartDate != null ? "'" + ((DateTime)sclass.StartDate).ToString("yyyy-MM-dd HH:mm:ss") + "'" : "null") +
                           ", " + (sclass.EndDate != null ? "'" + ((DateTime)sclass.EndDate).ToString("yyyy-MM-dd HH:mm:ss") + "'" : "null") +
                           ", " + sclass.ClassTypeId + 
                           ", " + sclass.SemesterId + 
                           "); ";
            try
            {
                SQLiteCommand command = new SQLiteCommand(query, conn);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        internal List<NonClassDate> FindNonClassDateListByClassId(int cid)
        {
            String query = " SELECT * FROM non_class_date WHERE class_id = " + cid + " order by non_class_date;";
            List<NonClassDate> ncList = new List<NonClassDate>();
            try
            {
                SQLiteCommand command = new SQLiteCommand(query, conn);
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    NonClassDate ndate = new NonClassDate();
                    ndate.Id = System.DBNull.Value != reader["id"] ? Convert.ToInt32(reader["id"]) : 0;
                    ndate.NonClassDateTime = System.DBNull.Value != reader["non_class_date"] ? (DateTime?)reader["non_class_date"] : (DateTime?)null;
                    ndate.ClassId = System.DBNull.Value != reader["class_id"] ? Convert.ToInt32(reader["class_id"]) : 0;
                    ncList.Add(ndate);
                }

            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            return ncList;
        }

        internal void AddNonClassDate(NonClassDate nonClassDate)
        {
            String query = " INSERT INTO non_class_date (non_class_date, class_id) VALUES (" +
                           " " + (nonClassDate.NonClassDateTime != null ? "'" + ((DateTime)nonClassDate.NonClassDateTime).ToString("yyyy-MM-dd HH:mm:ss") + "'" : "null") +
                           ",  " + nonClassDate.ClassId +
                           "); ";
            try
            {
                SQLiteCommand command = new SQLiteCommand(query, conn);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        internal void DeleteNonClassDate(NonClassDate nonClassDate)
        {
            String query = " DELETE from non_class_date where id = " + nonClassDate.Id + ";";
            try
            {
                SQLiteCommand command = new SQLiteCommand(query, conn);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
