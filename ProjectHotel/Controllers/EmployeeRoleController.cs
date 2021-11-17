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
    [Authorize("Administrator")]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeRoleController : ControllerBase
    {
        private IEmployeeRoleService employeeRoleService;
        private IMapper mapper = new MapperConfiguration(cfg => {

            cfg.CreateMap<EmployeeDTO, EmployeeViewModel>();
            cfg.CreateMap<EmployeeViewModel, EmployeeDTO>();

            cfg.CreateMap<EmployeeRoleDTO, EmployeeRoleViewModel>();
            cfg.CreateMap<EmployeeRoleViewModel, EmployeeRoleDTO>();

            cfg.CreateMap<EmployeeDTO, EmployeeRegistrationViewModel>();
            cfg.CreateMap<EmployeeRegistrationViewModel, EmployeeDTO>();

            cfg.CreateMap<EmployeeRoleDTO, EmployeeRoleCreateViewModel>();
            cfg.CreateMap<EmployeeRoleCreateViewModel, EmployeeRoleDTO>();
        }).CreateMapper();
        public EmployeeRoleController(IEmployeeRoleService employeeRoleService)
        {
            this.employeeRoleService = employeeRoleService;
        }
        [HttpGet]
        public IEnumerable<EmployeeRoleViewModel> Get()
        {
            return mapper.Map<IEnumerable<EmployeeRoleViewModel>>(employeeRoleService.Get());
        }
        [HttpGet("{ID}")]
        public EmployeeRoleViewModel Get(string ID)
        {   
            if(ID == null)
            {
                throw new ArgumentNullException();
            }
            var result = mapper.Map<EmployeeRoleViewModel>(employeeRoleService.Get(new Guid(ID)));
            if(result == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            else
            {
                return result;
            }
        }
        [HttpPost]
        public void Post([FromBody] EmployeeRoleCreateViewModel employeeRole)
        {
            if (ModelState.IsValid)
            {
                employeeRoleService.Add(mapper.Map<EmployeeRoleDTO>(employeeRole));
                Response.StatusCode = 201;
            }
            else
            {
                Response.StatusCode = 400;
            }
        }
        [HttpPut]
        public void Put([FromBody] EmployeeRoleCreateViewModel employeeRole)
        {
            if (ModelState.IsValid)
            {
                employeeRoleService.Edit(mapper.Map<EmployeeRoleDTO>(employeeRole));
                Response.StatusCode = 204;
            }
            else
            {
                Response.StatusCode = 400;
            }
        }
        [HttpDelete("{ID}")]
        public void Delete(string ID)
        {
            Guid id = new Guid(ID);
            try
            {
                employeeRoleService.Delete(id);
            }
            finally
            {
                Response.StatusCode = 204;
            }
        }
    }
}
