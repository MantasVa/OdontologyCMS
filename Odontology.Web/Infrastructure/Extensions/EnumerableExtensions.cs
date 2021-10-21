﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Odontology.Business.DTO;

namespace Odontology.Web.Infrastructure.Extensions
{
    internal static class EnumerableExtensions
    {
        public static IEnumerable<SelectListItem> ToSelectListItemsEnumerable(this IEnumerable<EmployeeDto> employees)
        {
            return new List<SelectListItem>(
                employees.Select(c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = c.Fullname
                    }
                ));
        }

        public static IEnumerable<SelectListItem> ToSelectListItemsEnumerable(this IEnumerable<UserNameDto> users)
        {
            return new List<SelectListItem>(
                users.Select(c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = c.Fullname
                    }
                ));
        }

        public static IEnumerable<SelectListItem> ToSelectListItemsEnumerable(this IEnumerable<RoleDto> employees)
        {
            return new List<SelectListItem>(
                employees.Select(c => new SelectListItem
                    {
                        Value = c.Name,
                        Text = c.Name
                    }
                ));
        }
    }
}
