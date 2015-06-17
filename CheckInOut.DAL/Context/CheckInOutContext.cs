using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using CheckInOut.DAL.Migrations;
using CheckInOut.DAL.Models;

namespace CheckInOut.DAL.Context
{
    public class CheckInOutContext: DbContext
    {
        public CheckInOutContext() : base("CheckInOutContext")
        {
        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<VisitorType> VisitorTypes { get; set; }
        public DbSet<Visitor> Visitors { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<DeviceVisitorType> DeviceVisitorTypes { get; set; }
        public DbSet<DataField> DataFields { get; set; }
        public DbSet<DeviceVisitorTypeDataField> DeviceVisitorTypeDataFields { get; set; }
        public DbSet<TrxVisit> TrxVisits { get; set; }
        public DbSet<ConfigurationEmail> ConfigurationEmails { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
