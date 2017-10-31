using System.ComponentModel.DataAnnotations;

namespace FriendOrganizer.Model
{
    public class FriendPhoneNumber
    {
        public int Id { get; set; }

        [Phone]
        [Required]
        public string Number { get; set; }

        public int FrinedId { get; set; }

        public Friend Friend { get; set; }
    }
}