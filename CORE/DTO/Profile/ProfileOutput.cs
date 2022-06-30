using CORE.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.DTO
{
    public class ProfileOutput
    {
        public int Id { get; set; }
        public string Serial { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public virtual Specialty Specialty { get; set; }
        public string CurrentJop { get; set; }
        public string MainJob { get; set; }
        public virtual Gender Gender { get; set; }
        public virtual DiscoveryChannels DiscoveryChannel { get; set; }
        public string Description { get; set; }
        public bool ProfileState { get; set; }
        public virtual PositionAbbreviation PositionAbbreviation { get; set; }
        public string ProfilePicturePath { get; set; }
        public string CVPath { get; set; }
        public virtual University Employer { get; set; }
        public virtual AcademicRank AcademicRank { get; set; }

        public virtual ICollection<ProfileCoursesOutput> ProfileCourses { get; set; }
        public virtual ICollection<ProfileExperiencesOutput> ProfileExperiences { get; set; }
        public virtual ICollection<ProfileAcademicDegreeOutput> ProfileAcademicDegree { get; set; }
        public virtual ICollection<ProfileAwardsOutput> ProfileAwards { get; set; }
        public virtual ICollection<ProfileScientificResearchOutput> ProfileScientificResearch { get; set; }

    }
}
