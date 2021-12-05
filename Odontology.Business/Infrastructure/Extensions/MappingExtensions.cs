using Mapster;
using Odontology.Business.DTO;
using Odontology.Domain.Models;

namespace Odontology.Business.Infrastructure.Extensions
{
    internal static class MappingExtensions
    {
        public static Visit ToVisit(this VisitCreateDto visitCreateDto)
        {
            var mapConfig = TypeAdapterConfig<VisitCreateDto, Visit>
                .NewConfig()
                .Map(dest => dest,
                    src => src.Visit)
                .Map(dest => dest.PatientId,
                    src => src.UserId)
                .Config;

            return visitCreateDto.Adapt<Visit>(mapConfig);
        }

        public static VisitDto ToVisitDto(this Visit visit, ApplicationUser employeeUser)
        {
            var mapConfig = TypeAdapterConfig<Visit, VisitDto>
                .NewConfig()
                .Map(dest => dest.Employee,
                    src => employeeUser)
                .Config;

            return visit.Adapt<VisitDto>(mapConfig);
        }
    }
}
