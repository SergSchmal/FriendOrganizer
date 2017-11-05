using System;
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
        private readonly IMeetingLookupDataService _meetingLookupDataService;

        public NavigationViewModel(IFriendLookupDataService friendLookupDataService, IMeetingLookupDataService meetingLookupDataService, IEventAggregator eventAggregator)
        {
            _friendLookupDataService = friendLookupDataService;
            _meetingLookupDataService = meetingLookupDataService;
            _eventAggregator = eventAggregator;
            Friends = new ObservableCollection<NagationItemViewModel>();
            Meetings = new ObservableCollection<NagationItemViewModel>();
            _eventAggregator.GetEvent<AfterDetailSavedEvent>().Subscribe(AfterDetailSaved);
            _eventAggregator.GetEvent<AfterDetailDeletedEvent>().Subscribe(AfterDetailDeleted);
        }

        public ObservableCollection<NagationItemViewModel> Friends { get; }
        public ObservableCollection<NagationItemViewModel> Meetings { get; }

        public async Task LoadAsync()
        {
            var lookup = await _friendLookupDataService.GetFriendLookupAsync();
            Friends.Clear();
            foreach (var item in lookup)
                Friends.Add(new NagationItemViewModel(item.Id, item.DisplayMember, nameof(FriendDetailViewModel), _eventAggregator));
            lookup = await _meetingLookupDataService.GetMeetingLookupAsync();
            Meetings.Clear();
            foreach (var item in lookup)
                Meetings.Add(new NagationItemViewModel(item.Id, item.DisplayMember, nameof(MeetingDetailViewModel), _eventAggregator));
        }

        private void AfterDetailSaved(AfterDetailSaveEventArgs args)
        {
            switch (args.ViewModelName)
            {
                case nameof(FriendDetailViewModel):
                    AfterDetailSaved(Friends, args);
                    break;
                case nameof(MeetingDetailViewModel):
                    AfterDetailSaved(Meetings, args);
                    break;
            }
        }

        private void AfterDetailSaved(ObservableCollection<NagationItemViewModel> items, AfterDetailSaveEventArgs args)
        {
            var lookupItem = items.SingleOrDefault(l => l.Id == args.Id);
            if (lookupItem == null)
            {
                items.Add(new NagationItemViewModel(args.Id, args.DisplayMember, 
                    args.ViewModelName,
                    _eventAggregator));
            }
            else
            {
                lookupItem.DisplayMember = args.DisplayMember;
            }
        }

        private void AfterDetailDeleted(AfterDetailDeleteEventArgs args)
        {
            switch (args.ViewModelName)
            {
                case nameof(FriendDetailViewModel):
                    AfterDetailDeleted(Friends, args);
                    break;
                case nameof(MeetingDetailViewModel):
                    AfterDetailDeleted(Meetings, args);
                    break;
            }
        }

        private void AfterDetailDeleted(ObservableCollection<NagationItemViewModel> items, AfterDetailDeleteEventArgs args)
        {
            var item = items.SingleOrDefault(f => f.Id == args.Id);
            if (item != null)
            {
                items.Remove(item);
            }
        }
    }
}