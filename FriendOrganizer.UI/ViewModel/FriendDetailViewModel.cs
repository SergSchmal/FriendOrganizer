using System.Threading.Tasks;
using System.Windows.Input;
using FriendOrganizer.Model;
using FriendOrganizer.UI.Data.Repositories;
using FriendOrganizer.UI.Event;
using FriendOrganizer.UI.View.Services;
using FriendOrganizer.UI.Wrapper;
using Prism.Commands;
using Prism.Events;

namespace FriendOrganizer.UI.ViewModel
{
    public class FriendDetailViewModel : ViewModelBase, IFriendDetailViewModel
    {
        private readonly IFriendRepository _friendRepository;
        private readonly IEventAggregator _eventAggregator;
        private readonly IMessageDialogService _messageDialogService;
        private FriendWrapper _friend;
        private bool _hasChanges;

        public FriendDetailViewModel(IFriendRepository friendRepository, IEventAggregator eventAggregator, IMessageDialogService messageDialogService)
        {
            _friendRepository = friendRepository;
            _eventAggregator = eventAggregator;
            _messageDialogService = messageDialogService;
            SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanExecute);
            DeleteCommand = new DelegateCommand(OnDeleteExecute);
        }

        public FriendWrapper Friend
        {
            get => _friend;
            set
            {
                if (Equals(value, _friend)) return;
                _friend = value;
                OnPropertyChanged();
            }
        }

        public bool HasChanges
        {
            get { return _hasChanges; }
            set
            {
                if (value == _hasChanges) return;
                _hasChanges = value;
                OnPropertyChanged();
                ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
            }
        }

        public ICommand SaveCommand { get; }
        public ICommand DeleteCommand { get; }

        public async Task LoadAsync(int? friendId)
        {
            var friend = friendId.HasValue 
                ? await _friendRepository.GetByIdAsync(friendId.Value) 
                : CreateNewFriend();

            Friend = new FriendWrapper(friend);
            Friend.PropertyChanged += (s, e) =>
            {
                if (!HasChanges)
                {
                    HasChanges = _friendRepository.HasChanges();
                }
                if (e.PropertyName == nameof(Friend.HasErrors))
                {
                    ((DelegateCommand) SaveCommand).RaiseCanExecuteChanged();
                }
            };
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
            //Little trick to trigger the validation
            if (Friend.Id == 0)
            {
                Friend.FirstName = "";
            }
        }

        private bool OnSaveCanExecute()
        {
            return Friend != null && !Friend.HasErrors && HasChanges;
        }

        private async void OnSaveExecute()
        {
            await _friendRepository.SaveAsync();
            HasChanges = _friendRepository.HasChanges();
            _eventAggregator.GetEvent<AfterFriendSaveEvent>().Publish(
                new AfterSaveFriendEventArgs
                {
                    Id = Friend.Id,
                    DisplayMember = $"{Friend.FirstName} {Friend.LastName}"
                });
        }

        private Friend CreateNewFriend()
        {
            var friend = new Friend();
            _friendRepository.Add(friend);
            return friend;
        }

        private async void OnDeleteExecute()
        {
            var result = _messageDialogService.ShowOKCancelDialog($"Do you really want to delete the friend {Friend.FirstName} {Friend.LastName}",
                "Question");
            if (result == MessageDialogResult.OK)
            {
                _friendRepository.Remove(Friend.Model);
                await _friendRepository.SaveAsync();
                _eventAggregator.GetEvent<AfterFriendDeleteEvent>().Publish(Friend.Id);
            }
        }

    }
}