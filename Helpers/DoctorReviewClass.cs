using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spitali
{
	public class DoctorReviewDataObject
	{
		public int DoctorId { get; set; }
		public double StarReview { get; set; } =
			0.0;
		public List<string> Comments { get; set; } = new List<string>() { { "" } };
	}
}
