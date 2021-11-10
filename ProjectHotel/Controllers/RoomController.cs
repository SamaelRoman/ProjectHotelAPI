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
    public class RoomController : ControllerBase
    {
        private IRoomService roomService;
        private IMapper mapper = new MapperConfiguration(cfg => {
            cfg.CreateMap<CategoryDTO, CategoryViewModel>();
            cfg.CreateMap<CategoryViewModel, CategoryDTO>();

            cfg.CreateMap<CategoryInfoDTO, CategoryInfoViewModel>();
            cfg.CreateMap<CategoryInfoViewModel, CategoryInfoDTO>();

            cfg.CreateMap<RoomViewModel, RoomDTO>();
            cfg.CreateMap<RoomDTO, RoomViewModel>();

            cfg.CreateMap<AvaiableRoomViewModel, RoomDTO>();
            cfg.CreateMap<RoomDTO, AvaiableRoomViewModel>();

            cfg.CreateMap<RoomImageDTO, RoomImageViewModel>();
            cfg.CreateMap<RoomImageViewModel, RoomImageDTO>();

            cfg.CreateMap<CustomerViewModel, CustomerDTO>();
            cfg.CreateMap<CustomerDTO, CustomerViewModel>();

            cfg.CreateMap<BookingInfoViewModel, BookingInfoDTO>();
            cfg.CreateMap<BookingInfoDTO, BookingInfoViewModel>();
        }).CreateMapper();
        public RoomController(IRoomService roomService)
        {
            this.roomService = roomService;
        }

        [Authorize("Administrator","Moderator")]
        [HttpGet]
        public IEnumerable<RoomViewModel> Get()
        {
            return mapper.Map<IEnumerable<RoomViewModel>>(roomService.Get());
        }

        [Authorize("Administrator", "Moderator")]
        [HttpGet("{ID}")]
        public RoomViewModel Get(string ID)
        {
            return mapper.Map<RoomViewModel>(roomService.Get(ID));

        }
        //api/Room/2021.11.12/2021.11.17/042951d5-61f3-45b4-96a0-f67a0fd13248 or api/Room/2021.11.12/2021.11.17/
        [HttpGet("{Start}/{End}/{CategoryID?}")]
        public IEnumerable<AvaiableRoomViewModel> Get(DateTime Start, DateTime End, string CategoryID)
        {
            return mapper.Map<IEnumerable<AvaiableRoomViewModel>>(roomService.GetAvailableRoomsByDate(Start, End, CategoryID));
        }

        [Authorize("Administrator", "Moderator")]
        [HttpPost]
        public void Post([FromBody] RoomViewModel room)
        {
            if (ModelState.IsValid)
            {
                roomService.Add(mapper.Map<RoomDTO>(room));
                Response.StatusCode = 201;
            }
            else
            {
                Response.StatusCode = 400;
            }
        }
        [Authorize("Administrator", "Moderator")]
        [HttpPut]
        public void Put([FromBody] RoomViewModel room)
        {
            if (ModelState.IsValid)
            {
                roomService.Edit(mapper.Map<RoomDTO>(room));
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
                roomService.Delete(ID);
            }
            finally
            {
                Response.StatusCode = 204;
            }
        }
    }
}
