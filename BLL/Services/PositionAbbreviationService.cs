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
    class PositionAbbreviationService: IPositionAbbreviationService
    {
        IUnitOfWork uow;
        IMapper mapper;
        public PositionAbbreviationService(IUnitOfWork _uow, IMapper _mapper)
        {
            uow = _uow;
            mapper = _mapper;
        }
        public ServiceResponse Add(PositionAbbreviationInput input)
        {
            try
            {
                if (uow.PositionAbbreviationRepo.Get().Select(U => U.Name).Contains(input.Name))
                    return new ServiceResponse
                    {
                        IsError = true,
                        Message = "هذا الاسم موجود من قبل",
                        Data = input.Name,
                        Code=400
                    };

                uow.PositionAbbreviationRepo.Insert(mapper.Map<PositionAbbreviation>(input));
                uow.Save();
                return new ServiceResponse
                {
                    IsError = true,
                    Message = "تمت الإضافة",
                    Data = uow.PositionAbbreviationRepo.Get().LastOrDefault().Id,
                    Code=200
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

        public ServiceResponse Update(PositionAbbreviationInput input)
        {
            try
            {
                if (uow.PositionAbbreviationRepo.Get().Select(U => U.Name).Contains(input.Name))
                    return new ServiceResponse
                    {
                        IsError = true,
                        Message = "هذا الاسم موجود من قبل",
                        Data = input.Name,
                        Code=400
                    };
                uow.PositionAbbreviationRepo.Update(mapper.Map<PositionAbbreviation>(input));
                uow.Save();
                return new ServiceResponse
                {
                    IsError = false,
                    Message = "تم التعديل بنجاح",
                    Data = input.Name,
                    Code=200
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
                uow.PositionAbbreviationRepo.Delete(Id);
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
                var data = mapper.Map<PositionAbbreviationOutput>(uow.PositionAbbreviationRepo.GetById(Id));
                if (data != null)
                    return new ServiceResponse
                    {
                        IsError = false,
                        Message = null,
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
        public ServiceResponse GetAll()
        {
            try
            {
             var   data= mapper.Map<List<PositionAbbreviationOutput>>(uow.PositionAbbreviationRepo.Get());
                if (data != null)
                    return new ServiceResponse
                    {
                        IsError = false,
                        Message = null,
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

    }
}
