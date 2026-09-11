namespace spikewall.Response
{
    /// <summary>
    /// Response that displays the duration of the Runners League time until Runners League Calculations begin
    /// </summary>
    public class WeeklyLeaderboardOptionsResponse : BaseResponse
    {
        public long? mode { get; set; }
        public long? type { get; set; }
        public long? param { get; set; }
        public long? startTime { get; set; }
        public long? resetTime { get; set; }
    }
}
