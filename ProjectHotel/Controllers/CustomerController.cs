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
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private ICustomerService customerService;
        private IMapper mapper = new MapperConfiguration(cfg => {
            cfg.CreateMap<CategoryDTO, CategoryViewModel>();
            cfg.CreateMap<CategoryViewModel, CategoryDTO>();

            cfg.CreateMap<CategoryInfoDTO, CategoryInfoViewModel>();
            cfg.CreateMap<CategoryInfoViewModel, CategoryInfoDTO>();

            cfg.CreateMap<RoomViewModel, RoomDTO>();
            cfg.CreateMap<RoomDTO, RoomViewModel>();

            cfg.CreateMap<RoomImageDTO, RoomImageViewModel>();
            cfg.CreateMap<RoomImageViewModel, RoomImageDTO>();

            cfg.CreateMap<CustomerViewModel, CustomerDTO>();
            cfg.CreateMap<CustomerDTO, CustomerViewModel>();

            cfg.CreateMap<BookingInfoViewModel, BookingInfoDTO>();
            cfg.CreateMap<BookingInfoDTO, BookingInfoViewModel>();
        }).CreateMapper();
        public CustomerController(ICustomerService customerService)
        {
            this.customerService = customerService;
        }
        [Authorize("Administrator", "Moderator")]
        [HttpGet]
        public IEnumerable<CustomerViewModel> Get()
        {
            return mapper.Map<IEnumerable<CustomerViewModel>>(customerService.Get());
        }
        [Authorize("Administrator", "Moderator")]
        [HttpGet("{ID}")]
        public CustomerViewModel Get(string ID)
        {
            return mapper.Map<CustomerViewModel>(customerService.Get(new Guid(ID)));
        }
        [Authorize("Administrator", "Moderator")]
        [HttpPost]
        public void Post([FromBody] CustomerViewModel customer)
        {
            if (ModelState.IsValid)
            {
                customerService.Add(mapper.Map<CustomerDTO>(customer));
                Response.StatusCode = 201;
            }
            else
            {
                Response.StatusCode = 400;
            }
        }
        [Authorize("Administrator", "Moderator")]
        [HttpPut]
        public void Put([FromBody] CustomerViewModel customer)
        {
            if (ModelState.IsValid)
            {
                customerService.Edit(mapper.Map<CustomerDTO>(customer));
                Response.StatusCode = 204;
            }
            else
            {
                Response.StatusCode = 400;
            }
        }
        [Authorize("Administrator", "Moderator")]
        [HttpDelete("{ID}")]
        public void Delete(string ID)
        {
            try
            {
                customerService.Delete(new Guid(ID));
            }
            finally
            {
                Response.StatusCode = 204;
            }
        }
    }
}
