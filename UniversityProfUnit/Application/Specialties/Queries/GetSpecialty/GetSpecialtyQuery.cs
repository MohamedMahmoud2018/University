using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UniversityProfUnit.Application.Specialties.SpecialtyDtos;
using UniversityProfUnit.Logic.InterFaces;

namespace UniversityProfUnit.Application.Specialties.Queries.GetSpecialty
{
    public class GetSpecialtyQuery : IRequest<List<SpecialtyDto>>
    {
    }

    public class GetSpecialtyQueryHandler : IRequestHandler<GetSpecialtyQuery, List<SpecialtyDto>>
    {
        private readonly IUniversityProfUnitContext _context;

        public GetSpecialtyQueryHandler(IUniversityProfUnitContext universityProfUnitContext)
        {
            _context = universityProfUnitContext;
        }
        public async Task<List<SpecialtyDto>> Handle(GetSpecialtyQuery request, CancellationToken cancellationToken)
        {
            List<SpecialtyDto> result = new List<SpecialtyDto>();

            result = await _context.Specialties.Select(x => new SpecialtyDto()
            {
                SpecialtyId = x.SpecialtyId,
                SpecialtyName = x.SpecialtyName
            }).ToListAsync();

            return result;
        }
    }
}
