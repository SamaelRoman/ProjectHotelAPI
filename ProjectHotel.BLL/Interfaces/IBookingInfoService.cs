using ProjectHotel.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectHotel.BLL.Interfaces
{
    public interface IBookingInfoService
    {
        public void Add(BookingInfoDTO bookingInfo);
        public BookingInfoDTO Get(Guid ID);
        public IEnumerable<BookingInfoDTO> Get();
        public void Edit(BookingInfoDTO bookingInfo);
        public void Delete(Guid ID);
        public IEnumerable<BookingInfoDTO> GetBookingInfoByPassportID(string PassportID);
    }
}
