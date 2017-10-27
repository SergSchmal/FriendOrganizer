using System.Data.Entity.Migrations;
using FriendOrganizer.Model;

namespace FriendOrganizer.DataAccess.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<FriendOrganizerDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(FriendOrganizerDbContext context)
        {
            context.Friends.AddOrUpdate(
                f => f.FirstName,
                new Friend {FirstName = "Thomas", LastName = "Huber"},
                new Friend {FirstName = "Sergej", LastName = "Schmal"},
                new Friend {FirstName = "Christian", LastName = "Laux"},
                new Friend {FirstName = "Heiko", LastName = "Sturm"}
            );
        }
    }
}
