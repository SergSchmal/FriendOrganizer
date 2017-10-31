using System;
using System.Threading.Tasks;
using System.Windows.Input;
using FriendOrganizer.UI.Event;
using FriendOrganizer.UI.View.Services;
using Prism.Commands;
using Prism.Events;

namespace FriendOrganizer.UI.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IMessageDialogService _messageDialogService;
        private Func<IFriendDetailViewModel> _friendDetailViewModelCreator;
        private IFriendDetailViewModel _friendDetailViewModel;

        public MainViewModel(INavigationViewModel navigationViewModel, Func<IFriendDetailViewModel> friendDetailViewModelCreator, 
            IEventAggregator eventAggregator, IMessageDialogService messageDialogService)
        {
            _eventAggregator = eventAggregator;
            _messageDialogService = messageDialogService;
            _friendDetailViewModelCreator = friendDetailViewModelCreator;
            _eventAggregator.GetEvent<OpenFriendDetailViewEvent>().Subscribe(OnOpenFriend);
            _eventAggregator.GetEvent<AfterFriendDeleteEvent>().Subscribe(AfterFriendDelete);
            NavigationViewModel = navigationViewModel;
            CreateNewFriendCommand = new DelegateCommand(OnCreateNewFriendExecute);
        }

        public async Task LoadAsync()
        {
            await NavigationViewModel.LoadAsync(); 
        }

        public ICommand CreateNewFriendCommand { get; }

        public INavigationViewModel NavigationViewModel { get; }

        public IFriendDetailViewModel FriendDetailViewModel
        {
            get { return _friendDetailViewModel; }
            set
            {
                if (Equals(value, _friendDetailViewModel)) return;
                _friendDetailViewModel = value;
                OnPropertyChanged();
            }
        }

        private async void OnOpenFriend(int? friendId)
        {
            if (FriendDetailViewModel != null && FriendDetailViewModel.HasChanges)
            {
                var result = _messageDialogService.ShowOKCancelDialog("You've made changes. Navigate away?", "Question");
                if (result == MessageDialogResult.Cancel) return;
            }
            FriendDetailViewModel = _friendDetailViewModelCreator();
            await FriendDetailViewModel.LoadAsync(friendId);
        }

        private void OnCreateNewFriendExecute()
        {
             OnOpenFriend(null);
        }

        private void AfterFriendDelete(int friendId)
        {
            FriendDetailViewModel = null;
        }
    }
}