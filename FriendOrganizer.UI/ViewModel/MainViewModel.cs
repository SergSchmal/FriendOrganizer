using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Autofac.Features.Indexed;
using FriendOrganizer.UI.Event;
using FriendOrganizer.UI.View.Services;
using Prism.Commands;
using Prism.Events;

namespace FriendOrganizer.UI.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IIndex<string, IDetailViewModel> _detailViewModelCreator;
        private readonly IMessageDialogService _messageDialogService;
        private IDetailViewModel _detailViewModel;

        public MainViewModel(INavigationViewModel navigationViewModel, 
            IIndex<string, IDetailViewModel> detailViewModelCreator,
            IEventAggregator eventAggregator, IMessageDialogService messageDialogService)
        {
            _detailViewModelCreator = detailViewModelCreator;
            _messageDialogService = messageDialogService;
            var eventAggregator1 = eventAggregator;
            eventAggregator1.GetEvent<OpenDetailViewEvent>().Subscribe(OnOpenDetailView);
            eventAggregator1.GetEvent<AfterDetailDeletedEvent>().Subscribe(AfterDetailDeleted);
            NavigationViewModel = navigationViewModel;
            CreateNewDetailCommand = new DelegateCommand<Type>(OnCreateNewDetailExecute);
        }

        public async Task LoadAsync()
        {
            await NavigationViewModel.LoadAsync(); 
        }

        public ICommand CreateNewDetailCommand { get; }

        public INavigationViewModel NavigationViewModel { get; }

        public IDetailViewModel DetailViewModel
        {
            get { return _detailViewModel; }
            set
            {
                if (Equals(value, _detailViewModel)) return;
                _detailViewModel = value;
                OnPropertyChanged();
            }
        }

        private async void OnOpenDetailView(OpenDetailViewEventArgs args)
        {
            if (DetailViewModel != null && DetailViewModel.HasChanges)
            {
                var result = _messageDialogService.ShowOKCancelDialog("You've made changes. Navigate away?", "Question");
                if (result == MessageDialogResult.Cancel) return;
            }
            DetailViewModel = _detailViewModelCreator[args.ViewModelName];
            await DetailViewModel.LoadAsync(args.Id);
        }

        private void OnCreateNewDetailExecute(Type viewModelType)
        {
            OnOpenDetailView(new OpenDetailViewEventArgs {ViewModelName = viewModelType.Name});
        }

        private void AfterDetailDeleted(AfterDetailDeleteEventArgs args)
        {
            DetailViewModel = null;
        }
    }
}