using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UniversityProfUnit.Application.Genders.Dtos;
using UniversityProfUnit.Logic.InterFaces;

namespace UniversityProfUnit.Application.Genders.Queries.GetGender
{
    public class GetGenderQuery : IRequest<List<GenderDto>>
    {
    }

    public class GetGenderQueryHandler : IRequestHandler<GetGenderQuery, List<GenderDto>>
    {
        private readonly IUniversityProfUnitContext _context;

        public GetGenderQueryHandler(IUniversityProfUnitContext universityProfUnitContext)
        {
            _context = universityProfUnitContext;
        }
        public async Task<List<GenderDto>> Handle(GetGenderQuery request, CancellationToken cancellationToken)
        {
            List<GenderDto> result = new List<GenderDto>();

            result = await _context.Genders.Select(x => new GenderDto
            {
                GenderId = x.GenderId,
                GenderName = x.GenderName
            }).ToListAsync();

            return result;
        }
    }
}
