using System;
using System.Collections.Generic;

#nullable disable

namespace Spitali.Models
{
    public partial class Doctor
    {
        public int DoctorId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Number { get; set; }
        public int Departments { get; set; }
        public int Hospital { get; set; }
        public int Salary { get; set; }

        public virtual Department Department { get; set; }
        public virtual HospitalName Hospitals { get; set; }
    }
}
