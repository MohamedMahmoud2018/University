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
    public class SpecialtyService : ISpecialtyService
    {
        IUnitOfWork uow;
        IMapper mapper;
        public SpecialtyService(IUnitOfWork _uow, IMapper _mapper)
        {
            uow = _uow;
            mapper = _mapper;
        }
        public ServiceResponse Add(SpecialtyInput input)
        {
            try
            {
                if (uow.SpecialtyRepo.Get().Select(U => U.Name).Contains(input.Name))
                    return new ServiceResponse
                    {
                        IsError = true,
                        Message = "هذا الاسم موجود من قبل",
                        Data = input.Name,
                        Code=400
                    };

                uow.SpecialtyRepo.Insert(mapper.Map<Specialty>(input));
                uow.Save();
                return new ServiceResponse
                {
                    IsError = true,
                    Message = "تمت الإضافة",
                    Data = uow.SpecialtyRepo.Get().LastOrDefault().Id,
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

        public ServiceResponse Update(SpecialtyInput input)
        {
            try
            {
                if (uow.SpecialtyRepo.Get().Select(U => U.Name).Contains(input.Name))
                    return new ServiceResponse
                    {
                        IsError = true,
                        Message = "هذا الاسم موجود من قبل",
                        Data = input.Name,
                        Code = 400
                    };

                uow.SpecialtyRepo.Update(mapper.Map<Specialty>(input));
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
                uow.SpecialtyRepo.Delete(Id);
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
                var data= mapper.Map<SpecialtyOutput>(uow.SpecialtyRepo.GetById(Id));
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
        public ServiceResponse GetAll(bool IsLookup=false)
        {
            try
            {
                var data = mapper.Map<List<SpecialtyOutput>>(uow.SpecialtyRepo.Get());
                  if (data != null)
                    return new ServiceResponse
                    {
                        IsError = false,
                        Code = 200,
                        Data = IsLookup ?
                    mapper.Map<List<SpecialtyOutput>>(uow.SpecialtyRepo.Get())
                    .Select(d => new { d.Id, d.Name }):data
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
    }
}
