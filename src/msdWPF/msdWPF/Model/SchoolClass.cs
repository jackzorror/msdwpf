using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace msdWPF.Model
{
    public class SchoolClass
    {
        public Int32 Id { get; set; }
        public String Name { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public String Status { get; set; }
        public bool IsActive { get; set; }
        public Int32 ClassTypeId { get; set; }
        public String ClassTypeName { get; set; }
        public Int32 SemesterId { get; set; }
        public String SemesterName { get; set; }
    }
}
