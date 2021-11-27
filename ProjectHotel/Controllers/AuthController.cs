using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectHotel.BLL.DTO;
using ProjectHotel.BLL.Interfaces;
using ProjectHotel.Helpers;
using ProjectHotel.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectHotel.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthService authService;
        private IMapper mapper = new MapperConfiguration(cfg => {
            cfg.CreateMap<EmployeeDTO, EmployeeViewModel>();
            cfg.CreateMap<EmployeeViewModel, EmployeeDTO>();

            cfg.CreateMap<EmployeeRoleDTO, EmployeeRoleViewModel>();
            cfg.CreateMap<EmployeeRoleViewModel, EmployeeRoleDTO>();
        }).CreateMapper();
        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }
        [HttpPost("/Token")]
        public JsonResult Token([FromBody] EmplpoyeeGetTokenViewModel emplpoyee)
        {
            return new JsonResult(authService.GetToken(emplpoyee.Login, emplpoyee.Password));
        }
        [HttpGet("/IsAuthenticated")]
        [Authorize("Administrator", "Moderator")]
        public EmployeeViewModel Get()
        {
            var employee = mapper.Map<EmployeeViewModel>(HttpContext.Items["Employee"] as EmployeeDTO);
            return employee;
        }
    }
}
