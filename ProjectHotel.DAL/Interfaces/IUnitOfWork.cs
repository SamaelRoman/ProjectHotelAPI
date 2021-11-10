using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectHotel.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        public IEmployeeRepository Employees { get; }
        public IEmployeeRoleRepository EmployeeRoles { get; }
        public IBookingInfoRepository BookingInfoes { get;}
        public ICategoryInfoRepository CategoryInfoes { get;}
        public ICategoryRepository Categories { get;}
        public ICustomerRepository Customers { get;}
        public IRoomImageRepository RoomImages { get;}
        public IRoomRepository Rooms { get;}

        void SaveChanges();
    }
}
