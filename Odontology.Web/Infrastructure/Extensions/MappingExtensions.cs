using System;
using System.Collections.Generic;
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
                        v.UserId = userId;
                    }
                }).Config;

            return viewModel.Adapt<VisitCreateDto>(mapConfig);
        }

        public static ApplicationUser ToApplicationUser(this RegistrationViewModel viewModel)
        {
            var mapConfig = TypeAdapterConfig<RegistrationViewModel, ApplicationUser>
                .NewConfig()
                .Map(dest => dest.UserName,
                    src => src.Email)
                .Config;

            return viewModel.Adapt<ApplicationUser>(mapConfig);
        }

        public static ArticleViewModel ToArticleViewModel(this ArticleDto articleDto)
        {
            var mapConfig = TypeAdapterConfig<ArticleViewModel, ArticleDto>
                .NewConfig().Config;

            return articleDto.Adapt<ArticleViewModel>(mapConfig);
        }

        public static ArticleCreateDto ToArticleCreateDto(this ArticleCreateViewModel viewModel)
        {
            var mapConfig = TypeAdapterConfig<ArticleCreateViewModel, ArticleCreateDto>
                .NewConfig()
                .Ignore(dest => dest.Files, 
                        src => src.Files)
                .Map(dest => dest.Article,
                    src => src.EntityViewModel)
                .Config;

            var dto = viewModel.Adapt<ArticleCreateDto>(mapConfig);
            dto.Files = viewModel.Files;
            return dto;
        }

        public static EmployeeEditDto ToEmployeeEditDto(this EntityCreateViewModel<EmployeeEditViewModel> viewModel)
        {
            var mapConfig = TypeAdapterConfig<EntityCreateViewModel<EmployeeEditViewModel>, EmployeeEditDto>
                .NewConfig()
                .Map(dest => dest.EmployeeDetailedDto,
                    src => src.EntityViewModel)
                .Config;

            var employeeEditDto = viewModel.Adapt<EmployeeEditDto>(mapConfig);
            employeeEditDto.File = viewModel.EntityViewModel.File;
            return employeeEditDto;
        }
    }
}
