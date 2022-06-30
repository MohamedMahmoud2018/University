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
    class KPIService: IKPIService
    {

        IUnitOfWork uow;
        IMapper mapper;

        public KPIService(IUnitOfWork _uow, IMapper _mapper)
        {
            uow = _uow;
            mapper = _mapper;
        }

        public ServiceResponse Add(KPIInput input)
        {
            try
            {
                var KPIList = uow.KPIRepo.Query(kPI => kPI.KPICategoryId == input.KPICategoryId);
                if (KPIList!=null)
                {
                    if (KPIList.Select(KP => KP.Wehight).Sum() + input.Wehight > 100)

                    {
                        //errMsg = "مجموع التقيمات أكبر من 100";
                        return new ServiceResponse
                        {
                            IsError = true,
                            Message = "مجموع التقيمات أكبر من 100",
                            Data = input.Id,
                            Code=400
                        };
                    }
                    if (KPIList.Select(KP => KP.Wehight).Sum() + input.Wehight < 100)

                    {
                        //errMsg = "مجموع التقيمات أكبر من 100";
                        return new ServiceResponse
                        {
                            IsError = true,
                            Message = "مجموع التقيمات أقل من 100",
                            Data = input.Id,
                            Code=400
                        };
                    }
                    if (KPIList.Select(K => K.Name).Contains(input.Name))
                    {
                        // errMsg = "هذا الاسم موجود بالفعل";
                        return new ServiceResponse
                        {
                            IsError = true,
                            Message = "هذا الاسم موجود بالفعل",
                            Data = input.Id,
                            Code=400
                        };
                    }
                    uow.KPIRepo.Insert(mapper.Map<KPI>(input));
                    uow.Save();
                    //errMsg = "Added Sucessfully";
                    return new ServiceResponse
                    {
                        IsError = false,
                        Message = "Added Sucessfully",
                        Data = input.Id,
                        Code=200
                    }; 
                }
                return new ServiceResponse
                {
                    IsError = true,
                    Message = "لا يوجد عنصر مماثل للمعيار الرئيسى",
                   
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

        public ServiceResponse AddCollection(ICollection<KPIInput> input)
        {
            try
            {
                
                var KPIList = uow.KPIRepo.Get(kPI => kPI.KPICategoryId == input.FirstOrDefault().KPICategoryId);
                List<KPIInput> UpdatedList=new List<KPIInput>();
                if (KPIList != null)
                {
                    if (input.Select(KP => KP.Wehight).Sum() > 100)

                    {
                        return new ServiceResponse
                        {
                            IsError = true,
                            Message = "مجموع التقيمات أكبر من 100",
                            Data = input.FirstOrDefault().KPICategoryId,
                            Code=400
                        };
                    }
                    if (input.Select(KP => KP.Wehight).Sum() < 100)

                    {
                        return new ServiceResponse
                        {
                            IsError = true,
                            Message = "مجموع التقيمات أقل من 100",
                            Data = input.FirstOrDefault().KPICategoryId,
                            Code = 400
                        };
                    }
                  
                    if (input.Select(K => K.Name).Distinct().Count() < input.Count)
                        return new ServiceResponse
                        {
                            IsError = true,
                            Message = "أسماء المدخلات  مكررة",
                            Data = input,
                            Code = 400
                        };
                    //if input is exsit update it else insert it
                    //if used in evaluation remark it as deleted
                    foreach (var item in KPIList)
                    {
                        if (input.ToList().Find(K => K.Id == item.Id) == null)
                        {
                            if (uow.ProfileEvaluationRepo.Get(E => E.KPIId == item.Id).FirstOrDefault() != null)
                            {
                                item.IsDeleted = true;
                                uow.KPIRepo.Update(mapper.Map<KPI>(item));
                            }
                            else
                                uow.KPIRepo.Delete(item.Id);
                        }
                        else
                        {
                            var x = input.ToList().Find(K => K.Id == item.Id);
                            UpdatedList.Add(x);
                            input.Remove(x);
                        }
                    }
                    uow.KPIRepo.Update(mapper.Map<ICollection<KPI>>(UpdatedList));
                    uow.KPIRepo.Insert(mapper.Map<ICollection<KPI>>(input));
                   
                    uow.Save();
                    return new ServiceResponse
                    {
                        IsError = false,
                        Message = "تمت الإضافة",
                        Data = input.FirstOrDefault().KPICategoryId,
                        Code = 200
                    }; 
                }
                return new ServiceResponse
                {
                    IsError = true,
                    Message = "لا يوجد عنصر مماثل للمعيار الرئيسى",
                    
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
        public ServiceResponse Update(ICollection<KPIInput> input)
        {
            try
            {
                if(input.Select(K=>K.Name).Distinct().Count()<input.Count)
                    return new ServiceResponse
                    {
                        IsError = true,
                        Message = "أسماء المدخلات  مكررة",
                        Data = input,
                        Code = 400
                    };
               
                if (input != null)
                {
                    #region check weight
                    if (input.Select(KP => KP.Wehight).Sum()  > 100)

                {
                    //errMsg = "مجموع التقيمات أكبر من 100";
                    return new ServiceResponse
                    {
                        IsError = true,
                        Message = "مجموع التقيمات أكبر من 100",
                        Data = input,
                        Code=400
                    };
                }

                if (input.Select(KP => KP.Wehight).Sum() < 100)

                {
                    //errMsg = "مجموع التقيمات أكبر من 100";
                    return new ServiceResponse
                    {
                        IsError = true,
                        Message = "مجموع التقيمات أقل من 100",
                        Data = input,
                        Code = 400
                    };
                }
                #endregion
                
                //    if (KPIList.Select(K => K.Name).Contains(input.Name))
                //{

                //    return new ServiceResponse
                //    {
                //        IsError = true,
                //        Message = "هذا الاسم موجود بالفعل",
                //        Data = input.Name,
                //        Code=400
                //    };
                //}
               
            }
                return new ServiceResponse
                {
                    IsError = true,
                    Message = "لا يوجد عنصر مماثل للمعيار الرئيسى",

                    Code = 404
                };
                var KPIList = uow.KPIRepo.Query(kPI => kPI.KPICategoryId == input.FirstOrDefault().KPICategoryId);
               
                #region Check if this KPI in use
                if (KPIList != null)
                {
                    foreach (var item in KPIList)
                    {
                        if (input.ToList().Find(K => K.Id == item.Id) == null)
                        {
                            if (uow.ProfileEvaluationRepo.Get(E => E.KPIId == item.Id) != null)
                            {
                                item.IsDeleted = true;
                                uow.KPIRepo.Update(item);
                            }
                        }
                        else uow.KPIRepo.Delete(item.Id);
                    }
                } 
                #endregion
                #region if success
                uow.KPIRepo.Update(mapper.Map<List<KPI>>(input));
                uow.Save();
                return new ServiceResponse
                {
                    IsError = false,
                    Message = "updated Sucessfully",
                    Data = input,
                    Code = 200
                };
                #endregion
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
                uow.KPIRepo.Delete(Id);
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
                List<KPIOutput> KPIOutputList =
                     mapper.Map<List<KPIOutput>>(uow.KPIRepo.Get(null,null,K=>K.SupKPI));
                if (KPIOutputList != null)
                {
                    //foreach (var item in KPIOutputList)
                    //{
                    //    item.SupKPIs = mapper.Map<List<SupKPIOutput>>
                    //   (uow.SupKPIRepo.Query(supkPI => supkPI.KPIId == item.Id));
                    //}
                    return new ServiceResponse
                    {
                        IsError = true,
                        Code = 200,
                        Data = KPIOutputList
                    };
                }
                return new ServiceResponse
                {
                    IsError = true,
                    Message = "هذا العنصر غير موجود",
                    Code = 404,
                    Data = KPIOutputList
                };
                //return mapper.Map<List<KPIOutput>>(uow.KPIRepo.Get());
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
                KPIOutput KPIOutputObj = mapper.Map<KPIOutput>
                    (uow.KPIRepo.GetById(Id));
                if (KPIOutputObj!=null)
                {
                    KPIOutputObj.SupKPI = mapper.Map<List<SupKPIOutput>>
                        (uow.SupKPIRepo.Query(supkPI => supkPI.KPIId == Id));
                    return new ServiceResponse
                    {
                        IsError = true,
                        Code = 200,
                        Data = KPIOutputObj
                    };
                }
                return new ServiceResponse
                {
                    IsError = true,
                    Message = "هذا العنصر غير موجود",
                    Code = 404,
                    Data = KPIOutputObj
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
