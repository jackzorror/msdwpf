using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace msdWPF.Model
{
    public class StudentModel
    {
        private MSDModel _MSDModel = new MSDModel();
        private ClassModel _classModel = new ClassModel();

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

        private SQLiteConnection lconn
        {
            get
            {
                if (null == _lconn)
                {
                    _lconn = new MSDModelUtil().GetSQLiteDBConnection();
                }
                return _lconn;
            }
        }
        private SQLiteConnection _lconn;

        internal MSDStudent FindStudentByFirstNameLastName(string first, string last)
        {
            String query = "SELECT * FROM student WHERE upper(last_name) = upper('" + last + "') AND upper(first_name) = upper('" + first + "');";
            try
            {
                SQLiteCommand command = new SQLiteCommand(query,lconn);
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    MSDStudent student = new MSDStudent();
                    student.Id = System.DBNull.Value != reader["id"] ? Convert.ToInt32(reader["id"]) : 0;
                    student.LastName = System.DBNull.Value != reader["last_name"] ? (String)reader["last_name"] : null;
                    student.FirstName = System.DBNull.Value != reader["first_name"] ? (String)reader["first_name"] : null;
                    student.Gender = System.DBNull.Value != reader["gender"] ? (String)reader["gender"] : null;
                    student.Dob = System.DBNull.Value != reader["dob"] ? (DateTime?)reader["dob"] : (DateTime?)null;
                    student.HomePhone = System.DBNull.Value != reader["home_phone"] ? (String)reader["home_phone"] : null;
                    student.CellPhone = System.DBNull.Value != reader["cell_phone"] ? (String)reader["cell_phone"] : null;
                    student.EmailAddress = System.DBNull.Value != reader["email_address"] ? (String)reader["email_address"] : null;
                    student.HomeAddress = System.DBNull.Value != reader["home_address"] ? (String)reader["home_address"] : null;
                    student.IsActive = System.DBNull.Value != reader["is_active"] ? Convert.ToInt32(reader["is_active"]) == 1 : false;

                    return student;
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            return null;
        }

        public List<String> FindAllStudentLastName()
        {
            String query = "SELECT distinct(last_name) FROM student WHERE is_active = 1 order by last_name;";
            try
            {
                List<String> nameList = new List<string>();

                SQLiteCommand command = new SQLiteCommand(lconn);
                command.CommandText = query;
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    nameList.Add(Convert.ToString(reader["last_name"]));
                }
                return nameList;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        internal List<string> FindAllStudentFirstName()
        {
            String query = "SELECT distinct(first_name) FROM student WHERE is_active = 1 order by first_name;";
            try
            {
                List<String> nameList = new List<string>();

                SQLiteCommand command = new SQLiteCommand(lconn);
                command.CommandText = query;
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    nameList.Add(Convert.ToString(reader["first_name"]));
                }
                return nameList;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        internal List<MSDStudentParent> FindStudentParentsByStudentId(int sid)
        {
            String query = "SELECT * FROM student_parent WHERE student_id = " + sid + ";";
            try
            {
                SQLiteCommand command = new SQLiteCommand(query, lconn);
                SQLiteDataReader reader = command.ExecuteReader();
                List<MSDStudentParent> parents = new List<MSDStudentParent>();
                parents.Add(new MSDStudentParent());
                parents.Add(new MSDStudentParent());
                int index = 0;
                while (reader.Read())
                {
                    var parent = parents[index];
                    parent.Id = System.DBNull.Value != reader["id"] ? Convert.ToInt32(reader["id"]) : 0;
                    parent.LastName = System.DBNull.Value != reader["last_name"] ? (String)reader["last_name"] : null;
                    parent.FirstName = System.DBNull.Value != reader["first_name"] ? (String)reader["first_name"] : null;
                    parent.WorkPhone = System.DBNull.Value != reader["work_phone"] ? (String)reader["work_phone"] : null;
                    parent.CellPhone = System.DBNull.Value != reader["cell_phone"] ? (String)reader["cell_phone"] : null;
                    parent.EmailAddress = System.DBNull.Value != reader["email_address"] ? (String)reader["email_address"] : null;
                    parent.Relationship = System.DBNull.Value != reader["relationship"] ? (String)reader["relationship"] : null;
                    parent.MSDStudentId = System.DBNull.Value != reader["student_id"] ? Convert.ToInt32(reader["student_id"]) : sid;
                    if (index == 1) break;
                    index++;
                }

                return parents;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        internal MSDStudentMedical FindStudentMedicalByStudentId(int sid)
        {
            String query = "SELECT * FROM student_medical WHERE student_id = " + sid + ";";
            MSDStudentMedical medical = new MSDStudentMedical();
            try
            {
                SQLiteCommand command = new SQLiteCommand(query, lconn);
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    medical.Id = System.DBNull.Value != reader["id"] ? Convert.ToInt32(reader["id"]) : 0;
                    medical.InsuranceCompany = System.DBNull.Value != reader["insurance_company"] ? (String)reader["insurance_company"] : null;
                    medical.PolicyNumber = System.DBNull.Value != reader["policy_number"] ? (String)reader["policy_number"] : null;
                    medical.Phone = System.DBNull.Value != reader["phone"] ? (String)reader["phone"] : null;
                    medical.PediatricianName = System.DBNull.Value != reader["pediatrician_name"] ? (String)reader["pediatrician_name"] : null;
                    medical.EmergencyName = System.DBNull.Value != reader["emergency_name"] ? (String)reader["emergency_name"] : null;
                    medical.EmergencyPhone = System.DBNull.Value != reader["emergency_phone"] ? (String)reader["emergency_phone"] : null;
                    medical.EmergencyPhoneAlt = System.DBNull.Value != reader["emergency_phone_alt"] ? (String)reader["emergency_phone_alt"] : null;
                    medical.MSDStudentId = System.DBNull.Value != reader["student_id"] ? Convert.ToInt32(reader["student_id"]) : sid;
                    break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return medical;
        }

        internal void SaveStudentInformation(MSDStudent student)
        {
            String query = " UPDATE student  SET " +
                           " last_name = '" + student.LastName + "'" +
                           ", first_name = '" + student.FirstName + "'" +
                           ", gender = '" + student.Gender + "'" +
                           ", dob = " + (student.Dob != null ? "'" + ((DateTime)student.Dob).ToString("yyyy-MM-dd HH:mm:ss") + "'": "null") +
                           ", home_phone = '" + student.HomePhone + "'" +
                           ", cell_phone = '" + student.CellPhone + "'" +
                           ", email_address = '" + student.EmailAddress + "'" +
                           ", home_address = '" + student.HomeAddress + "'" + 
                           " WHERE id = " + student.Id;
            try
            {
                SQLiteCommand command = new SQLiteCommand(query, lconn);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        internal MSDStudent AddStudentInformation(MSDStudent student)
        {
            String query = " INSERT INTO student (last_name, first_name, gender, dob, home_phone, cell_phone, email_address, home_address) VALUES (" +
                           " '" + student.LastName + "'" +
                           ",  '" + student.FirstName + "'" +
                           ",  '" + student.Gender + "'" +
                           ", " + (student.Dob != null ? "'" + ((DateTime)student.Dob).ToString("yyyy-MM-dd HH:mm:ss") + "'" : "null") +
                           ", '" + student.HomePhone + "'" +
                           ", '" + student.CellPhone + "'" +
                           ", '" + student.EmailAddress + "'" +
                           ", '" + student.HomeAddress + "'" +
                           "); ";
            try
            {
                SQLiteCommand command = new SQLiteCommand(query, lconn);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return FindStudentByFirstNameLastName(student.FirstName, student.LastName);
        }

        internal void SaveStudentParent(MSDStudentParent parent)
        {
            String query = "";
            if (parent.Id != 0)
            {
                query = " UPDATE student_parent  SET " +
                               " last_name = '" + parent.LastName + "'" +
                               ", first_name = '" + parent.FirstName + "'" +
                               ", work_phone = '" + parent.WorkPhone + "'" +
                               ", cell_phone = '" + parent.CellPhone + "'" +
                               ", email_address = '" + parent.EmailAddress + "'" +
                               ", relationship = '" + parent.Relationship + "'" +
                               " WHERE id = " + parent.Id;
            }
            else
            {
                query = " INSERT INTO student_parent (last_name, first_name, work_phone, cell_phone, email_address, relationship, student_id ) VALUES (" +
                               "  '" + parent.LastName + "'" +
                               ", '" + parent.FirstName + "'" +
                               ", '" + parent.WorkPhone + "'" +
                               ", '" + parent.CellPhone + "'" +
                               ", '" + parent.EmailAddress + "'" +
                               ", '" + parent.Relationship + "'" +
                               ", " + parent.MSDStudentId +
                               "); ";
            }
            try
            {
                SQLiteCommand command = new SQLiteCommand(query, lconn);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        internal void SaveStudentMedical(MSDStudentMedical medical)
        {
            String query = "";
            if (medical.Id != 0)
            {
                query = " UPDATE student_medical  SET " +
                               " insurance_company = '" + medical.InsuranceCompany + "'" +
                               ", policy_number = '" + medical.PolicyNumber + "'" +
                               ", phone = '" + medical.Phone + "'" +
                               ", pediatrician_name = '" + medical.PediatricianName + "'" +
                               ", emergency_name = '" + medical.EmergencyName + "'" +
                               ", emergency_phone = '" + medical.EmergencyPhone + "'" +
                               ", emergency_phone_alt = '" + medical.EmergencyPhoneAlt + "'" +
                               " WHERE id = " + medical.Id;
            }
            else
            {
                query = " INSERT INTO student_medical (insurance_company, policy_number, phone, pediatrician_name, emergency_name, emergency_phone, emergency_phone_alt, student_id) VALUES (" +
                               " '" + medical.InsuranceCompany + "'" +
                               ", '" + medical.PolicyNumber + "'" +
                               ", '" + medical.Phone + "'" +
                               ", '" + medical.PediatricianName + "'" +
                               ", '" + medical.EmergencyName + "'" +
                               ", '" + medical.EmergencyPhone + "'" +
                               ", '" + medical.EmergencyPhoneAlt + "'" +
                               ", " + medical.MSDStudentId +
                               ");";
            }

            try
            {
                SQLiteCommand command = new SQLiteCommand(query, lconn);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        internal MSDStudent FindStudentById(int id)
        {
            String query = "SELECT * FROM student WHERE id = " + id + ";";
            try
            {
                SQLiteCommand command = new SQLiteCommand(query, lconn);
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    MSDStudent student = new MSDStudent();
                    student.Id = System.DBNull.Value != reader["id"] ? Convert.ToInt32(reader["id"]) : 0;
                    student.LastName = System.DBNull.Value != reader["last_name"] ? (String)reader["last_name"] : null;
                    student.FirstName = System.DBNull.Value != reader["first_name"] ? (String)reader["first_name"] : null;
                    student.Gender = System.DBNull.Value != reader["gender"] ? (String)reader["gender"] : null;
                    student.Dob = System.DBNull.Value != reader["dob"] ? (DateTime?)reader["dob"] : (DateTime?)null;
                    student.HomePhone = System.DBNull.Value != reader["home_phone"] ? (String)reader["home_phone"] : null;
                    student.CellPhone = System.DBNull.Value != reader["cell_phone"] ? (String)reader["cell_phone"] : null;
                    student.EmailAddress = System.DBNull.Value != reader["email_address"] ? (String)reader["email_address"] : null;
                    student.HomeAddress = System.DBNull.Value != reader["home_address"] ? (String)reader["home_address"] : null;
                    student.IsActive = System.DBNull.Value != reader["is_active"] ? Convert.ToInt32(reader["is_active"]) == 1 : false;

                    return student;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        internal List<SchoolSemester> FindAllSemesterList()
        {
            
            return _MSDModel.FindAllSchoolSemester();
        }


        internal SchoolSemester FindCurrentSchoolSemester()
        {
            return _MSDModel.FindCurrentSchoolSemester();
        }

        internal List<SchoolClassSummary> GetRegisteredClassSummariesByStudentIdAndSemesterId(int studentid, int semesterid)
        {
            String query =
                "SELECT * FROM school_class AS sc JOIN student_registered_class AS src ON sc.id = src.class_id " +
                " WHERE sc.is_active = 1 AND src.is_active = 1 AND src.student_id = " + studentid +
                " AND sc.semester_id = " + semesterid;
            List<SchoolClassSummary> rClassSummaries = null;
            try
            {
                SQLiteCommand command = new SQLiteCommand(query, lconn);
                SQLiteDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    if (null == rClassSummaries) rClassSummaries = new List<SchoolClassSummary>();

                    SchoolClassSummary sClassSummary = new SchoolClassSummary();
                    sClassSummary.Id = System.DBNull.Value != reader["id"] ? Convert.ToInt32(reader["id"]) : 0;
                    sClassSummary.Name = System.DBNull.Value != reader["name"] ? (String) reader["name"] : null;
                    sClassSummary.SemesterId = System.DBNull.Value != reader["semester_id"]
                        ? Convert.ToInt32(reader["semester_id"])
                        : 0;
                    sClassSummary.ClassTypeId = System.DBNull.Value != reader["class_type_id"]
                        ? Convert.ToInt32(reader["class_type_id"])
                        : 0;
                    rClassSummaries.Add(sClassSummary);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return rClassSummaries;
        }

        internal List<SchoolClassSummary> GetNonRegisterdClassSummariesByStudentIdAndSemesterId(int studentid, int semesterid)
        {
            List<SchoolClassSummary> registeredCalss = GetRegisteredClassSummariesByStudentIdAndSemesterId(studentid, semesterid);
            List<SchoolClassSummary> currentClassInSemester =
                _classModel.FindAllSchoolClassSummariesBySemesterId(semesterid);
            List<SchoolClassSummary> summaries = new List<SchoolClassSummary>();

            foreach (SchoolClassSummary summary in currentClassInSemester)
            {
                Boolean found = false;
                foreach (SchoolClassSummary classSummary in registeredCalss)
                {
                    if (classSummary.Id.Equals(summary.Id))
                    {
                        found = true;
                        break;
                    }
                }
                if (!found) summaries.Add(summary);
            }
            return summaries;
        }
    }
}
