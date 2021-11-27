using ProjectHotel.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectHotel.BLL.Interfaces
{
    public interface IRoomService
    {
        public void Add(RoomDTO room);
        public IEnumerable<RoomDTO> Get();
        public RoomDTO Get(string ID);
        public void Edit(RoomDTO room);
        public void Delete(string ID);
        public List<RoomDTO> GetAvailableRoomsByDate(DateTime Start, DateTime End, string CategoryID = null);
        public bool GetAbailableState(DateTime Start, DateTime End, string RoomID);
        public decimal? GetTotalPriceForRoom(string ID, DateTime Start, DateTime End);
    }
}
