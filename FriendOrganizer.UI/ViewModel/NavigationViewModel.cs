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
            _eventAggregator.GetEvent<AfterFriendSaveEvent>().Subscribe(AfterFriendSave);
            _eventAggregator.GetEvent<AfterFriendDeleteEvent>().Subscribe(AfterFriendDelete);
        }

        public ObservableCollection<NagationItemViewModel> Friends { get; }

        public async Task LoadAsync()
        {
            var lookup = await _friendLookupDataService.GetFriendLookupAsync();
            Friends.Clear();
            foreach (var item in lookup)
                Friends.Add(new NagationItemViewModel(item.Id, item.DisplayMember, _eventAggregator));
        }

        private void AfterFriendSave(AfterSaveFriendEventArgs args)
        {
            var lookupItem = Friends.SingleOrDefault(l => l.Id == args.Id);
            if (lookupItem == null)
            {
                Friends.Add(new NagationItemViewModel(args.Id, args.DisplayMember, _eventAggregator));
            }
            else
            {
                lookupItem.DisplayMember = args.DisplayMember;
            }
        }

        private void AfterFriendDelete(int friendId)
        {
            var friend = Friends.SingleOrDefault(f => f.Id == friendId);
            if (friend != null)
            {
                Friends.Remove(friend);
            }
        }
    }
}