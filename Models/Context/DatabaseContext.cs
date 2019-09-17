using AlpataProje.Models.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AlpataProje.Models.Context
{
    [DbConfigurationType(typeof(EntityFrameworkDbConfiguration))]
    public class DatabaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<AuthorizedService> AuthorizedServices { get; set; }
        public DbSet<CareRequests> CareRequests { get; set; }

        public DatabaseContext() : base("Alpata_VehicleCare") // Database Name
        {

        }
    }
}