using spikewall.Object;

namespace spikewall.Response
{
    public class LeagueOperatorDataResponse : BaseResponse
    {
        public LeagueData[] LeagueList;
        public long LeagueID;

        public LeagueOperatorDataResponse()
        {
            LeagueList = new LeagueData[21];
            LeagueID = LeagueID;
        }
    }
}
