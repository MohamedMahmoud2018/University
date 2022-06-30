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
    public class GradeService : IGradeService
    {
        IUnitOfWork uow;
        IMapper mapper;
        public GradeService(IUnitOfWork _uow, IMapper _mapper)
        {
            uow = _uow;
            mapper = _mapper;
        }
        public ServiceResponse Add(GradeInput input)
        {
            try
            {

                if (uow.GradeRepo.Get().Select(U => U.Name).Contains(input.Name))
                    return new ServiceResponse
                    {
                        IsError = true,
                        Message = "هذا الاسم موجود من قبل",
                        Data = input.Name,
                        Code = 400
                    };
                uow.GradeRepo.Insert(mapper.Map<Grade>(input));
                uow.Save();
                return new ServiceResponse
                {
                    IsError = false,
                    Message = "تمت الإضافة",
                    Data = uow.GradeRepo.Get().LastOrDefault().Id,
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

        public ServiceResponse Update(GradeInput input)
        {
            try
            {
                if (uow.GradeRepo.Get().Select(U => U.Name).Contains(input.Name))
                    return new ServiceResponse
                    {
                        IsError = true,
                        Message = "هذا الاسم موجود من قبل",
                        Data = input.Name,
                        Code = 400
                    };
                uow.GradeRepo.Update(mapper.Map<Grade>(input));
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

                uow.GradeRepo.Delete(Id); uow.Save();
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
                // return mapper.Map<List<GradeOutput>>(uow.GradeRepo.Get());
                var data = mapper.Map<List<GradeOutput>>(uow.GradeRepo.Get());

                if (data != null)
                    return new ServiceResponse
                    {
                        IsError = false,
                        Code = 200,
                        Data = mapper.Map<List<GradeOutput>>(uow.GradeRepo.Get())
                        //.Select(AD => new { AD.Id, AD.Name }) : data
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
                var data = mapper.Map<GradeOutput>(uow.GradeRepo.GetById(Id));
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
