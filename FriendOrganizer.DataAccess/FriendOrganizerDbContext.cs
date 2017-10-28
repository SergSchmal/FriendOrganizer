using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Conventions;
using FriendOrganizer.Model;

namespace FriendOrganizer.DataAccess
{
    public class FriendOrganizerDbContext : DbContext
    {
        public FriendOrganizerDbContext() : base("FriendOrganizerDb")
        {
            
        }

        public DbSet<Friend> Friends { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingEntitySetNameConvention>();

            //modelBuilder.Configurations.Add(new FriendConfiguration());
        }

        //public class FriendConfiguration : EntityTypeConfiguration<Friend>
        //{
        //    public FriendConfiguration()
        //    {
        //        Property(f => f.FirstName).IsRequired().HasMaxLength(50);
        //    }
        //}
    }
}