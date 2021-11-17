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
    public class BookingInfoController : ControllerBase
    {
        private IBookingInfoService bookingInfoService;
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

            cfg.CreateMap<BookingInfoRegViewModel, BookingInfoDTO>();
            cfg.CreateMap<BookingInfoDTO, BookingInfoRegViewModel>();

            cfg.CreateMap<BookingInfoViewModel, BookingInfoDTO>();
            cfg.CreateMap<BookingInfoDTO, BookingInfoViewModel>();
        }).CreateMapper();
        public BookingInfoController(IBookingInfoService bookingInfoService)
        {
            this.bookingInfoService = bookingInfoService;
        }
        [Authorize("Administrator", "Moderator")]
        [HttpGet]
        public IEnumerable<BookingInfoViewModel> Get()
        {
            return mapper.Map<IEnumerable<BookingInfoViewModel>>(bookingInfoService.Get());
        }
        [Authorize("Administrator", "Moderator")]
        [HttpGet("{ID}")]
        public BookingInfoViewModel Get(string ID)
        {
            if (ID == null)
            {
                throw new ArgumentNullException();
            }
            var result = mapper.Map<BookingInfoViewModel>(bookingInfoService.Get(new Guid(ID)));
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
        [Route("/api/GetBookingInfo/{PassportID}")]
        [HttpGet]
        public IEnumerable<BookingInfoViewModel> GetBookingInfos(string PassportID)
        {
            return mapper.Map<IEnumerable<BookingInfoViewModel>>(bookingInfoService.GetBookingInfoByPassportID(PassportID));
        }
        [HttpPost]

        public void Post([FromBody]BookingInfoRegViewModel bookingInfo)
        {
            if (ModelState.IsValid)
            {
                bookingInfoService.Add(mapper.Map<BookingInfoDTO>(bookingInfo));
                Response.StatusCode = 201;
            }
            else
            {
                Response.StatusCode = 400;
            }
        }
        [Authorize("Administrator", "Moderator")]
        [HttpPut]
        public void Put([FromBody] BookingInfoViewModel bookingInfo)
        {
            if (ModelState.IsValid)
            {
                bookingInfoService.Edit(mapper.Map<BookingInfoDTO>(bookingInfo));
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
                bookingInfoService.Delete(new Guid(ID));
            }
            finally
            {
                Response.StatusCode = 204;
            }
        }
    }
}
