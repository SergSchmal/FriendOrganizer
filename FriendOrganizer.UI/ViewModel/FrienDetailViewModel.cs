using System.Threading.Tasks;
using FriendOrganizer.Model;
using FriendOrganizer.UI.Data;

namespace FriendOrganizer.UI.ViewModel
{
    public class FrienDetailViewModel : ViewModelBase, IFrienDetailViewModel
    {
        private readonly IFriendDataService _dataService;
        private Friend _friend;

        public FrienDetailViewModel(IFriendDataService dataService)
        {
            _dataService = dataService;
        }

        public async Task LoadAsync(int friendId)
        {
            Friend = await _dataService.GetByIdAsync(friendId);
        }

        public Friend Friend
        {
            get { return _friend; }
            set
            {
                if (Equals(value, _friend)) return;
                _friend = value;
                OnPropertyChanged();
            }
        }
    }
}