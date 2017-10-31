using System.Windows.Input;
using FriendOrganizer.UI.Event;
using Prism.Commands;
using Prism.Events;

namespace FriendOrganizer.UI.ViewModel
{
    public class NagationItemViewModel : ViewModelBase
    {
        private readonly string _detailViewModelName;
        private readonly IEventAggregator _eventAggregator;
        private string _displayMember;

        public NagationItemViewModel(int id, string displayMember, string detailViewModelName, IEventAggregator eventAggregator)
        {
            _detailViewModelName = detailViewModelName;
            _eventAggregator = eventAggregator;
            Id = id;
            DisplayMember = displayMember;
            OpenDetailViewCommand = new DelegateCommand(OnOpenDetailViewExecute);
        }

        public int Id { get; }

        public string DisplayMember
        {
            get { return _displayMember; }
            set
            {
                if (value == _displayMember) return;
                _displayMember = value;
                OnPropertyChanged();
            }
        }

        public ICommand OpenDetailViewCommand { get; }

        private void OnOpenDetailViewExecute()
        {
            _eventAggregator.GetEvent<OpenDetailViewEvent>().Publish(new OpenDetailViewEventArgs{ Id = Id, ViewModelName = _detailViewModelName });
        }
    }
}