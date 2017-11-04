using Prism.Events;

namespace FriendOrganizer.UI.Event
{
    public class AfterDetailDeletedEvent : PubSubEvent<AfterDetailDeleteEventArgs>
    {
    }

    public class AfterDetailDeleteEventArgs
    {
        public int Id { get; set; }
        public string ViewModelName { get; set; }

    }
}