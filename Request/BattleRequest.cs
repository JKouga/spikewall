namespace spikewall.Request
{
    public class GetDailyBattleHistoryRequest : BaseRequest
    {
        public long count { get; set; }
    }

    public class ResetDailyBattleMatchingRequest : BaseRequest
    {
        public long Type { get; set; }
    }
}
