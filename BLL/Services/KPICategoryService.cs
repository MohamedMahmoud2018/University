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
    class KPICategoryService : IKPICategoryService
    {
        IUnitOfWork uow;
        IMapper mapper;

        public KPICategoryService(IUnitOfWork _uow, IMapper _mapper)
        {
            uow = _uow;
            mapper = _mapper;
        }
        public ServiceResponse Add(KPICategoryInput input)
        {
            try
            {
               
                if (uow.KPICategoryRepo.Get().Select(U => U.Name).Contains(input.Name))
                    return new ServiceResponse
                    {
                        IsError = true,
                        Message = "هذا الاسم موجود من قبل",
                        Data = input.Name,
                        Code = 400
                    };
                uow.KPICategoryRepo.Insert(mapper.Map<KPICategory>(input));
                uow.Save();
                return new ServiceResponse
                {
                    IsError = false,
                    Message = "تمت الإضافة",
                    Data = uow.KPICategoryRepo.Get().LastOrDefault().Id,
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

        public ServiceResponse Update(KPICategoryInput input)
        {
            try
            {
               
                if (uow.KPICategoryRepo.Get().Select(U => U.Name).Contains(input.Name))
                    return new ServiceResponse
                    {
                        IsError = true,
                        Message = "هذا الاسم موجود من قبل",
                        Data = input.Name,
                        Code = 400
                    };
                uow.KPICategoryRepo.Update(mapper.Map<KPICategory>(input));
                uow.Save();
                return new ServiceResponse
                {
                    IsError = false,
                    Message = "تم التعديل بنجاح",
                    Data = input.Name,
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
                uow.KPICategoryRepo.Delete(Id);
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

        public ServiceResponse GetAll()
        {
            try
            {
                List<KPICategoryOutput> KPICategoryOutputList =
                     mapper.Map<List<KPICategoryOutput>>(uow.KPICategoryRepo.Get());
                foreach (var item in KPICategoryOutputList)
                {
                    //var x = uow.KPIRepo.Get(kPI => kPI.KPICategoryId == item.Id, null, K => K.SupKPI);
                    item.KPIs =mapper.Map<List<KPIOutput>>
                        (uow.KPIRepo.Get(kPI => kPI.KPICategoryId == item.Id &&
                        kPI.IsDeleted==false, null, K => K.SupKPI));
                        
                   //(uow.KPIRepo.Get(kPI => kPI.KPICategoryId == item.Id,null, kPI => kPI.SupKPI));
                }
                if (KPICategoryOutputList != null)
                    return new ServiceResponse
                    {
                        IsError = false,
                        Code = 200,
                        Data =new { Data = KPICategoryOutputList ,
                        SupKRanges=from Sup in uow.SupKPIRepo.Get()
                                   join grade in uow.GradeRepo.Get()
                                   on Sup.Wehight equals grade.Degree
                                   select new
                                   {
                                       SupKPIId=Sup.Id,
                                       SupKPIName = Sup.Name,
                                       Min = grade.MinValue,
                                       Max = grade.MaxValue
                                   }
                        }
                    };

                return new ServiceResponse
                {
                    IsError = true,
                    Message = "هذا العنصر غير موجود",
                    Code = 404,
                    Data = KPICategoryOutputList
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

        public ServiceResponse GetOne(int Id)
        {
            try
            {
                KPICategoryOutput KPICategoryOutputObj = mapper.Map<KPICategoryOutput>
                    (uow.KPICategoryRepo.GetById(Id));

                KPICategoryOutputObj.KPIs =mapper.Map<List< KPIOutput >>
                    ( uow.KPIRepo.Query(kPI => kPI.KPICategoryId == Id));
                foreach (var kpi in KPICategoryOutputObj.KPIs)
                {
                    kpi.SupKPI = mapper.Map<List<SupKPIOutput>>
                        (uow.SupKPIRepo.Query(supkPI => supkPI.KPIId == kpi.Id));
                }
                if (KPICategoryOutputObj != null)
                    return new ServiceResponse
                    {
                        IsError = false,
                        Code = 200,
                        Data = KPICategoryOutputObj
                    };

                return new ServiceResponse
                {
                    IsError = true,
                    Message = "هذا العنصر غير موجود",
                    Code = 404,
                    Data = KPICategoryOutputObj
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
