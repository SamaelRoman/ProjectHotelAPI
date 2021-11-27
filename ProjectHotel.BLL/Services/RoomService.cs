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
    public class RoomService : IRoomService
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
        public RoomService(IUnitOfWork DataBase)
        {
            this.DataBase = DataBase;
        }
        public void Add(RoomDTO room)
        {
            try
            {
                DataBase.Rooms.Add(mapper.Map<Room>(room));
                DataBase.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Delete(string ID)
        {
            try
            {
                DataBase.Rooms.Delete(ID);
                DataBase.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Edit(RoomDTO room)
        {
            try
            {
                DataBase.Rooms.Edit(mapper.Map<Room>(room));
                DataBase.SaveChanges();
            }
            catch (Exception ex)
            {
                throw  ex;
            }
        }

        public IEnumerable<RoomDTO> Get()
        {
            return mapper.Map<IEnumerable<RoomDTO>>(DataBase.Rooms.Get());
        }

        public RoomDTO Get(string ID)
        {
            return mapper.Map<RoomDTO>(DataBase.Rooms.Get(ID));
        }
        public bool GetAbailableState(DateTime Start, DateTime End,string RoomID)
        {
            if (Start > End)
            {
                throw new Exception("Дата начала бронирования не может быть позже чем дата окончания бронирования!");
            }
            if (Start.Date == End.Date)
            {
                throw new Exception("Дата начала бронирвоания и дата окончания бронирования не могут совпадать!");
            }
            var CurrentRoom = DataBase.Rooms.Get(RoomID);
            if (CurrentRoom == null)
            {
                throw new Exception($"По данному ID {RoomID} номера не найденно!");
            }

            bool check = true;
            foreach (var BI in CurrentRoom.BookingInfos)
            {
                if (Start >= BI.StartBooking && Start <= BI.EndBooking ||
                   End >= BI.StartBooking && End <= BI.EndBooking ||
                   Start <= BI.StartBooking && End >= BI.EndBooking)
                {
                    check = false;
                    break;
                }
            }
            if (check)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public List<RoomDTO> GetAvailableRoomsByDate(DateTime Start, DateTime End,string CategoryID = null)
        {
            if (Start > End)
            {
                throw new Exception("Дата начала бронирования не может быть позже чем дата окончания бронирования!");
            }
            if(Start.Date == End.Date)
            {
                throw new Exception("Дата начала бронирвоания и дата окончания бронирования не могут совпадать!");
            }
            List<RoomDTO> AllRooms = new List<RoomDTO>();
            AllRooms = (List<RoomDTO>)Get();
            List<RoomDTO> AvailableRooms = new List<RoomDTO>();

            foreach (var Room in AllRooms)
            {
                bool check = true;
                foreach (var BI in Room.BookingInfos)
                {
                    if (Start >= BI.StartBooking && Start <= BI.EndBooking ||
                       End >= BI.StartBooking && End <= BI.EndBooking ||
                       Start <= BI.StartBooking && End >= BI.EndBooking)
                    {
                        check = false;
                        break;
                    }
                }
                if (check)
                {
                    AvailableRooms.Add(Room);
                }
            }

            List<RoomDTO> Result = null;
            if (CategoryID != null)
            {
                Result = AvailableRooms.Where(R => R.CategoryID == new Guid(CategoryID)).ToList();
            }
            else
            {
                Result = AvailableRooms;
            }

            if (Result == null || Result.Count == 0)
            {
                throw new Exception("На указанный промежуток времени свободных номеров нет!");
            }
            return Result;
        }

        public decimal? GetTotalPriceForRoom(string ID, DateTime Start, DateTime End)
        {

            if(ID != null && Start != null && End != null)
            {
                int NumberOfDays = (End - Start).Days;
                decimal result = 0;
                var CurrentRoom = Get(ID);
                if(CurrentRoom == null)
                {
                    throw new ArgumentException();
                }
                for (int i = 0; i <= NumberOfDays; i++)
                {
                    result += CurrentRoom.Category.CategoryInfos.First(CI => CI.PriceAtTheMomentStart <= Start.AddDays(i) && (CI.PriceAtTheMomentEnd >= Start.AddDays(i) || CI.PriceAtTheMomentEnd == null)).Price;
                }
                return result;
            }
            else
            {
                throw new ArgumentNullException();
            }
            
        }
    }
}
