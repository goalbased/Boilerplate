using System.Data.Entity;
using Bolierplate.Repository.Tables;

namespace Bolierplate.Repository
{
    public class BolierplateDbContext : DbContext
    {
        public BolierplateDbContext(): base("name=BolierplateDbContext")
        {
        }

        public DbSet<Test> Tests { get; set; }
        public virtual DbSet<TestsDetail> TestsDetails { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Test>()
                .HasMany(e => e.TestsDetails)
                .WithRequired(e => e.Test)
                .HasForeignKey(e => e.TestsId)
                .WillCascadeOnDelete(false);
        }
    }
}