using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniversityProfUnit.Application.Profiles.Commands.UpdateProfileCourses
{
    public class UpdateProfileCoursesDto
    {
		public int ProfileCourseId { get; set; }
		public string CourseName { get; set; }
		public DateTime FromDate { get; set; }
		public DateTime ToDate { get; set; }
		public string Description { get; set; }
	}
}
