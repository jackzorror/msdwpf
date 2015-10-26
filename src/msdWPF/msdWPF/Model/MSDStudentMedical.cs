using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace msdWPF.Model
{
    public class MSDStudentMedical
    {
        public Int32 Id { get; set; }
        public String InsuranceCompany { get; set; }
        public String PolicyNumber { get; set; }
        public String Phone { get; set; }
        public String PediatricianName { get; set; }
        public String EmergencyName { get; set; }
        public String EmergencyPhone { get; set; }
        public String EmergencyPhoneAlt { get; set; }
        public Int32 MSDStudentId { get; set; }
    }
}
