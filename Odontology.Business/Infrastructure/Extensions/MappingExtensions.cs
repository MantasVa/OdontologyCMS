using Mapster;
using Odontology.Business.DTO;
using Odontology.Domain.Models;

namespace Odontology.Business.Infrastructure.Extensions
{
    public static class MappingExtensions
    {
        public static Visit ToVisit(this VisitCreateDto viewModel)
        {
            var mapConfig = TypeAdapterConfig<VisitCreateDto, Visit>
                .NewConfig()
                .Map(dest => dest,
                    src => src.Visit)
                .Map(dest => dest.PatientId,
                    src => src.UserId)
                .Config;

            return viewModel.Adapt<Visit>(mapConfig);
        }
    }
}
