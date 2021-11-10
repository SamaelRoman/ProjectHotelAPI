using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ProjectHotel.BLL.DTO;
using ProjectHotel.BLL.Interfaces;
using ProjectHotel.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectHotel.Helpers
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private IMapper mapper = new MapperConfiguration(cfg=> {
            cfg.CreateMap<EmployeeDTO, EmployeeViewModel>();
            cfg.CreateMap<EmployeeViewModel, EmployeeDTO>();

            cfg.CreateMap<EmployeeRoleDTO, EmployeeRoleViewModel>();
            cfg.CreateMap<EmployeeRoleViewModel, EmployeeRoleDTO>();
        }).CreateMapper();
        private string[] Roles;
        public AuthorizeAttribute(params string[] Roles)
        {
            if(Roles == null)
            {
                this.Roles = null;
            }
            else
            {
                this.Roles = Roles;
            }
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var employee = mapper.Map<EmployeeViewModel>(context.HttpContext.Items["Employee"] as EmployeeDTO);
            if (employee == null)
            {
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
            else
            {
                if (Roles != null)
                {
                    if (!Roles.Contains(employee.Role.RoleName))
                    {
                        context.Result = new JsonResult(new { message = "Forbidden" }) { StatusCode = StatusCodes.Status403Forbidden };
                    }
                }
            }
        }
    }
}
