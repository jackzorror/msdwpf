using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace msdWPF.Model
{
    public class MSDStudent
    {
        public Int32 Id { get; set; }
        public String LastName { get; set; }
        public String FirstName { get; set; }
        public String Gender { get; set; }
        public DateTime? Dob { get; set; }
        public String HomePhone { get; set; }
        public String CellPhone { get; set; }
        public String EmailAddress { get; set; }
        public String SchoolName { get; set; }
        public String SchoolGrade { get; set; }
        public String HomeAddress { get; set; }
        public bool IsActive { get; set; }
    }
}
