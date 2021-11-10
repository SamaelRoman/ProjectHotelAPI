using AutoMapper;
using ProjectHotel.BLL.DTO;
using ProjectHotel.BLL.Interfaces;
using ProjectHotel.DAL.Entities;
using ProjectHotel.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectHotel.BLL.Services
{
    public class CategoryService : ICategoryService
    {
        private IUnitOfWork DataBase;
        private IMapper mapper = new MapperConfiguration(cfg=> {
            cfg.CreateMap<CategoryDTO, Category>();
            cfg.CreateMap<Category, CategoryDTO>();

            cfg.CreateMap<CategoryInfoDTO, CategoryInfo>();
            cfg.CreateMap<CategoryInfo, CategoryInfoDTO>();

            cfg.CreateMap<Room, RoomDTO>();
            cfg.CreateMap<RoomDTO, Room>();

            cfg.CreateMap<RoomImageDTO, RoomImage>();
            cfg.CreateMap<RoomImage, RoomImageDTO>();

            cfg.CreateMap<Customer, CustomerDTO>();
            cfg.CreateMap<CustomerDTO, Customer>();

            cfg.CreateMap<BookingInfo, BookingInfoDTO>();
            cfg.CreateMap<BookingInfoDTO, BookingInfo>();

        }).CreateMapper();
        public CategoryService(IUnitOfWork DataBase)
        {
            this.DataBase = DataBase;
        }
        public void Add(CategoryDTO category)
        {
            try
            {
                DataBase.Categories.Add(mapper.Map<Category>(category));
                DataBase.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Delete(Guid ID)
        {
            try
            {
                DataBase.Categories.Delete(ID);
                DataBase.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void Edit(CategoryDTO category)
        {
            try
            {
                DataBase.Categories.Edit(mapper.Map<Category>(category));
                DataBase.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public IEnumerable<CategoryDTO> Get()
        {
            try
            {
                return mapper.Map<IEnumerable<CategoryDTO>>(DataBase.Categories.Get());
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public CategoryDTO Get(Guid ID)
        {
            try
            {
                return mapper.Map<CategoryDTO>(DataBase.Categories.Get(ID));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
