using System;
using System.Collections.Generic;

#nullable disable

namespace Spitali.Models
{
    public partial class Department
    {
        public Department()
        {
            Doctors = new HashSet<Doctor>();
            Spitals = new HashSet<Spital>();
        }

        public int DepartmentId { get; set; }
        public string Departments { get; set; }

        public virtual ICollection<Doctor> Doctors { get; set; }
        public virtual ICollection<Spital> Spitals { get; set; }
    }
}
