using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniversityProfUnit.Application.Profiles.Commands.UpdateProfileExperiences
{
    public class UpdateProfileExperiencesDto
    {
		public int ProfileExperienceId { get; set; }
		public string JopName { get; set; }
		public string Employer { get; set; }
		public DateTime FromDate { get; set; }
		public DateTime ToDate { get; set; }
		public string Description { get; set; }
	}
}
