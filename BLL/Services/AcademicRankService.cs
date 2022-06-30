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
   public class AcademicRankService : IAcademicRankService
    {
        IUnitOfWork uow;
        IMapper mapper;
        public AcademicRankService(IUnitOfWork _uow, IMapper _mapper)
        {
            uow = _uow;
            mapper = _mapper;
        }
        public ServiceResponse Add(AcademicRankInput input)
        {
            try
            {
                if (uow.AcademicRankRepo.Get().Select(U => U.Name).Contains(input.Name))
                    return new ServiceResponse
                    {
                        IsError = true,
                        Message = "هذا الاسم موجود من قبل",
                        Data = input.Name,
                        Code = 400
                    };
                uow.AcademicRankRepo.Insert(mapper.Map<AcademicRank>(input));
                uow.Save();
                return new ServiceResponse
                {
                    IsError = false,
                    Message = "تمت الإضافة",
                    Data = uow.AcademicRankRepo.Get().LastOrDefault().Id,
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

        public ServiceResponse Update(AcademicRankInput input)
        {
            try
            {
                if (uow.AcademicRankRepo.Get().Select(U => U.Name).Contains(input.Name))
                    return new ServiceResponse
                    {
                        IsError = true,
                        Message = "هذا الاسم موجود من قبل",
                        Data = input.Name,
                        Code = 400
                    };
                uow.AcademicRankRepo.Update(mapper.Map<AcademicRank>(input));
                uow.Save();
                return new ServiceResponse
                {
                    IsError = false,
                    Message = "تم التعديل",
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
                uow.AcademicRankRepo.Delete(Id);
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
                var data = mapper.Map<AcademicRankOutput>(uow.AcademicRankRepo.GetById(Id));

                if (data != null)
                    return new ServiceResponse
                    {
                        IsError = false,
                        Code = 200,
                        Data = mapper.Map<AcademicRankOutput>(uow.AcademicRankRepo.GetById(Id))
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
                var data = mapper.Map<List<AcademicRankOutput>>(uow.AcademicRankRepo.Get());

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

    }
}
