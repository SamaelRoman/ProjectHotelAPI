using AutoMapper;
using ProjectHotel.BLL.DTO;
using ProjectHotel.BLL.Interfaces;
using ProjectHotel.DAL.Entities;
using ProjectHotel.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectHotel.BLL.Services
{
    public class CustomerService : ICustomerService
    {
        private IUnitOfWork DataBase;
        private IMapper mapper = new MapperConfiguration(cfg => {
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
        public CustomerService(IUnitOfWork DataBase)
        {
            this.DataBase = DataBase;
        }
        public void Add(CustomerDTO customer)
        {
            try
            {
                DataBase.Customers.Add(mapper.Map<Customer>(customer));
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
                DataBase.Customers.Delete(ID);
                DataBase.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Edit(CustomerDTO customer)
        {
            try
            {
                DataBase.Customers.Edit(mapper.Map<Customer>(customer));
                DataBase.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<CustomerDTO> Get()
        {
            return mapper.Map<IEnumerable<CustomerDTO>>(DataBase.Customers.Get());
        }

        public CustomerDTO Get(Guid ID)
        {
            return mapper.Map<CustomerDTO>(DataBase.Customers.Get(ID));
        }
        public CustomerDTO GetByPassportID(string PassportID)
        {
            return mapper.Map<CustomerDTO>(DataBase.Customers.Get().FirstOrDefault(C => C.PassportID.ToLower() == PassportID.ToLower()));
        }
    }
}
