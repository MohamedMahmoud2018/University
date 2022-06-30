using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniversityProfUnit.Logic.ProfileAgreget.ProfileDtos
{
    public class ProfileCoursesDto
    {
		public int ProfileCourseId { get; set; }
		public string CourseName { get; set; }
		public DateTime FromDate { get; set; }
		public DateTime ToDate { get; set; }
		public string Description { get; set; }
	}
}
