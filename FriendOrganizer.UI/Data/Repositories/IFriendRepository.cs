using System.Threading.Tasks;
using FriendOrganizer.Model;

namespace FriendOrganizer.UI.Data.Repositories
{
    public interface IFriendRepository : IGenericRepository<Friend>
    {
        Task<bool> HasMeetingsAsync(int friendId);
        void RemovePhoneNumber(FriendPhoneNumber model);
    }
}