using System;
using System.Collections.Generic;

#nullable disable

namespace Spitali.Models
{
    public partial class Spital
    {
        public int SpitalId { get; set; }
        public int HospitalName { get; set; }
        public string Address { get; set; }
        public int Departments { get; set; }
        public int EmployeeNumber { get; set; }

        public virtual Department Department { get; set; }
        public virtual HospitalName HospitalNames { get; set; }
    }
}
