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
        public DbSet<ReliefRequistion> ReliefRequistions { get; set; }
        public DbSet<ReliefRequisitionDetail> ReliefRequisitionDetails { get; set; }
        public DbSet<Round> Rounds { get; set; }
        //public DbSet<OrderDeatil> OrderDeatils { get; set; }
        //public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //TODO: Add mapping information for each Poco model.
            modelBuilder.Configurations.Add(new ReliefRequistionMap());
            modelBuilder.Configurations.Add(new ReliefRequisitionDetailMap());
            modelBuilder.Configurations.Add(new RoundMap());
            //modelBuilder.Configurations.Add(new OrderDeatilMap());
            //modelBuilder.Configurations.Add(new ProductMap());

        }
    }
}
