using spikewall.Object;

namespace spikewall.Response
{
    public class EventUserRaidBossListResponse : BaseResponse
    {
        public EventRaidBossState[] EventRaidbossStates { get; set; }
    }
}
