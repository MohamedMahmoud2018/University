using AutoMapper;
using CORE.DAL;
using CORE.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    class ProfileEvaluationService: IProfileEvaluationService
    {
        IUnitOfWork uow;
        IMapper mapper;
        public ProfileEvaluationService(IUnitOfWork _uow, IMapper _mapper)
        {
            uow = _uow;
            mapper = _mapper;
        }

        public ServiceResponse Add(ProfileEvaluationInput input)
        {
            try
            {
                uow.ProfileEvaluationRepo.Insert(mapper.Map<ProfileEvaluation>(input));
                uow.Save();
                return new ServiceResponse
                {
                    IsError = false,
                    Message = "تمت الإضافة",
                    Data = uow.AcademicDegreeRepo.Get().LastOrDefault().Id,
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

        public ServiceResponse AddCollection(ICollection<ProfileEvaluationInput> input)
        {
            try
            {
                List<ProfileEvaluationInput> ErrorList = new List<ProfileEvaluationInput>();
                List<GradeOutput> grades =mapper.Map<List<GradeOutput>>( uow.GradeRepo.Get());
              //check degree between min and max
                foreach (var profileEval in input)
                {
                    int min = grades.Where(G => G.Degree == profileEval.Grade).Select(G => G.MinValue).FirstOrDefault();
                    int max = grades.Where(G => G.Degree == profileEval.Grade).Select(G => G.MaxValue).FirstOrDefault();

                    if (profileEval.SupKPIDegree<=min || profileEval.SupKPIDegree>max)
                    {
                        ErrorList.Add(profileEval);
                    }
                }
                if (ErrorList.Count > 0)
                {
                    return new ServiceResponse
                    {
                        IsError = true,
                        Message = "الرجاء التأكد من القيم المدخلة",
                        Data=ErrorList,
                        Code = 400
                    };
                }
               //to calc kpi degree
                foreach (var profileEval in input)
                { 
                int kpiWeight= mapper.Map<KPI>(uow.KPIRepo.GetById(profileEval.KPIId)).Wehight;
                    profileEval.KPIDegree = (kpiWeight* profileEval.SupKPIDegree)/100;
                }
                
                    uow.ProfileEvaluationRepo.Insert(mapper.Map<ICollection<ProfileEvaluation>>(input));
                uow.Save();
                return new ServiceResponse
                {
                    IsError = false,
                    Message = "تمت الإضافة",
                    Data = uow.AcademicDegreeRepo.Get().LastOrDefault().Id,
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

        public ServiceResponse Update(ICollection<ProfileEvaluationInput> input)
        {
            try
            {
                List<object> ErrorList = new List<object>();
                List<GradeOutput> grades = mapper.Map<List<GradeOutput>>(uow.GradeRepo.Get());
                //check degree between min and max
                foreach (var profileEval in input)
                {
                    int min = grades.Where(G => G.Degree == profileEval.Grade).Select(G => G.MinValue).FirstOrDefault();
                    int max = grades.Where(G => G.Degree == profileEval.Grade).Select(G => G.MaxValue).FirstOrDefault();

                    if (profileEval.SupKPIDegree <= min || profileEval.SupKPIDegree > max)
                    {
                        ErrorList.Add(new {KPIName=uow.KPIRepo.GetById(profileEval.KPIId).Name,
                            MinValue=min,MaxValue=max });
                    }
                }
                if (ErrorList.Count > 0)
                {
                    return new ServiceResponse
                    {
                        IsError = true,
                        Message = "الرجاء التأكد من القيم المدخلة",
                        Data = ErrorList,
                        Code = 400
                    };
                }
                //to calc kpi degree
                foreach (var profileEval in input)
                {
                    int kpiWeight = mapper.Map<KPI>(uow.KPIRepo.GetById(profileEval.KPIId)).Wehight;
                    profileEval.KPIDegree = (kpiWeight * profileEval.SupKPIDegree) / 100;
                }

               
                uow.ProfileEvaluationRepo.Update(mapper.Map<ICollection<ProfileEvaluation>>(input));
                uow.Save();
                return new ServiceResponse
                {
                    IsError = false,
                    Message = "تم التعديل بنجاح",
                    Data = input,
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
        public ServiceResponse Delete(int Id)
        {
            try
            {
                uow.ProfileEvaluationRepo.Delete(Id);
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

                return new ServiceResponse
                {
                    IsError = true,
                    Message = ex.Message,
                    Code = 500
                };
            }
        }

       

        public ServiceResponse GetOne(int profileId, int KPICategoryId)
        {
            try
            {
                var Result = from k in uow.KPIRepo.Get()
                             join PEval in uow.ProfileEvaluationRepo.Get()
                             on k.Id equals PEval.KPIId
                             join SupK in uow.SupKPIRepo.Get()
                             on k.Id equals SupK.KPIId
                             where PEval.PofileId == profileId && PEval.KPICategoryId == KPICategoryId
                             && PEval.SupKPIId==SupK.Id
                             select new {Id=PEval.Id,
                                 PofileId=PEval.PofileId,
                                 KPICategoryId=PEval.KPICategoryId,
                                 KPIId = k.Id,
                                 KPIName= k.Name,
                                 KPIDegree= PEval.KPIDegree,
                                 SupKPIId=SupK.Id,
                                 SupKPIName=SupK.Name,
                                 SupKPIDegree=PEval.SupKPIDegree,
                                 Grade =SupK.Wehight,
                                 Notes=PEval.Notes };

                if (Result != null)
                {
                    return new ServiceResponse
                    {
                        IsError = false,
                        Message = "data",
                        Data = Result,
                        Code=200
                    };

                }
                return new ServiceResponse
                {
                    IsError = true,
                    Message = "no data",
                    Data = Result,
                    Code=404
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

        public ServiceResponse GetAll()
        {
            try
            {
                var data= mapper.Map<List<ProfileEvaluationOutput>>(uow.ProfileEvaluationRepo.Get());
                if (data != null)
                    return new ServiceResponse
                    {
                        IsError = false,
                        Code = 200,
                        Data = data
                    };

                return new ServiceResponse
                {
                    IsError = true,
                    Message = "هذا العنصر غير موجود",
                    Code = 404,
                    Data = data
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

        

    }
}
