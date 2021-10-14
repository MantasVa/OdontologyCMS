using System;
using Mapster;
using Odontology.Business.DTO;
using Odontology.Domain.Models;
using Odontology.Web.ViewModels;

namespace Odontology.Web.Infrastructure.Extensions
{
    internal static class MappingExtensions
    {
        public static VisitCreateDto ToVisitCreateDto(this VisitCreateViewModel viewModel, string currentUserId = null)
        {
            var mapConfig = TypeAdapterConfig<VisitCreateViewModel, VisitCreateDto>
                .NewConfig()
                .Map(dest => dest.Visit,
                    src => src.EntityViewModel)
                .AfterMapping(v =>
                {
                    if (currentUserId != null && int.TryParse(currentUserId, out int userId))
                    {
                        v.PatientId = userId;
                    }
                }).Config;

            return viewModel.Adapt<VisitCreateDto>(mapConfig);
        }

        public static ApplicationUser ToApplicationUser(this RegistrationViewModel viewModel)
        {
            var mapConfig = TypeAdapterConfig<RegistrationViewModel, ApplicationUser>
                .NewConfig()
                .Map(dest => dest.UserName,
                    src => src.Email).Config;

            return viewModel.Adapt<ApplicationUser>(mapConfig);
        }
    }
}
