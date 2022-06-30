using AutoMapper;
using CORE.DAL;
using CORE.DTO;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ProfileServices: IProfileServices
    {
        IUnitOfWork uow;
        IMapper mapper;
        public ProfileServices(IUnitOfWork _uow, IMapper _mapper)
        {
            uow = _uow;
            mapper = _mapper;
        }
        public ServiceResponse Add(ProfileInput input)
        {
            try
            {
                
                var ProfileList = uow.ProfileRepo.Get().Select(P=>new {P.Email,P.Serial,P.PhoneNumber });

                #region Some validation
                //if (input.ProfileExperiences.Count < 3 || input.ProfileCourses.Count < 3)
                //{
                //    return new ServiceResponse
                //    {
                //        IsError = true,
                //        Message = "عدد الدورات التدريبية أو الخبرات أقل من 3",
                //        Data = input.Id
                //    };
                //    //return false;
                //}

                if (ProfileList.Select(P => P.PhoneNumber).Contains(input.PhoneNumber))
                {
                    return new ServiceResponse
                    {
                        IsError = true,
                        Message = "رقم الهاتف مكرر",
                        Data = input.Id,
                        Code=400
                    };
                    //return false;
                }
                if (ProfileList.Select(P => P.Serial).Contains(input.Serial))
                {
                    return new ServiceResponse
                    {
                        IsError = true,
                        Message = "رقم الهوية مكرر",
                        Data = input.Id,
                        Code = 400
                    };
                    //return false;
                }
                if (ProfileList.Select(P => P.Email).Contains(input.Email))
                {
                    return new ServiceResponse
                    {
                        IsError = true,
                        Message = "البريد الالكترونى مكرر",
                        Data = input.Id,
                        Code = 400
                    };
                    //  return false;
                } 
                #endregion
                //if (input.UniversityId == 0)
                //{
                //    if(!uow.UniversityRepo.Get().Select(U=>U.Name).Contains(input.UniversityName))
                //    uow.UniversityRepo.Insert(new University { Id = 0, Name = input.UniversityName });
                //    var x = uow.UniversityRepo.GetLastOrDefault(U=>U.Name==input.UniversityName).Id;
                //}
                var NewProfile = mapper.Map<profile>(input);
                if (input.ProfilePicture != null)
                {
                    if (!ChckFileExtension(input.ProfilePicture, "profilePicture"))
                        return new ServiceResponse
                        {
                            IsError = true,
                            Message = "الرجاء التأكد من صيغة الصورة",
                            Code = 400
                        };
                }
                if (input.CV != null)
                {
                    if (!ChckFileExtension(input.CV, "cv"))
                        return new ServiceResponse
                        {
                            IsError = true,
                            Message = "الرجاء التأكد من صيغة السيرة الذاتية",
                            Code = 400
                        };
                }
                if (input.ProfilePicture != null&&input.ProfilePicture.Length>0)
                    NewProfile.ProfilePicturePath = Upload(input.ProfilePicture, "profilePicture", input.Serial);
                if (input.CV != null&&input.CV.Length>0)
                    NewProfile.CVPath = Upload(input.CV, "cv", input.Serial);

                uow.ProfileRepo.Insert(NewProfile);
                uow.Save();
                var LastId = uow.ProfileRepo.Get().Where(P=>P.Serial==input.Serial&&P.Email==input.Email).LastOrDefault().Id;
                return new ServiceResponse
                {
                    IsError = false,
                    Message = "تمت الإضافة بنجاح",
                    Data = LastId,
                    Code = 200
                };
            }
            catch (Exception ex)
            {
                // insert ex exception in databse Error Log Table
                return new ServiceResponse
                {
                    IsError = true,
                    Message = "لم يتم الإضافة"+"  "+ex.Message+"   "+ex.InnerException.Message,
                    Data = input.Id,
                    Code = 500
                };
            }
        }

        public ServiceResponse Update(ProfileInput input)
        {
            try
            {
                #region Some Validations
                var ProfileList = uow.ProfileRepo.Get()
                    .Select(P => new {P.Id, P.Email, P.Serial, P.PhoneNumber })
                    .Where(P=>P.Id!=input.Id);

                //if (input.ProfileExperiences.Count < 3 || input.ProfileCourses.Count < 3)
                //{
                //    return new ServiceResponse
                //    {
                //        IsError = true,
                //        Message = "عدد الدورات التدريبية أو الخبرات أقل من 3",
                //        Data = input.Id
                //    };
                //    //return false;
                //}

                if (ProfileList.Select(P => P.PhoneNumber).Contains(input.PhoneNumber))
                {
                    return new ServiceResponse
                    {
                        IsError = true,
                        Message = "رقم الهاتف مكرر",
                        Data = input.Id,
                        Code = 400
                    };
                    //return false;
                }
                if (ProfileList.Select(P => P.Serial).Contains(input.Serial))
                {
                    return new ServiceResponse
                    {
                        IsError = true,
                        Message = "رقم الهوية مكرر",
                        Data = input.Id,
                        Code = 400
                    };
                    //return false;
                }
                if (ProfileList.Select(P => P.Email).Contains(input.Email))
                {
                    return new ServiceResponse
                    {
                        IsError = true,
                        Message = "البريد الالكترونى مكرر",
                        Data = input.Id,
                        Code = 400
                    };
                    //  return false;
                }

                #endregion

                #region Check if courses not exist in input  to delete from db
               
                var DBProfileCourse = uow.ProfileCoursesRepo.Get(P => P.ProfileId == input.Id);
                if (input.ProfileCourses != null) 
                    foreach (var profCours in DBProfileCourse)
                {
                    if (input.ProfileCourses.ToList().Find(K => K.Id == profCours.Id) == null)
                    {
                        uow.ProfileCoursesRepo.Delete(profCours.Id);
                    }

                }
                else if (DBProfileCourse != null)
                    uow.ProfileCoursesRepo.DeleteRange(DBProfileCourse.ToList());

                var DBProfileExper = uow.ProfileExperiencesRepo.Get(P => P.ProfileId == input.Id);
                if (input.ProfileExperiences != null)
                    foreach (var profexp in DBProfileExper)
                {
                    if (input.ProfileExperiences.ToList().Find(K => K.Id == profexp.Id) == null)
                    {
                        uow.ProfileExperiencesRepo.Delete(profexp.Id);
                    }
                }
                else if(DBProfileExper!=null)
                    uow.ProfileExperiencesRepo.DeleteRange(DBProfileExper.ToList());

                var DBProfileAcad = uow.ProfileAcademicDegreeRepo.Get(P => P.ProfileId == input.Id);
                if (input.ProfileAcademicDegree != null)
                    foreach (var profAcd in DBProfileAcad)
                {
                    if (input.ProfileAcademicDegree.ToList().Find(K => K.Id == profAcd.Id) == null)
                    {
                        uow.ProfileAcademicDegreeRepo.Delete(profAcd.Id);
                    }
                }
                else if (DBProfileAcad != null)
                    uow.ProfileAcademicDegreeRepo.DeleteRange(DBProfileAcad.ToList());


                var DBProfileAwards = uow.ProfileAwardsRepo.Get(P => P.ProfileId == input.Id);
                if (input.ProfileAwards != null)
                    foreach (var profAward in DBProfileAwards)
                {
                    if (input.ProfileAwards.ToList().Find(K => K.Id == profAward.Id) == null)
                    {
                        uow.ProfileAwardsRepo.Delete(profAward.Id);
                    }
                }
                else if (DBProfileAwards != null)
                    uow.ProfileAwardsRepo.DeleteRange(DBProfileAwards.ToList());

                var DBProfileresearches = uow.ProfileScientificResearchRepo.Get(P => P.ProfileId == input.Id);
                if (input.ProfileScientificResearch != null)
                    foreach (var profresearche in DBProfileresearches)
                    {
                        if (input.ProfileScientificResearch.ToList().Find(K => K.Id == profresearche.Id) == null)
                        {
                            uow.ProfileScientificResearchRepo.Delete(profresearche.Id);
                        }
                    }
                else if (DBProfileresearches != null)
                    uow.ProfileScientificResearchRepo.DeleteRange(DBProfileresearches.ToList());
                    #endregion

                    #region check image and cv are sent againe
                var NewProfile = mapper.Map<profile>(input);

                if (input.ProfilePicture != null)
                {
                    if (!ChckFileExtension(input.ProfilePicture, "profilePicture"))
                        return new ServiceResponse
                        {
                            IsError = true,
                            Message = "الرجاء التأكد من صيغة الصورة",
                            Code = 400
                        };
                    NewProfile.ProfilePicturePath = Upload(input.ProfilePicture, "profilePicture", input.Serial, input.ProfilePicturePath);
                }
                if (input.CV != null)
                {
                    if (!ChckFileExtension(input.CV, "cv"))
                        return new ServiceResponse
                        {
                            IsError = true,
                            Message = "الرجاء التأكد من صيغة السيرة الذاتية",
                            Code = 400
                        };
                    NewProfile.CVPath = Upload(input.CV, "cv", input.Serial, input.CVPath);
                }
                #endregion
                uow.ProfileRepo.Update(NewProfile);
               // uow.ProfileRepo.Update(mapper.Map<profile>(input));
                uow.Save();
                return new ServiceResponse
                {
                    IsError = false,
                    Message = "تم التعديل",
                    Data = input.Id,
                    Code = 200
                };
            }
            catch (Exception ex)
            {
                // insert ex exception in databse Error Log Table
                return new ServiceResponse
                {
                    IsError = true,
                    Message = "خطأ فى التعديل"+"    "+ex.Message,
                    Data = input.Id,
                    Code = 500
                };
            }
        }

        public ServiceResponse Delete(int Id)
        {
            try
            {
                var DBProfileCourse = uow.ProfileCoursesRepo.Get(P => P.ProfileId == Id);
                uow.ProfileCoursesRepo.DeleteRange(DBProfileCourse.ToList());
                var DBProfileExper = uow.ProfileExperiencesRepo.Get(P => P.ProfileId == Id);
                uow.ProfileExperiencesRepo.DeleteRange(DBProfileExper.ToList());
                var DBProfileAcad = uow.ProfileAcademicDegreeRepo.Get(P => P.ProfileId == Id);
                uow.ProfileAcademicDegreeRepo.DeleteRange(DBProfileAcad.ToList());
                var DBProfileAwards = uow.ProfileAwardsRepo.Get(P => P.ProfileId == Id);
                uow.ProfileAwardsRepo.DeleteRange(DBProfileAwards.ToList());
                ////////////may be delete partcipants
                var DBProfileResearch = uow.ProfileScientificResearchRepo.Get(P => P.ProfileId == Id);
                uow.ProfileScientificResearchRepo.DeleteRange(DBProfileResearch.ToList());

                uow.ProfileRepo.Delete(Id);
                uow.Save();
                return new ServiceResponse
                {
                    IsError = false,
                    Message = "تم الحذف",
                    Code = 200
                };
            }
            catch (Exception ex)
            {
                // insert ex exception in databse Error Log Table
                return new ServiceResponse
                {
                    IsError = true,
                    Message =  ex.Message,
                    Code = 500
                };
            }
        }
        public ServiceResponse GetOne(int Id)
        {
            try
            {
                //GetEvaluation(Id);
                //get the profile then fill name of some data that we have its id
                //and all collections

                var OriginProfile = uow.ProfileRepo.Get(P=>P.Id==Id,null,P=>P.Specialty,
                    P=>P.Gender,P=>P.DiscoveryChannel,P=>P.PositionAbbreviation,
                    P=>P.AcademicRank,P=>P.Employer).FirstOrDefault();

                OriginProfile.Employer.City = uow.CityRepo.Get(C => C.Id == OriginProfile.Employer.CityId
                , null, C => C.Country).FirstOrDefault();


                if (OriginProfile == null)
                    return new ServiceResponse
                    {
                        IsError = true,
                        Message = "هذا العنصر غير موجود",
                        Code = 404,
                        Data = Id
                    };
                ProfileOutput OutProf = mapper.Map<ProfileOutput>(OriginProfile);

                //Fille Collections
                #region Profile Academic Degree
                OutProf.ProfileAcademicDegree = mapper.Map<ICollection<ProfileAcademicDegreeOutput>>
                            (uow.ProfileAcademicDegreeRepo.Get(C => C.ProfileId == Id, null,
                            D => D.University, D => D.Specialty, D => D.AcademicDegree));

                if (OutProf.ProfileAcademicDegree != null)
                    foreach (var AcademicDegree in OutProf.ProfileAcademicDegree)
                    {
                        AcademicDegree.University.City = uow.CityRepo
                            .Get(C => C.Id == AcademicDegree.University.CityId, null,
                            C => C.Country).FirstOrDefault();
                    }

                #endregion

                OutProf.ProfileAwards = mapper.Map<List<ProfileAwardsOutput>>
                    (uow.ProfileAwardsRepo.Get(A => A.ProfileId == Id));

                #region ProfileCourses
                OutProf.ProfileCourses = mapper.Map<List<ProfileCoursesOutput>>
                           (uow.ProfileCoursesRepo.Get(C => C.ProfileId == Id, null,
                           C => C.CertificateType));
                if (OutProf.ProfileCourses != null)
                    foreach (var Course in OutProf.ProfileCourses)
                    {
                        Course.City = uow.CityRepo
                         .Get(C => C.Id == Course.CityId, null, C => C.Country).FirstOrDefault();
                    }
                #endregion

                #region ProfileExperiences
                OutProf.ProfileExperiences = mapper.Map<List<ProfileExperiencesOutput>>
                            (uow.ProfileExperiencesRepo.Get(C => C.ProfileId == Id, null,
                            E => E.Employer, c => c.SpecialityRelation));
                if (OutProf.ProfileExperiences != null)
                    foreach (var Exper in OutProf.ProfileExperiences)
                    {
                        Exper.Employer.City = uow.CityRepo
                            .Get(C => C.Id == Exper.Employer.CityId, null, C => C.Country)
                            .FirstOrDefault();
                    }
                #endregion

                OutProf.ProfileScientificResearch = mapper.Map<List<ProfileScientificResearchOutput>>
                    (uow.ProfileScientificResearchRepo.Get(A => A.ProfileId == Id));

              

                return new ServiceResponse
                {
                    IsError = false,
                    Code = 200,
                    Data = OutProf
                };
            }
            catch (Exception ex)
            {
                // insert ex exception in databse Error Log Table
                return new ServiceResponse
                {
                    IsError = true,
                    Message = ex.Message,
                    Code = 500
                };
            }
        }
        public ServiceResponse GetAll(int page,string searchKey=null)
        {
            try
            {
                if(page<0)
                    return new ServiceResponse
                    {
                        IsError = true,
                        Message = "رقم الصفحة غير صحيح",
                        Code = 400
                    };
                int skip = (page-1) * 9;
                var AllProfiles= searchKey!=null?
                    uow.ProfileRepo.Get(P=>P.Name.Contains(searchKey), null, P => P.Specialty,
                    P => P.Gender, P => P.DiscoveryChannel, P => P.PositionAbbreviation,
                    P => P.AcademicRank, P => P.Employer)
                    .OrderByDescending(P => P.Id).Skip(skip).Take(9).ToList() :

                    uow.ProfileRepo.Get(null, null, P => P.Specialty,
                    P => P.Gender, P => P.DiscoveryChannel, P => P.PositionAbbreviation,
                    P => P.AcademicRank, P => P.Employer).OrderByDescending(P => P.Id).Skip(skip).Take(9).ToList();
               
                if (AllProfiles == null)
                    return new ServiceResponse
                    {
                        IsError = true,
                        Message = "هذا العنصر غير موجود",
                        Code = 404
                    };
                decimal PagesCount = 0;
               // if (searchKey!=null)
                 PagesCount = searchKey != null ? Math.Ceiling((decimal) (uow.ProfileRepo.Get()
                    .Count(P => P.Name.Contains(searchKey)))/9)
                        : Math.Ceiling((decimal)(uow.ProfileRepo.Get().Count()) / 9);

                List<ProfileOutput> OutputProfiles = mapper.Map<List<ProfileOutput>>
                    (AllProfiles);
               
                var data= OutputProfiles.Select(P=>new { 
                    Id=P.Id, 
                    Serial=P.Serial,
                    Name=P.Name,
                    Gender = P.Gender,
                    MainJob =P.MainJob,
                    PositionAbbreviation=P.PositionAbbreviation,
                    Specialty=P.Specialty,
                    ProfilePicturePath=P.ProfilePicturePath,
                    Employeer=P.Employer
                });
                return new ServiceResponse
                {
                    IsError = false,
                    Code = 200,
                    Data =new { 
                        PagesCount= PagesCount,
                        Data = data
                    } 
                };
            }
            catch (Exception ex)
            {
                // insert ex exception in databse Error Log Table
                return new ServiceResponse
                {
                    IsError = true,
                    Message = ex.Message,
                    Code = 500
                };
            }
        }

        #region testfillter
       public ServiceResponse FilterKeys()
        {
            try
            {
                return new ServiceResponse
                {
                    IsError = false,
                   Data=Enums.SearchKeys,
                    Code = 200
                };
            }
            catch (Exception ex)
            {

                return new ServiceResponse
                {
                    IsError = true,
                    Message = ex.Message,
                    Code = 500
                };
            }
        }

        public ServiceResponse FilterData(int page,string SearchKey, string SearchValue)
        {
            try
            {
                decimal PagesCount = 0;
                // if (searchKey!=null)
                
                       
                if (page < 0)
                    return new ServiceResponse
                    {
                        IsError = true,
                        Message = "رقم الصفحة غير صحيح",
                        Code = 400
                    };
                int skip = (page - 1) * 9;
                var FilterResult = new List<profile>();
                switch (SearchKey)
                {
                    case "Speciality":
                        FilterResult = uow.ProfileRepo.Get(P => P.Specialty.Name.
                        Contains(SearchValue), null, P => P.Specialty,
                    P => P.Gender, P => P.DiscoveryChannel, P => P.PositionAbbreviation,
                    P => P.AcademicRank, P => P.Employer).Skip(page).Take(9).ToList();

                        PagesCount = Math.Ceiling((decimal)(uow.ProfileRepo.Get(P => P.Specialty
                        .Name.Contains(SearchValue)).Count()) / 9);
                        break;

                    case "Gender":
                        FilterResult = uow.ProfileRepo.Get(P => P.Gender.Name.
                        Contains(SearchValue), null, P => P.Specialty,
                    P => P.Gender, P => P.DiscoveryChannel, P => P.PositionAbbreviation,
                    P => P.AcademicRank, P => P.Employer).Skip(page).Take(9).ToList();

                        PagesCount = Math.Ceiling((decimal)(uow.ProfileRepo.Get(P => P.Gender
                        .Name.Contains(SearchValue)).Count()) / 9);
                        break;

                    case "PositionAbbreviation":
                        FilterResult = uow.ProfileRepo.Get(P => P.PositionAbbreviation.Name.
                        Contains(SearchValue), null, P => P.Specialty,
                    P => P.Gender, P => P.DiscoveryChannel, P => P.PositionAbbreviation,
                    P => P.AcademicRank, P => P.Employer).Skip(page).Take(9).ToList();

                        PagesCount = Math.Ceiling((decimal)(uow.ProfileRepo.Get(P => P.
                        PositionAbbreviation.Name.Contains(SearchValue)).Count()) / 9);
                        break;

                    case "University":
                        FilterResult = uow.ProfileRepo.Get(P => P.Employer.Name.
                        Contains(SearchValue), null, P => P.Specialty,
                    P => P.Gender, P => P.DiscoveryChannel, P => P.PositionAbbreviation,
                    P => P.AcademicRank, P => P.Employer).Skip(page).Take(9).ToList();

                        PagesCount = Math.Ceiling((decimal)(uow.ProfileRepo.Get(P => P.
                        Employer.Name.Contains(SearchValue)).Count()) / 9);
                        break;

                    case "MainJob":
                        FilterResult = uow.ProfileRepo.Get(P => P.MainJob.
                        Contains(SearchValue), null, P => P.Specialty,
                    P => P.Gender, P => P.DiscoveryChannel, P => P.PositionAbbreviation,
                    P => P.AcademicRank, P => P.Employer).Skip(page).Take(9).ToList();

                        PagesCount = Math.Ceiling((decimal)(uow.ProfileRepo.Get(P => P.
                        MainJob.Contains(SearchValue)).Count()) / 9);
                        break;

                    case "DiscoveryChannel":
                        FilterResult = uow.ProfileRepo.Get(P => P.DiscoveryChannel.Name.
                        Contains(SearchValue), null, P => P.Specialty,
                    P => P.Gender, P => P.DiscoveryChannel, P => P.PositionAbbreviation,
                    P => P.AcademicRank, P => P.Employer).Skip(page).Take(9).ToList();

                        PagesCount = Math.Ceiling((decimal)(uow.ProfileRepo.Get(P => P.
                        DiscoveryChannel.Name.Contains(SearchValue)).Count()) / 9);
                        break;

                    case "AcademicRank":
                        FilterResult = uow.ProfileRepo.Get(P => P.AcademicRank.Name.
                        Contains(SearchValue), null, P => P.Specialty,
                    P => P.Gender, P => P.DiscoveryChannel, P => P.PositionAbbreviation,
                    P => P.AcademicRank, P => P.Employer).Skip(page).Take(9).ToList();

                        PagesCount = Math.Ceiling((decimal)(uow.ProfileRepo.Get(P => P.
                        AcademicRank.Name.Contains(SearchValue)).Count()) / 9);
                        break;


                    defaulte: FilterResult = null;
                }
                if(FilterResult!=null)
                return new ServiceResponse
                {
                    IsError = false,
                    Data =new
                    {
                        PageCount= PagesCount,
                       Data=mapper.Map<List<ProfileOutput>>( FilterResult),
                       //.Select(P => new
                       // {
                       //     Id = P.Id,
                       //     Serial = P.Serial,
                       //     Name = P.Name,
                       //     Gender = P.Gender,
                       //     MainJob = P.MainJob,
                       //     PositionAbbreviation = P.PositionAbbreviation,
                       //     Specialty = P.Specialty,
                       //     ProfilePicturePath = P.ProfilePicturePath,
                       //     Employeer = P.Employer
                       // })
                    },
                    Message = "",
                    Code = 200
                };
                return new ServiceResponse
                {
                    IsError = true,
                    Message = "لا يوجد بيانات",
                    Code = 404
                };
            }
            catch (Exception ex)
            {

                return new ServiceResponse
                {
                    IsError = true,
                    Message = ex.Message,
                    Code = 500
                };
            }
        }
        #endregion

        public ServiceResponse GetEvaluation(int ProfileId)
        {
            try
            {
                
                var result = from Eval in uow.ProfileEvaluationRepo
                             .Get(P => P.PofileId == ProfileId, null, E => E.KPICategory)
                             join KPI in uow.KPIRepo.Get(K => K.IsDeleted == false)
                             on Eval.KPIId equals KPI.Id
                             group Eval by Eval.KPICategoryId into res
                             select new
                             {
                                 CategoryDegree = res.Select(P => P.KPIDegree).Sum(),
                                 Categ = res.Select(P => P.KPICategory.Name).FirstOrDefault(),
                                 CategoryWeight = res.Select(P => P.KPICategory.Wehight).FirstOrDefault()
                             };
               
                if(result.ToList().Count>0)
               
                return new ServiceResponse
                {
                    IsError = false,
                    Data=result.ToList(),
                    Message = "",
                    Code = 200
                };
                return new ServiceResponse
                {
                    IsError = true,
                    Data = result.ToList(),
                    Message = "لم يتم تقييم هذا المرشح",
                    Code = 400
                };
            }
            catch (Exception ex)
            {

                return new ServiceResponse
                {
                    IsError = true,
                    Message = ex.Message,
                    Code = 500
                };
            }
        }
        string Upload(IFormFile file, string fileType,string serial,string path=null)
        {
            try
            {
                
                var folderName = "";
                if (fileType.ToLower().Equals("cv"))
                    folderName = Path.Combine("wwwroot", "CV");
                if (fileType.ToLower().Equals("profilepicture"))
                    folderName = Path.Combine("wwwroot", "ProfilePictures");

                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                
                if (file.Length > 0)
                {
                    var fileName = serial+ ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);
                    //check if the file is already exists delete it
                    if (!string.IsNullOrEmpty(path))
                    {
                        if (File.Exists(Path.Combine(Directory.GetCurrentDirectory(), path)))
                            File.Delete(Path.Combine(Directory.GetCurrentDirectory(), path));
                    }
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    return dbPath;
                }
                else
                {
                    return "error";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
                // StatusCode(500, $"Internal server error: {ex}");
            }
        }

   
        bool ChckFileExtension(IFormFile postedFile, string fileType)
        {
            var postedFileExtension = Path.GetExtension(postedFile.FileName);
            
                if (fileType.ToLower().Equals("profilepicture")) 
            { 
                    if (!string.Equals(postedFileExtension, ".jpg", StringComparison.OrdinalIgnoreCase)
                    && !string.Equals(postedFileExtension, ".png", StringComparison.OrdinalIgnoreCase)
                    && !string.Equals(postedFileExtension, ".gif", StringComparison.OrdinalIgnoreCase)
                    && !string.Equals(postedFileExtension, ".jpeg", StringComparison.OrdinalIgnoreCase))
                    {
                        return false;
                    }
                return true;
            }
                else if (fileType.ToLower().Equals("cv"))
            {
                if (!string.Equals(postedFileExtension, ".pdf", StringComparison.OrdinalIgnoreCase))
                {
                    return false;
                }
                return true;
            }
            return false;
        }
    }
}
