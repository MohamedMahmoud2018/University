using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityProfUnit.Data;
using UniversityProfUnit.Logic.InterFaces;

namespace UniversityProfUnit
{
    public static class ContextServiceExtensions
    {
        public static IServiceCollection AddUniversityProfUnitContext(this IServiceCollection services)
        {
            services.AddTransient<IUniversityProfUnitContext, UniversityProfUnitContext>();
            return services;
        }
    }
}
