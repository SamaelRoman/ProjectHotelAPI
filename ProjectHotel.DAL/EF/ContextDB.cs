using Microsoft.EntityFrameworkCore;
using ProjectHotel.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectHotel.DAL.EF
{
    public class ContextDB : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeRole> EmployeeRoles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryInfo> CategoryInfos { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomImage> RoomImages { get; set; }
        public DbSet<BookingInfo> BookingInfos { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public ContextDB(string ConnectionString):base(GetOptions(ConnectionString))
        {

        }
        private static DbContextOptions GetOptions(string ConnectionString)
        {
            return SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder(), ConnectionString).Options;
        }
    }
}
