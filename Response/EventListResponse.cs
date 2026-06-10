using spikewall.Object;

namespace spikewall.Response
{
    /// <summary>
    /// Response containing event list (FIXME: Elaborate)
    /// </summary>
    public class EventListResponse : BaseResponse
    {
        public Event[]? eventList { get; set; }

        public EventListResponse()
        {
            this.eventList = Array.Empty<Event>();
        }
    }
}
