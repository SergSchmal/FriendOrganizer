using Prism.Events;

namespace FriendOrganizer.UI.Event
{
    public class AfterFriendSaveEvent : PubSubEvent<AfterSaveFriendEventArgs>
    {
        
    }

    public class AfterSaveFriendEventArgs
    {
        public int Id { get; set; }
        public string DisplayMember { get; set; }
    }
}