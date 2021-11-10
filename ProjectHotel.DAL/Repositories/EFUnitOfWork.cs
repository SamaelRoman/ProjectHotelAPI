using ProjectHotel.DAL.EF;
using ProjectHotel.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectHotel.DAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private bool Disposed = false;
        private IEmployeeRepository _employeeRepository { get; set; }
        private IEmployeeRoleRepository _employeeRoleRepository { get; set; }
        private IBookingInfoRepository _bookingInfoesRepository { get; set; }
        private ICategoryInfoRepository _categoryInfoRepository { get; set; }
        private ICategoryRepository _categoryRepository { get; set; }
        private ICustomerRepository _customerRepository { get; set; }
        private IRoomImageRepository _roomImageRepository { get; set; }
        private IRoomRepository _roomRepository { get; set; }

        private ContextDB contextDB;
        public EFUnitOfWork(string ConnectionString)
        {
            this.contextDB = new ContextDB(ConnectionString);
        }
        public IEmployeeRepository Employees
        {
            get
            {
                if(_employeeRepository == null)
                {
                    _employeeRepository = new EmployeeRepository(contextDB);
                }
                return _employeeRepository;
            }
        }
        public IEmployeeRoleRepository EmployeeRoles
        {
            get
            {
                if (_employeeRoleRepository == null)
                {
                    _employeeRoleRepository = new EmployeeRoleRepository(contextDB);
                }
                return _employeeRoleRepository;
            }
        }

        public IBookingInfoRepository BookingInfoes
        {
            get
            {
                if(_bookingInfoesRepository == null)
                {
                    _bookingInfoesRepository = new BookingInfosRepository(contextDB);
                }
                return _bookingInfoesRepository;
            }
        }

        public ICategoryInfoRepository CategoryInfoes
        {
            get
            {
                if(_categoryInfoRepository == null)
                {
                    _categoryInfoRepository = new CategoryInfoRepository(contextDB);
                }
                return _categoryInfoRepository;
            }
        }

        public ICategoryRepository Categories
        {
            get
            {
                if(_categoryRepository == null)
                {
                    _categoryRepository = new CategoryRepository(contextDB);
                }
                return _categoryRepository;
            }
        }

        public ICustomerRepository Customers
        {
            get
            {
                if(_customerRepository == null)
                {
                    _customerRepository = new CustomerRepository(contextDB);
                }
                return _customerRepository;
            }
        }

        public IRoomImageRepository RoomImages
        {
            get
            {
                if(_roomImageRepository == null)
                {
                    _roomImageRepository = new RoomImagesRepository(contextDB);
                }
                return _roomImageRepository;
            }
        }

        public IRoomRepository Rooms
        {
            get
            {
                if(_roomRepository == null)
                {
                    _roomRepository = new RoomRepository(contextDB);
                }
                return _roomRepository;
            }
        }

        public void Dispose()
        {
            if (!Disposed)
            {
                contextDB.Dispose();
                Disposed = true;
            }
        }

        public void SaveChanges()
        {
            contextDB.SaveChanges();
        }
    }
}
