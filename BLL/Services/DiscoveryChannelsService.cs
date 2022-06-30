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
    public class DiscoveryChannelsService : IDiscoveryChannelsService
    {
        IUnitOfWork uow;
        IMapper mapper;

        public DiscoveryChannelsService(IUnitOfWork _uow, IMapper _mapper)
        {
            uow = _uow;
            mapper = _mapper;
        }
        public ServiceResponse Add(DiscoveryChannelsInput input)
        {
            try
            {
                if (uow.DiscoveryChannelsRepo.Get().Select(U => U.Name).Contains(input.Name))
                    return new ServiceResponse
                    {
                        IsError = true,
                        Message = "هذا الاسم موجود من قبل",
                        Data = input.Name,
                        Code=400
                    };
                uow.DiscoveryChannelsRepo.Insert(mapper.Map<DiscoveryChannels>(input));
                uow.Save();
                return new ServiceResponse
                {
                    IsError = true,
                    Message = "تمت الإضافة",
                    Data = uow.DiscoveryChannelsRepo.Get().LastOrDefault().Id,
                    Code=200
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse
                {
                    IsError = true,
                    Message = ex.Message,
                    Data = null,
                    Code=500
                };
            }
        }
        public ServiceResponse Update(DiscoveryChannelsInput input)
        {
            try
            {
                if (uow.DiscoveryChannelsRepo.Get().Select(U => U.Name).Contains(input.Name))
                    return new ServiceResponse
                    {
                        IsError = true,
                        Message = "هذا الاسم موجود من قبل",
                        Data = input.Name,
                        Code=400
                    };
                uow.DiscoveryChannelsRepo.Update(mapper.Map<DiscoveryChannels>(input));
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
                    Data = null,
                    Code=500
                };
            }
        }

        public ServiceResponse Delete(int Id)
        {
            try
            {
                uow.DiscoveryChannelsRepo.Delete(Id);
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
                var data = mapper.Map<List<DiscoveryChannelsOutput>>(uow.DiscoveryChannelsRepo.Get());
                if(data!=null)
                return new ServiceResponse
                {
                    IsError = false,
                    Code = 200,
                    Data = IsLookup ?
                    mapper.Map<List<DiscoveryChannelsOutput>>(uow.DiscoveryChannelsRepo.Get())
                    .Select(DC => new { DC.Id, DC.Name }) : data
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
                var data = mapper.Map<DiscoveryChannelsOutput>(uow.DiscoveryChannelsRepo.GetById(Id));
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
