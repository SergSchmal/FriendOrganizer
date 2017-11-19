using System.Threading.Tasks;
using FriendOrganizer.UI.View.Services;
using Prism.Events;

namespace FriendOrganizer.UI.ViewModel
{
    public class ProgrammingLanguageDetailViewMOdel : DetailViewModelBase
    {
        public ProgrammingLanguageDetailViewMOdel(IEventAggregator eventAggregator, IMessageDialogService messageDialogService) : base(eventAggregator, messageDialogService)
        {
            Title = "Programming Languages";
        }

        protected override bool OnSaveCanExecute()
        {
            throw new System.NotImplementedException();
        }

        protected override void OnSaveExecute()
        {
            throw new System.NotImplementedException();
        }

        protected override void OnDeleteExecute()
        {
            throw new System.NotImplementedException();
        }

        public override Task LoadAsync(int id)
        {
            Id = id;
            return Task.Delay(0);
        }
    }
}