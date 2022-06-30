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
   public class CityService: ICityService
    {
        IUnitOfWork uow;
        IMapper mapper;
        public CityService(IUnitOfWork _uow, IMapper _mapper)
        {
            uow = _uow;
            mapper = _mapper;
        }

        public ServiceResponse Add(CityInput input)
        {
            try
            {
                if (uow.CityRepo.Get().Where(C=>C.CountryId==input.CountryId)
                    .Select(U => U.ArabicName).Contains(input.ArabicName))
                    return new ServiceResponse
                    {
                        IsError = true,
                        Message = "هذا الاسم موجود من قبل",
                        Data = input.ArabicName,
                        Code = 400
                    };
                uow.CityRepo.Insert(mapper.Map<City>(input));
                uow.Save();
                return new ServiceResponse
                {
                    IsError = false,
                    Message = "تمت الإضافة",
                    Data = uow.CityRepo.Get().LastOrDefault().Id,
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

        public ServiceResponse Update(CityInput input)
        {
            try
            {
                if (uow.CityRepo.Get().Where(C => C.CountryId == input.CountryId)
                    .Select(U => U.ArabicName).Contains(input.ArabicName))
                    return new ServiceResponse
                    {
                        IsError = true,
                        Message = "هذا الاسم موجود من قبل",
                        Data = input.ArabicName,
                        Code = 400
                    };
                uow.CityRepo.Update(mapper.Map<City>(input));
                uow.Save();
                return new ServiceResponse
                {
                    IsError = false,
                    Message = "تم التعيل",
                    Data = input.ArabicName,
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
                uow.CityRepo.Delete(Id);
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
        public ServiceResponse GetOne(int Id)
        {
            try
            {
                var data = mapper.Map<CityOutput>(uow.CityRepo.GetById(Id));

                if (data != null)
                    return new ServiceResponse
                    {
                        IsError = false,
                        Code = 200,
                        Data = mapper.Map<CityOutput>(uow.CityRepo.GetById(Id))
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
        public ServiceResponse GetAll()
        {
            try
            {
                var data = mapper.Map<List<CityOutput>>(uow.CityRepo.Get());

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

        public ServiceResponse GetByCountryID(int countryId)
        {
            try
            {
                var data = mapper.Map<List<CityOutput>>(uow.CityRepo.Get(C=>C.CountryId==countryId));

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
                    Message = "لا يوجد مدن متاحة",
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
