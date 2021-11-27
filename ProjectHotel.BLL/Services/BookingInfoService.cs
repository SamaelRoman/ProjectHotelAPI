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
    public class BookingInfoService : IBookingInfoService
    {
        private IUnitOfWork DataBase;
        private IRoomService roomService;
        private ICustomerService customerService;
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
        public BookingInfoService(IUnitOfWork DataBase, IRoomService roomService,ICustomerService customerService)
        {
            this.DataBase = DataBase;
            this.roomService = roomService;
            this.customerService = customerService;
        }
        public void Add(BookingInfoDTO bookingInfo)
        {
            if (roomService.GetAbailableState(bookingInfo.StartBooking, bookingInfo.EndBooking, bookingInfo.RoomID) == false)
            {
                throw new Exception($"Номер с ID {bookingInfo.RoomID} не доступен для бронирования не указанную дату!");
            }
            if(customerService.GetByPassportID(bookingInfo.Customer.PassportID) != null)
            {                
                bookingInfo.CustomerID = customerService.GetByPassportID(bookingInfo.Customer.PassportID).ID;
                bookingInfo.Customer = null;
            }
            try
            {
                bookingInfo.Room = mapper.Map<RoomDTO>(DataBase.Rooms.Get(bookingInfo.RoomID));
                bookingInfo.TotalPrice = (decimal)roomService.GetTotalPriceForRoom(bookingInfo.RoomID, bookingInfo.StartBooking, bookingInfo.EndBooking);
                bookingInfo.Room = null;
                DataBase.BookingInfoes.Add(mapper.Map<BookingInfo>(bookingInfo));
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
                DataBase.BookingInfoes.Delete(ID);
                DataBase.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Edit(BookingInfoDTO bookingInfo)
        {
            try
            {
                
                bookingInfo.Room = mapper.Map<RoomDTO>(DataBase.Rooms.Get(bookingInfo.RoomID));
                bookingInfo.TotalPrice = (decimal)roomService.GetTotalPriceForRoom(bookingInfo.RoomID, bookingInfo.StartBooking, bookingInfo.EndBooking);
                bookingInfo.Room = null;
                DataBase.BookingInfoes.Edit(mapper.Map<BookingInfo>(bookingInfo));
                DataBase.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BookingInfoDTO Get(Guid ID)
        {
            return mapper.Map<BookingInfoDTO>(DataBase.BookingInfoes.Get(ID));
        }

        public IEnumerable<BookingInfoDTO> Get()
        {
            return mapper.Map<IEnumerable<BookingInfoDTO>>(DataBase.BookingInfoes.Get());
        }
        public IEnumerable<BookingInfoDTO> GetBookingInfoByPassportID(string PassportID)
        {
            var Customer = customerService.GetByPassportID(PassportID);
            if(Customer == null)
            {
                throw new Exception($"По данному номер паспорта {PassportID} клиент не обнаружен!");
            }
            return mapper.Map<IEnumerable<BookingInfoDTO>>(DataBase.BookingInfoes.Get().Where(BI=>BI.CustomerID == Customer.ID));
        }
    }
}
