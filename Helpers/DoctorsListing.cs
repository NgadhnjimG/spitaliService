using Spitali.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spitali.Helpers
{
    public class DoctorsListing
    {
        public Doctor Doctor { get; set; }
        public DoctorReviewDataObject Reviews { get; set; }
    }
}
