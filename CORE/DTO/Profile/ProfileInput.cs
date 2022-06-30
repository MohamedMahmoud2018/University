using CORE.DAL;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.DTO
{
    public class ProfileInput
    {
        [Required]
        public int Id { get; set; }
        [    StringLength(10, ErrorMessage = "عدد الارقام 10")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "رقم الهوية يحتوى على أرقام فقط")]

        public string Serial { get; set; }
        [Required(ErrorMessage = "الأسم إلزامى")]
        public string Name { get; set; }

        [Required(ErrorMessage = "البريد الالكترونى إلزامى")]
        [EmailAddress(ErrorMessage = "البريد الالكترونى غير صحيح")]
        public string Email { get; set; }

        [Required(ErrorMessage = "رقم الهاتف إلزامى "), StringLength(13)]
            // StringLength(13, ErrorMessage = "رقم الهاتف 9 أرقام فقط بدون مفتاح البلد")]
        //[RegularExpression(@"^[\/+]([0-9]{12})$", ErrorMessage ="رقم الهاتف لابد أن يكون أرقام فقط")]
        public string PhoneNumber { get; set; }
        public int SpecialtyId { get; set; }
        public string CurrentJop { get; set; }
        public string MainJob { get; set; }
        public int PositionAbbreviationId { get; set; }
        public int GenderId { get; set; }
        public int EmployerId { get; set; }
        public Nullable<int> DiscoveryChannelId { get; set; }
        public string Description { get; set; }
        public bool ProfileState { get; set; }
        //
        public string ProfilePicturePath { get; set; }
        public string CVPath { get; set; }
        //
        public IFormFile ProfilePicture { get; set; }
        public IFormFile CV { get; set; }
        public int AcademicRankId { get; set; }

        public virtual ICollection<ProfileCoursesInput> ProfileCourses { get; set; }
        public virtual ICollection<ProfileExperiencesInput> ProfileExperiences { get; set; }
        public virtual ICollection<ProfileAcademicDegreeInput> ProfileAcademicDegree { get; set; }
        public virtual ICollection<ProfileAwardsInput> ProfileAwards { get; set; }
        public virtual ICollection<ProfileScientificResearchInput> ProfileScientificResearch { get; set; }


    }
}
