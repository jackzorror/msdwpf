using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace msdWPF.Model
{
    public class SchoolClassSummary
    {
        public Int32 Id { get; set; }
        public String Name { get; set; }
        public Int32 ClassTypeId { get; set; }
        public Int32 SemesterId { get; set; }
    }
}
