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
    class UniversityServices : IUniversityServices
    {
        IUnitOfWork uow;
        IMapper mapper;
        public UniversityServices(IUnitOfWork _uow, IMapper _mapper)
        {
            uow = _uow;
            mapper = _mapper;
        }
        public ServiceResponse Add(UniversityInput input)
        {
            try
            {
                if (uow.UniversityRepo.Get().Select(U => U.Name).Contains(input.Name))
                    return new ServiceResponse
                    {
                        IsError = true,
                        Message = "هذا الاسم موجود من قبل",
                        Data = input.Name,
                        Code = 400
                    };
                uow.UniversityRepo.Insert(mapper.Map<University>(input));
                uow.Save();
                var id = uow.UniversityRepo.Get().LastOrDefault().Id;
                return new ServiceResponse
                {
                    IsError = false,
                    Message = "تمت الإضافة",
                    Data =id,
                    Code = 200
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
        public ServiceResponse Update(UniversityInput input)
        {
            try
            {
                if (uow.UniversityRepo.Get(U=>U.CityId==input.CityId).Select(U => U.Name).Contains(input.Name))
                    return new ServiceResponse
                    {
                        IsError = true,
                        Message = "هذا الاسم موجود من قبل",
                        Data = input.Name,
                        Code = 400
                    };
                uow.UniversityRepo.Update(mapper.Map<University>(input));
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
                // insert ex exception in databse Error Log Table
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
                uow.UniversityRepo.Delete(Id);
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
                    Message = ex.Message,
                    Code = 500
                };
            }

        }

        public ServiceResponse GetAll()
        {
            try
            {
                //return mapper.Map<List<UniversityOutput>>(uow.UniversityRepo.Get());
               // var x = uow.UniversityRepo.Get().ToList();
                var data= mapper.Map<List<UniversityOutput>>(uow.UniversityRepo.Get().ToList());
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
                // insert ex exception in databse Error Log Table
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
                var data = mapper.Map<UniversityOutput>(uow.UniversityRepo.GetById(Id));
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
                // insert ex exception in databse Error Log Table
                return new ServiceResponse
                {
                    IsError = true,
                    Message = ex.Message,
                    Code = 500
                };
            }
        }

        public ServiceResponse GetByCityID(int cityId)
        {
            try
            {
                var data = mapper.Map<List<UniversityOutput>>(uow.UniversityRepo.Get(C => C.CityId == cityId));

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
                    Message = "لا يوجد جامعات متاحة",
                    Code = 404,
                    Data = data
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
    }
}
