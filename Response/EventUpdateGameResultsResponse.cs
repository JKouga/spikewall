using spikewall.Object;

namespace spikewall.Response
{
    public class EventUpdateGameResultsResponse : BaseResponse
    {
        public PlayerState playerState {  get; set; }
        public Chao[] chaoState { get; set; }
        public Incentive[] DailyChallengeIncentive { get; set; }
        public Character[] characterState { get; set; }
        public Message[] MessageList { get; set; }
        public OperatorMessage[] OperatorMessageList { get; set; }
        public long TotalMessage { get; set; }
        public Character[] PlayCharacterState { get; set; }
        public Item[] EventIncentiveList { get; set; }
        public WheelOptions WheelOptions { get; set; }
        public EventState EventState { get; set; }
    }
}
