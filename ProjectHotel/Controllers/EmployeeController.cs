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
    public class EmployeeController : ControllerBase
    {
        private IEmployeeService employeeService;
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
        public EmployeeController(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        [HttpGet]
        public IEnumerable<EmployeeViewModel> Get()
        {
            return mapper.Map<IEnumerable<EmployeeViewModel>>(employeeService.Get());
        }

        [HttpGet("{ID}")]
        public EmployeeViewModel Get(string ID)
        {
            if(ID == null)
            {
                throw new ArgumentNullException();
            }
            var result = mapper.Map<EmployeeViewModel>(employeeService.Get(new Guid(ID)));
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
        public void Post([FromBody]EmployeeRegistrationViewModel employee)
        {
            if (ModelState.IsValid)
            {
                employeeService.Add(mapper.Map<EmployeeDTO>(employee));
                Response.StatusCode = 201;
            }
            else
            {
                Response.StatusCode = 400;
            }
        }
        [HttpPut]
        public void Put([FromBody] EmployeeViewModel employee)
        {
            if (ModelState.IsValid)
            {
                employeeService.Edit(mapper.Map<EmployeeDTO>(employee));
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
                employeeService.Delete(id);
            }
            finally
            {
                Response.StatusCode = 204;
            }
            
        }
    }
}
