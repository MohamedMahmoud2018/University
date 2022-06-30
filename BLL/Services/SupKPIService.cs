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
    public class SupKPIService : ISupKPIService

    {
        IUnitOfWork uow;
        IMapper mapper;

        public SupKPIService(IUnitOfWork _uow, IMapper _mapper)
        {
            uow = _uow;
            mapper = _mapper;
        }

        public ServiceResponse Add(SupKPIInput input)
        {
            try
            {
                var SupKPIList = uow.SupKPIRepo.
                    Query(SupKPI => SupKPI.KPICategoryId == input.KPICategoryId &&  SupKPI.KPIId == input.KPIId);

                if (SupKPIList!=null)
                {
                    if (SupKPIList.Select(KP => KP.Wehight).Sum() + input.Wehight > 100)

                    {
                        //errMsg = "مجموع التقيمات أكبر من 100";
                        return new ServiceResponse
                        {
                            IsError = true,
                            Message = "مجموع التقيمات أكبر من 100",
                            Data = input.Id,
                            Code = 400
                        };
                    }
                    if (SupKPIList.Select(K => K.Name).Contains(input.Name))
                    {
                        // errMsg = "هذا الاسم موجود بالفعل";
                        return new ServiceResponse
                        {
                            IsError = true,
                            Message = "هذا الاسم موجود بالفعل",
                            Data = input.Id,
                            Code = 400
                        };
                    }
                    uow.SupKPIRepo.Insert(mapper.Map<SupKPI>(input));
                    uow.Save();
                    //errMsg = "Added Sucessfully";
                    return new ServiceResponse
                    {
                        IsError = false,
                        Message = "Added Sucessfully",
                        Data = input.Id,
                        Code = 200
                    };
                }
                return new ServiceResponse
                {
                    IsError = true,
                    Message = "عنصر التقييم الرئيسى غير موجود",
                    Code = 404,
                    Data = input.KPIId
                };
                
            }
            catch (Exception ex)
            {

                return new ServiceResponse
                {
                    IsError = true,
                    Message = ex.Message,
                    Data = input.Id,
                    Code=500
                };
            }

        }
        public ServiceResponse AddCollection(ICollection<SupKPIInput> input)
        {
            try
            {
                //  int sum=input.Select(K=>K.)
                //int s1 = KPIList.Select(KP => KP.Wehight).Sum();
                //int s2 = input.Select(KP => KP.Wehight).Sum();
                var SupKPIList = uow.SupKPIRepo.
                       Query(SupKPI => SupKPI.KPICategoryId == input.FirstOrDefault().KPICategoryId 
                       && SupKPI.KPIId == input.FirstOrDefault().KPIId);
               
                List<SupKPIInput> ReturnList = new List<SupKPIInput>();
                var Values = input.Select(i => i.Wehight);
               

                //check if items count !=5
                if (input.Count != 5)
                {
                    return new ServiceResponse
                    {
                        IsError = true,
                        Message = "عدد العناصر لا يساوي 5",
                        Data = ReturnList,
                        Code=400
                    };
                }
                //check if all values is not repeated
                if (Values.Count() != Values.Distinct().Count())
                {
                    return new ServiceResponse
                    {
                        IsError = true,
                        Message = "الأوزان مكررة",
                        Data = ReturnList,
                        Code = 400
                    };
                }
                //check if names is repeated 
                foreach (var item in input)
                {
                    //.Contains(item.Name) && !KPIList.Select(K => K.Id).Contains(item.Id)
                    if (input.Select(K => K.Name).Where(k => k == item.Name).Count() > 1
                        ||item.Wehight<0|| item.Wehight >5)
                    {
                        ReturnList.Add(item);
                    }
                }
                if (ReturnList.Count > 0)
                    return new ServiceResponse
                    {
                        IsError = false,
                        Message = "أسماء المدخلات التالية مكررة أو الوزن غير صحيح",
                        Data = ReturnList,
                        Code = 400
                    };

                //if item is exist update else add
                //var supToAdd = input.Select(x=>new { x}).Except(mapper.Map<List<SupKPIInput>>(SupKPIList).Select(x=>new { x})).ToList();
                //var supToUpdate = input.Intersect(mapper.Map<List<SupKPIInput>>(SupKPIList)).ToList();
                foreach (var item in input)
                {
                    if (SupKPIList.Select(K => K.Id).Contains(item.Id))
                    {
                        uow.SupKPIRepo.Update(mapper.Map<SupKPI>(item));

                    }
                    else
                    {
                        uow.SupKPIRepo.Insert(mapper.Map<SupKPI>(item));
                    }
                }
                // uow.KPIRepo.Insert(mapper.Map<ICollection<KPI>>(input));
                //uow.SupKPIRepo.Update(mapper.Map<List<SupKPI>>(supToUpdate));
                //uow.SupKPIRepo.Insert(mapper.Map<List<SupKPI>>(supToAdd));
                uow.Save();
                return new ServiceResponse
                {
                    IsError = false,
                    Message = "Added Sucessfully",
                    Data = input.FirstOrDefault().KPIId,
                    Code = 200
                };
            }
            catch (Exception ex)
            {

                return new ServiceResponse
                {
                    IsError = true,
                    Message = ex.Message,
                    Data = input.FirstOrDefault().KPIId,
                    Code=500
                };
            }
        }
        public ServiceResponse Update(SupKPIInput input)
        {
            try
            {
                var SupKPIList = uow.SupKPIRepo.
                    Query(SupKPI => SupKPI.KPICategoryId == input.KPICategoryId 
                    && SupKPI.KPIId == input.KPIId );
               
                if (SupKPIList!=null)
                {
                    if ( SupKPIList.Where(K => K.Id != input.Id).Select(K => K.Name)
                        .Contains(input.Name))
                    {
                        return new ServiceResponse
                        {
                            IsError = true,
                            Message = "هذا الاسم موجود بالفعل",
                            Data = input.Id,
                            Code = 400
                        };
                    }
                    uow.SupKPIRepo.Update(mapper.Map<SupKPI>(input));
                    uow.Save();
                    return new ServiceResponse
                    {
                        IsError = false,
                        Message = "تم التعديل",
                        Data = input.Id,
                        Code = 200
                    }; 
                }
                return new ServiceResponse
                {
                    IsError = true,
                    Message = "هذا العنصر غير موجود",
                    Code = 404,
                    Data = input.Name
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
                uow.SupKPIRepo.Delete(Id);
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
                var data = mapper.Map<List<SupKPIOutput>>(uow.SupKPIRepo.Get());
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

        public ServiceResponse GetOne(int Id)
        {
            try
            {
                 var data =  mapper.Map<SupKPIOutput>(uow.SupKPIRepo.GetById(Id));
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
