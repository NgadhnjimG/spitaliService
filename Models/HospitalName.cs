using System;
using System.Collections.Generic;

#nullable disable

namespace Spitali.Models
{
    public partial class HospitalName
    {
        public HospitalName()
        {
            Doctors = new HashSet<Doctor>();
            Spitals = new HashSet<Spital>();
        }

        public int HospitalNameId { get; set; }
        public string HospitalName1 { get; set; }
        public int InstitutionType { get; set; }

        public virtual ICollection<Doctor> Doctors { get; set; }
        public virtual ICollection<Spital> Spitals { get; set; }
    }
}
