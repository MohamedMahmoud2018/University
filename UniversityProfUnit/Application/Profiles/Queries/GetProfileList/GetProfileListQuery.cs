using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UniversityProfUnit.Logic;
using UniversityProfUnit.Logic.InterFaces;

namespace UniversityProfUnit.Application.Profiles.Queries.GetProfileList
{
    public class GetProfileListQuery : IRequest<List<GetProfileListDto>>
    {
    }

    public class GetProfileListQueryHandler : IRequestHandler<GetProfileListQuery, List<GetProfileListDto>>
    {
        private readonly IUniversityProfUnitContext _context;
        public GetProfileListQueryHandler(IUniversityProfUnitContext universityProfUnitContext)
        {
            _context = universityProfUnitContext;
        }
        public async Task<List<GetProfileListDto>> Handle(GetProfileListQuery request, CancellationToken cancellationToken)
        {
            List<GetProfileListDto> result = new List<GetProfileListDto>();

            result = await _context.Profiles.Select(x => new GetProfileListDto
            {
                ProfileSerial = x.Serial,
                Name = $"{x.FirstName} {x.SecondName}",
                MainJob = x.MainJob,
                PositionAbbreviation = x.PositionAbbreviation,
                SpecialtyId = x.SpecialtyId
            }).ToListAsync();

            List<int> specialtyIds = result.Select(x => x.SpecialtyId).ToList();
            List<Specialty> specialties = await _context.Specialties.Where(x => specialtyIds.Contains(x.SpecialtyId)).ToListAsync();
            result.ForEach(x => x.SpecialtyName = specialties.FirstOrDefault(y => y.SpecialtyId == x.SpecialtyId).SpecialtyName);

            int serial = default;
            foreach (var item in result.OrderBy(x => x.ProfileSerial).ToList())
            {
                item.Serial = ++serial;
            }

            return result;
        }
    }
}
