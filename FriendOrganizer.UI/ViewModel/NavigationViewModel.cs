using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using FriendOrganizer.Model;
using FriendOrganizer.UI.Data;
using FriendOrganizer.UI.Event;
using Prism.Events;

namespace FriendOrganizer.UI.ViewModel
{
    public class NavigationViewModel : ViewModelBase, INavigationViewModel
    {
        private readonly IFriendLookupDataService _friendLookupDataService;
        private readonly IEventAggregator _eventAggregator;
        private NagationItemViewModel _selectedFriend;

        public NavigationViewModel(IFriendLookupDataService friendLookupDataService, IEventAggregator eventAggregator)
        {
            _friendLookupDataService = friendLookupDataService;
            _eventAggregator = eventAggregator;
            Friends = new ObservableCollection<NagationItemViewModel>();
            _eventAggregator.GetEvent<AfterSaveFriendEvent>().Subscribe(AfterSaveFriend);
        }

        private async void AfterSaveFriend(AfterSaveFriendEventArgs args)
        {
            var lookupItem = Friends.Single(l => l.Id == args.Id);
            lookupItem.DisplayMember = args.DisplayMember;
        }

        public async Task LoadAsync()
        {
            var lookup = await _friendLookupDataService.GetFriendLookupAsync();
            Friends.Clear();
            foreach (var item in lookup)
            {
                Friends.Add(new NagationItemViewModel(item.Id, item.DisplayMember));
            }
        }

        public ObservableCollection<NagationItemViewModel> Friends { get; }

        public NagationItemViewModel SelectedFriend
        {
            get { return _selectedFriend; }
            set
            {
                if (Equals(value, _selectedFriend)) return;
                _selectedFriend = value;
                OnPropertyChanged();
                if (_selectedFriend != null)
                    _eventAggregator.GetEvent<OpenFriendDetailViewEvent>().Publish(_selectedFriend.Id);
            }
        }
    }
}