using Prism.Events;

namespace FriendOrganizer.UI.Event
{
    public class AfterDetailDeleteEvent : PubSubEvent<AfterDetailDeleteEventArgs>
    {
    }

    public class AfterDetailDeleteEventArgs
    {
        public int Id { get; set; }
        public string ViewModelName { get; set; }

    }
}