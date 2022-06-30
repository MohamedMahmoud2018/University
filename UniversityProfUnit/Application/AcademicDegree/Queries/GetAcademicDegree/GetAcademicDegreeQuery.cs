using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UniversityProfUnit.Application.AcademicDegree.Dtos;
using UniversityProfUnit.Logic.InterFaces;

namespace UniversityProfUnit.Application.AcademicDegree.Queries.GetAcademicDegree
{
    public class GetAcademicDegreeQuery : IRequest<List<AcademicDegreeDto>>
    {
    }

    public class GetAcademicDegreeQueryHandler : IRequestHandler<GetAcademicDegreeQuery, List<AcademicDegreeDto>>
    {
        private readonly IUniversityProfUnitContext _context;

        public GetAcademicDegreeQueryHandler(IUniversityProfUnitContext universityProfUnitContext)
        {
            _context = universityProfUnitContext;
        }
        public async Task<List<AcademicDegreeDto>> Handle(GetAcademicDegreeQuery request, CancellationToken cancellationToken)
        {
            List<AcademicDegreeDto> result = new List<AcademicDegreeDto>();

            result = await _context.AcademicDegrees.Select(x => new AcademicDegreeDto
            {
                AcademicDegreeId = x.AcademicDegreeId,
                AcademicDegreeName = x.AcademicDegreeName
            }).ToListAsync();

            return result;
        }
    }
}
