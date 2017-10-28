using System.Collections.ObjectModel;
using System.Threading.Tasks;
using FriendOrganizer.Model;
using FriendOrganizer.UI.Data;

namespace FriendOrganizer.UI.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel(INavigationViewModel navigationViewModel, IFrienDetailViewModel frienDetailViewModel)
        {
            NavigationViewModel = navigationViewModel;
            FrienDetailViewModel = frienDetailViewModel;
        }


        public async Task LoadAsync()
        {
            await NavigationViewModel.LoadAsync(); 
        }

        public INavigationViewModel NavigationViewModel { get; }
        public IFrienDetailViewModel FrienDetailViewModel { get; }
    }
}