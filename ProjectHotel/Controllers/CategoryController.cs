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
    public class CategoryController : ControllerBase
    {
        private ICategoryService categoryService { get; set; }
        private IMapper mapper = new MapperConfiguration(cfg=> {
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
        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }
        [Authorize("Administrator", "Moderator")]
        [HttpGet]
        public IEnumerable<CategoryViewModel> Get()
        {
            return mapper.Map<IEnumerable<CategoryViewModel>>(categoryService.Get());
        }
        [Authorize("Administrator", "Moderator")]
        [HttpGet("{ID}")]
        public CategoryViewModel Get(string ID)
        {
            if (ID == null)
            {
                throw new ArgumentNullException();
            }
            var Result =  mapper.Map<CategoryViewModel>(categoryService.Get(new Guid(ID)));
            if(Result == null)
            {
                Response.StatusCode = 404;
                return null;
                /*throw new NullReferenceException($"Элемент с данным ID {ID} в базе данных не обнаружен");*/
            }
            else
            {
                return Result;
            }
        }
        [Authorize("Administrator", "Moderator")]
        [HttpPost]
        public void Post([FromBody]CategoryViewModel category)
        {
            if(category == null)
            {
                throw new ArgumentNullException();
            }
            if (ModelState.IsValid)
            {
                categoryService.Add(mapper.Map<CategoryDTO>(category));
                Response.StatusCode = 201;
            }
            else
            {
                Response.StatusCode = 400;
            }
        }
        [Authorize("Administrator", "Moderator")]
        [HttpPut]
        public void Put([FromBody]CategoryViewModel category)
        {
            if (category == null)
            {
                throw new ArgumentNullException();
            }
            if (ModelState.IsValid)
            {
                categoryService.Edit(mapper.Map<CategoryDTO>(category));
                Response.StatusCode = 204;
            }
            else
            {
                Response.StatusCode = 400;
            }
        }
        [Authorize("Administrator", "Moderator")]
        [HttpDelete("{ID}")]
        public void Delte(string ID)
        {
            Guid id = new Guid(ID);
            try
            {
                categoryService.Delete(id);
            }
            finally
            {
                Response.StatusCode = 204;
            }
        }
    }
}
