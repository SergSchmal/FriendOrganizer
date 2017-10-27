using System.Collections;
using System.Collections.Generic;
using FriendOrganizer.Model;

namespace FriendOrganizer.UI.Data
{
    public class FriendDataService : IFriendDataService
    {
        public IEnumerable<Friend> GetAll()
        {
            yield return new Friend{ FirstName = "Thomas", LastName = "Huber"};
            yield return new Friend{ FirstName = "Sergej", LastName = "Schmal"};
            yield return new Friend{ FirstName = "Christian", LastName = "Laux"};
            yield return new Friend{ FirstName = "Heiko", LastName = "Sturm"};
        }
    }
}