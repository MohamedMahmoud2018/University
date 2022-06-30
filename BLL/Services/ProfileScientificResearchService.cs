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
    public class ProfileScientificResearchService : IProfileScientificResearchService
    {
        IUnitOfWork uow;
        IMapper mapper;
        public ProfileScientificResearchService(IUnitOfWork _uow, IMapper _mapper)
        {
            uow = _uow;
            mapper = _mapper;
        }

        public ServiceResponse Add(ProfileScientificResearchInput input)
        {
            try
            {
                uow.ProfileScientificResearchRepo.Insert(mapper.Map<ProfileScientificResearch>(input));
                uow.Save();
                return new ServiceResponse
                {
                    IsError = false,
                    Message = "تمت الإضافة",
                    Data = uow.ProfileScientificResearchRepo.Get().LastOrDefault().Id,
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
        public ServiceResponse Update(ProfileScientificResearchInput input)
        {
            try
            {
                var DBParticipants = uow.ScientificResearchParticipantRepo
                        .Get(P => P.ProfileScientificResearchId == input.Id);

                foreach (var Participants in DBParticipants)
                {
                    if (input.ScientificResearchParticipant.ToList().Find(K => K.Id == Participants.Id) == null)
                    {
                        uow.ScientificResearchParticipantRepo.Delete(Participants.Id);
                    }
                }
                uow.ProfileScientificResearchRepo.Update(mapper.Map<ProfileScientificResearch>(input));
                uow.Save();
                return new ServiceResponse
                {
                    IsError = false,
                    Message = "تم التعديل",
                    Data = input.Id,
                    Code = 200
                };
            }
            catch (Exception ex)
            {
                // insert ex exception in databse Error Log Table
                return new ServiceResponse
                {
                    IsError = true,
                    Message = "خطأ فى التعديل" + "    " + ex.Message,
                    Data = input.Id,
                    Code = 500
                };
            }
        }

        public ServiceResponse Delete(int Id)
        {
            try { 

            var DBPResearchPartcipants = uow.ScientificResearchParticipantRepo.Get(P => P.ProfileScientificResearchId == Id);
            uow.ScientificResearchParticipantRepo.DeleteRange(DBPResearchPartcipants.ToList());

            uow.ProfileScientificResearchRepo.Delete(Id);
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
                    Message =  ex.Message,
                    Code = 500
                };
}
        }

        public ServiceResponse GetAll()
        {
            try
            {
                var data = mapper.Map<List<ProfileScientificResearchOutput>>
                    (uow.ProfileScientificResearchRepo.Get());
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
                var data = mapper.Map<ProfileScientificResearchOutput>
                    (uow.ProfileScientificResearchRepo.GetById(Id));
                if (data != null)
                {
                    //data.ScientificResearchParticipant =
                    //    mapper.Map<List<ScientificResearchParticipantOutput>>
                    //    (uow.ScientificResearchParticipantRepo.Get()
                    //    .Where(x => x.ProfileScientificResearchId == Id));
                    return new ServiceResponse
                    {
                        IsError = false,
                        Code = 200,
                        Data = data
                    };
                }
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
