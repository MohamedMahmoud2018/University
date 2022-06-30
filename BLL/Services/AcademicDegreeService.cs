using AutoMapper;
using CORE.DAL;
using CORE.DTO;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    
    public class AcademicDegreeService : IAcademicDegreeService
    {
        IUnitOfWork uow;
        IMapper mapper;
        public AcademicDegreeService(IUnitOfWork _uow, IMapper _mapper)
        {
            uow = _uow;
            mapper = _mapper;
        }
        public ServiceResponse Add(AcademicDegreeInput input)
        {
            try
            {
                if (uow.AcademicDegreeRepo.Get().Select(U => U.Name).Contains(input.Name))
                    return new ServiceResponse
                    {
                        IsError = true,
                        Message = "هذا الاسم موجود من قبل",
                        Data = input.Name,
                        Code=400
                    };
                uow.AcademicDegreeRepo.Insert(mapper.Map<AcademicDegree>(input));
                uow.Save();
                return  new ServiceResponse
                {
                    IsError = false,
                    Message = "تمت الإضافة",
                    Data = uow.AcademicDegreeRepo.Get().LastOrDefault().Id,
                    Code=200
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

        public ServiceResponse Update(AcademicDegreeInput input)
        {
            try
            {
                if (uow.AcademicDegreeRepo.Get().Select(U => U.Name).Contains(input.Name))
                    return new ServiceResponse
                    {
                        IsError = true,
                        Message = "هذا الاسم موجود من قبل",
                        Data = input.Name,
                        Code=400
                    };
                uow.AcademicDegreeRepo.Update(mapper.Map<AcademicDegree>(input));
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
                uow.AcademicDegreeRepo.Delete(Id);
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
        public ServiceResponse GetAll(bool IsLookup = false)
        {
            try
            {
                var data= mapper.Map<List<AcademicDegreeOutput>>(uow.AcademicDegreeRepo.Get());

                if (data != null)
                    return new ServiceResponse
                    {
                        IsError = false,
                        Code = 200,
                        Data = IsLookup ? mapper.Map<List<AcademicDegreeOutput>>(uow.AcademicDegreeRepo.Get())
                        .Select(AD => new { AD.Id, AD.Name }):data
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
                var data = mapper.Map<AcademicDegreeOutput>(uow.AcademicDegreeRepo.GetById(Id));
              if(data!=null)
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
