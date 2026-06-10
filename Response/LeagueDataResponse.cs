using spikewall.Object;

namespace spikewall.Response
{
    /// <summary>
    /// Response containing runners league data
    /// </summary>
    public class LeagueDataResponse : BaseResponse
    {
        public LeagueData leagueData { get; set; }
        public long mode { get; set; }

        public LeagueDataResponse()
        {
            this.leagueData = new LeagueData();
            this.mode = 0;
        }
    }
}
