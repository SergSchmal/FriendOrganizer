using System.Threading.Tasks;

namespace FriendOrganizer.UI.View.Services
{
    public interface IMessageDialogService
    {
        Task ShowInfoDialogAsync(string text);
        Task<MessageDialogResult> ShowOKCancelDialogAsync(string text, string title);
    }
}