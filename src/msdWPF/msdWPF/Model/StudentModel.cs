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
            String query = "SELECT distinct(last_name) FROM student order by last_name;";
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
            String query = "SELECT distinct(first_name) FROM student order by first_name;";
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
            String query = "SELECT * WHERE msd_student_id = " + sid + ";";
            try
            {
                MySqlCommand command = new MySqlCommand(query, conn);
                MySqlDataReader reader = command.ExecuteReader();
                DataTable table = new DataTable();
                table.Load(reader);
                if (table.Rows.Count > 0)
                {
                    List<MSDStudentParent> parents = new List<MSDStudentParent>();
                    foreach (DataRow row in table.Rows)
                    {
                        MSDStudentParent parent = new MSDStudentParent();
                        parent.Id = System.DBNull.Value != row.ItemArray[0] ? (int)row.ItemArray[0] : 0;
                        parent.LastName = System.DBNull.Value != row.ItemArray[1] ? (String)row.ItemArray[1] : null;
                        parent.FirstName = System.DBNull.Value != row.ItemArray[2] ? (String)row.ItemArray[2] : null;
                        parent.WorkPhone = System.DBNull.Value != row.ItemArray[3] ? (String)row.ItemArray[3] : null;
                        parent.CellPhone = System.DBNull.Value != row.ItemArray[4] ? (String)row.ItemArray[4] : null;
                        parent.EmailAddress = System.DBNull.Value != row.ItemArray[5] ? (String)row.ItemArray[5] : null;
                        parent.Relationship = System.DBNull.Value != row.ItemArray[6] ? (String)row.ItemArray[6] : null;
                        parent.MSDStudentId = System.DBNull.Value != row.ItemArray[7] ? (int)row.ItemArray[7] : 0;
                        parents.Add(parent);
                    }

                    return parents;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        internal MSDStudentMedical FindStudentMedicalByStudentId(int sid)
        {
            String query = "SELECT id, insurance_company, policy_number, phone, pediatrician_name, emergency_name, emergency_phone, emergency_phone_alt, msd_student_id FROM msd.msd_student_medical_info WHERE msd_student_id = " + sid + ";";
            try
            {
                MySqlCommand command = new MySqlCommand(query, conn);
                MySqlDataReader reader = command.ExecuteReader();
                DataTable table = new DataTable();
                table.Load(reader);
                if (table.Rows.Count == 1)
                {
                    MSDStudentMedical medical = new MSDStudentMedical();
                    DataRow row = table.Rows[0];
                    medical.Id = System.DBNull.Value != row.ItemArray[0] ? (int)row.ItemArray[0] : 0;
                    medical.InsuranceCompany = System.DBNull.Value != row.ItemArray[1] ? (String)row.ItemArray[1] : null;
                    medical.PolicyNumber = System.DBNull.Value != row.ItemArray[2] ? (String)row.ItemArray[2] : null;
                    medical.Phone = System.DBNull.Value != row.ItemArray[3] ? (String)row.ItemArray[3] : null;
                    medical.PediatricianName = System.DBNull.Value != row.ItemArray[4] ? (String)row.ItemArray[4] : null;
                    medical.EmergencyName = System.DBNull.Value != row.ItemArray[5] ? (String)row.ItemArray[5] : null;
                    medical.EmergencyPhone = System.DBNull.Value != row.ItemArray[6] ? (String)row.ItemArray[6] : null;
                    medical.EmergencyPhoneAlt = System.DBNull.Value != row.ItemArray[7] ? (String)row.ItemArray[7] : null;
                    medical.MSDStudentId = System.DBNull.Value != row.ItemArray[8] ? (int)row.ItemArray[8] : 0;

                    return medical;
                }
            }
            catch (Exception ex)
            {
                ;
            }
            return null;
        }

        internal void SaveStudentInformation(MSDStudent student)
        {
            String query = " UPDATE msd.msd_student  SET " +
                           " last_name = '" + student.LastName + "'" +
                           ", first_name = '" + student.FirstName + "'" +
                           ", gender = '" + student.Gender + "'" +
                           ", dob = '" + ((DateTime)student.Dob).ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                           ", home_phone = '" + student.HomePhone + "'" +
                           ", cell_phone = '" + student.CellPhone + "'" +
                           ", email_address = '" + student.EmailAddress + "'" +
                           ", school_name = '" + student.SchoolName + "'" +
                           ", school_grade = '" + student.SchoolGrade + "'" +
                           ", home_address = '" + student.HomeAddress + "'" + 
                           " WHERE id = " + student.Id;
            try
            {
                MySqlCommand command = new MySqlCommand(query,conn);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        internal void SaveStudentParent(MSDStudentParent parent)
        {
            String query = " UPDATE msd.msd_student_parent  SET " +
                           " last_name = '" + parent.LastName + "'" +
                           ", first_name = '" + parent.FirstName + "'" +
                           ", work_phone = '" + parent.WorkPhone + "'" +
                           ", cell_phone = '" + parent.CellPhone + "'" +
                           ", email_address = '" + parent.EmailAddress + "'" +
                           ", relationship = '" + parent.Relationship + "'" +
                           " WHERE id = " + parent.Id;
            try
            {
                MySqlCommand command = new MySqlCommand(query, conn);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        internal void SaveStudentMedical(MSDStudentMedical medical)
        {
            String query = " UPDATE msd.msd_student_medical_info  SET " +
                           " insurance_company = '" + medical.InsuranceCompany + "'" +
                           ", policy_number = '" + medical.PolicyNumber + "'" +
                           ", phone = '" + medical.Phone + "'" +
                           ", pediatrician_name = '" + medical.PediatricianName + "'" +
                           ", emergency_name = '" + medical.EmergencyName + "'" +
                           ", emergency_phone = '" + medical.EmergencyPhone + "'" +
                           ", emergency_phone_alt = '" + medical.EmergencyPhoneAlt + "'" +
                           " WHERE id = " + medical.Id;
            try
            {
                MySqlCommand command = new MySqlCommand(query, conn);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        internal MSDStudent FindStudentById(int id)
        {

            String query = "SELECT * FROM msd.msd_student WHERE id = " + id + ";";
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
                ;
            }
            return null;
        }
    }
}
