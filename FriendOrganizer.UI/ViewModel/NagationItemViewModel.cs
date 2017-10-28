namespace FriendOrganizer.UI.ViewModel
{
    public class NagationItemViewModel : ViewModelBase
    {
        private string _displayMember;

        public NagationItemViewModel(int id, string displayMember)
        {
            Id = id;
            DisplayMember = displayMember;
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
    }
}