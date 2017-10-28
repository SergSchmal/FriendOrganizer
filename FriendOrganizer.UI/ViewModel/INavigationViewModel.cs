using System.Collections.ObjectModel;
using System.Threading.Tasks;
using FriendOrganizer.Model;

namespace FriendOrganizer.UI.ViewModel
{
    public interface INavigationViewModel
    {
        Task LoadAsync();
    }
}