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

        // FIXME: This is an array but shouldn't actually be strings, set up "LeaderboardEntry" object
        public LeaderboardEntry[]? entriesList { get; set; }

        public WeeklyLeaderboardEntriesResponse()
        {
            Player player = new();
            this.playerEntry = LeaderboardEntry.PlayerToLeaderboardEntry(player, (long)mode);
            this.lastOffset = 0;
            this.startTime = 0;
            this.resetTime = 0;
            this.startIndex = 0;
            this.mode = 0;
            this.totalEntries = 0;
            this.entriesList = Array.Empty<LeaderboardEntry>();
        }
    }
}
