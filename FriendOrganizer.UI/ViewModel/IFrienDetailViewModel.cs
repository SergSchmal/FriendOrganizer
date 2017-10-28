using System.Threading.Tasks;

namespace FriendOrganizer.UI.ViewModel
{
    public interface IFrienDetailViewModel
    {
        Task LoadAsync(int friendId);
    }
}