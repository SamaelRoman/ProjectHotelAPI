using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectHotel.DAL.EF
{
    public class ContextDBFactory : IDesignTimeDbContextFactory<ContextDB>
    {
        public ContextDB CreateDbContext(string[] args)
        {
            return new ContextDB("Server=(localdb)\\MSSQLLocalDB;Database=ProjectHotelDB;Trusted_Connection=True;");
        }
    }
}
