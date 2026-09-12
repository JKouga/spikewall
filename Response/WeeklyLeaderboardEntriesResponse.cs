using spikewall.Object;

namespace spikewall.Response
{
    /// <summary>
    /// Response that records any runs into the Runners League in either Endless or Quick Mode; 
    /// records highest score in one run and total score after a certain number of runs
    /// </summary>
    public class WeeklyLeaderboardEntriesResponse : BaseResponse
    {
        public LeaderboardEntry? playerEntry { get; set; }
        public long? lastOffset { get; set; }
        public long? startTime { get; set; }
        public long? resetTime { get; set; }
        public long? startIndex { get; set; }
        public long? mode { get; set; }
        public long? totalEntries { get; set; }
        public LeaderboardEntry[]? entriesList { get; set; }
    }
}
