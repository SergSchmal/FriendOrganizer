using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using FriendOrganizer.UI.Data.Lookups;
using FriendOrganizer.UI.Event;
using Prism.Events;

namespace FriendOrganizer.UI.ViewModel
{
    public class NavigationViewModel : ViewModelBase, INavigationViewModel
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IFriendLookupDataService _friendLookupDataService;

        public NavigationViewModel(IFriendLookupDataService friendLookupDataService, IEventAggregator eventAggregator)
        {
            _friendLookupDataService = friendLookupDataService;
            _eventAggregator = eventAggregator;
            Friends = new ObservableCollection<NagationItemViewModel>();
            _eventAggregator.GetEvent<AfterSaveFriendEvent>().Subscribe(AfterSaveFriend);
        }

        public ObservableCollection<NagationItemViewModel> Friends { get; }

        public async Task LoadAsync()
        {
            var lookup = await _friendLookupDataService.GetFriendLookupAsync();
            Friends.Clear();
            foreach (var item in lookup)
                Friends.Add(new NagationItemViewModel(item.Id, item.DisplayMember, _eventAggregator));
        }

        private async void AfterSaveFriend(AfterSaveFriendEventArgs args)
        {
            var lookupItem = Friends.Single(l => l.Id == args.Id);
            lookupItem.DisplayMember = args.DisplayMember;
        }
    }
}