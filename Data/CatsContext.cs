using System.Data.Entity;
using Cats.Models;
using Cats.Models.Mapping;

namespace Cats.Data
{
    public partial class CatsContext : DbContext
    {
        static CatsContext()
        {
            Database.SetInitializer<CatsContext>(null);
        }

        public CatsContext() : base("Name=CatsContext") { }

        // TODO: Add properties to access set of Poco classes
        public DbSet<RegionalRequest> RegionalRequests { get; set; }
        public DbSet<RegionalRequestDetail> RegionalRequestDetails { get; set; }
        public DbSet<ReliefRequisition> ReliefRequisitions { get; set; }
        public DbSet<ReliefRequisitionDetail> ReliefRequisitionDetails { get; set; }
        public DbSet<AdminUnit> AdminUnits { get; set; }
        public DbSet<Commodity> Commodities { get; set; }
        public DbSet<CommodityType> CommodityTypes { get; set; }
        public DbSet<FDP> Fdps { get; set; }
        public DbSet<Program> Programs { get; set; }
        public DbSet<AdminUnitType> AdminUnitTypes { get; set; }
        public DbSet<Hub> Hubs { get; set; }

        //public DbSet<OrderDeatil> OrderDeatils { get; set; }
        //public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //TODO: Add mapping information for each Poco model.
            modelBuilder.Configurations.Add(new RegionalRequestMap());
            modelBuilder.Configurations.Add(new RegionalRequestDetailMap());
            modelBuilder.Configurations.Add(new ReliefRequisitionMap());
            modelBuilder.Configurations.Add(new ReliefRequisitionDetailMap());
            modelBuilder.Configurations.Add(new AdminUnitMap());
            modelBuilder.Configurations.Add(new CommodityMap());
            modelBuilder.Configurations.Add(new CommodityTypeMap());
            modelBuilder.Configurations.Add(new FDPMap());
            modelBuilder.Configurations.Add(new ProgramMap());
            modelBuilder.Configurations.Add(new AdminUnitTypeMap());
            //modelBuilder.Configurations.Add(new OrderDeatilMap());
            //modelBuilder.Configurations.Add(new ProductMap());

        }
    }
}
