using System.Data.Entity.Migrations;
using System.Linq;
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
            context.ProgrammingLanguages.AddOrUpdate(
                pl => pl.Name,
                new ProgrammingLanguage{ Name = "C#"},
                new ProgrammingLanguage{ Name = "TypeScript"},
                new ProgrammingLanguage{ Name = "F#"},
                new ProgrammingLanguage{ Name = "Swift"},
                new ProgrammingLanguage{ Name = "Java"});

            context.SaveChanges();

            context.FriendPhoneNumbers.AddOrUpdate(pn => pn.Number,
                new FriendPhoneNumber {Number = "+49 12345678", FrinedId = context.Friends.First().Id});
        }
    }
}
