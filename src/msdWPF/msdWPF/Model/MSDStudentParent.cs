using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace msdWPF.Model
{
    public class MSDStudentParent
    {
        public Int32 Id { get; set; }
        public String LastName { get; set; }
        public String FirstName { get; set; }
        public String WorkPhone { get; set; }
        public String CellPhone { get; set; }
        public String EmailAddress { get; set; }
        public String Relationship { get; set; }
        public Int32 MSDStudentId { get; set; }
    }
}
