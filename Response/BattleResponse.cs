using spikewall.Object;

namespace spikewall.Response
{
    public class BattleDataNoScoreResponse : BaseResponse
    {
        public long startTime { get; set; }
        public long endTime { get; set; }
    }
    public class BattleDataNoRivalResponse : BaseResponse
    {
        public long startTime { get; set; }
        public long endTime { get; set; }
        public BattleData battleData { get; set; }
    }

    public class DailyBattleDataResponse : BaseResponse
    {
        public BattlePair battlePair { get; set; }
    }

    public class UpdateDailyBattleStatusResponse : BaseResponse
    {
        public long endTime { get; set; }
        public BattleStatus battleStatus { get; set; }
        public bool rewardFlag { get; set; }
    }

    public class UpdateDailyBattleStatusWithRewardResponse : BaseResponse
    {
        public long endTime { get; set; }
        public BattleStatus battleStatus { get; set; }
        public bool rewardFlag { get; set; }
        public RewardBattlePair rewardBattlePair { get; set; }
    }
    public class UpdateDailyBattleStatusWithRewardNoRivalResponse : BaseResponse
    {
        public long endTime { get; set; }
        public BattleData battleData { get; set; }
        public BattleStatus battleStatus { get; set; }
        public bool rewardFlag { get; set; }
        public long rewardStartTime { get; set; }
        public long rewardEndTime { get; set; }
    }
    public class UpdateDailyBattleStatusWithRewardNoDataResponse : BaseResponse
    {
        public long endTime { get; set; }
        public BattleStatus battleStatus { get; set; }
        public bool rewardFlag { get; set; }
        public long rewardStartTime { get; set; }
        public long rewardEndTime { get; set; }
    }

    public class ResetDailyBattleMatchingResponse : BaseResponse
    {
        public BattlePair battlePair { get; set; }
        public PlayerState playerState { get; set; }
    }

    public class GetDailyBattleHistoryResponse : BaseResponse
    {
        public BattlePair[] battleDataList { get; set; }
    }

    public class GetDailyBattleStatusResponse : BaseResponse
    {
        public long endTime { get; set; }
        public BattleStatus battleStatus { get; set; }
    }

    public class PostDailyBattleResultResponse : BaseResponse
    {
        public BattlePair battlePair { get; set; }
        public BattleStatus battleStatus { get; set; }
        public bool rewardFlag { get; set; }
    }
    public class PostDailyBattleResultNoRivalResponse : BaseResponse
    {
        public long startTime { get; set; }
        public long endTime { get; set; }
        public BattlePair battlePair { get; set; }
        public BattleStatus battleStatus { get; set; }
        public bool rewardFlag { get; set; }
    }
    public class PostDailyBattleResultNoDataResponse : BaseResponse
    {
        public long startTime { get; set; }
        public long endTime { get; set; }
        public BattleStatus battleStatus { get; set; }
        public bool rewardFlag { get; set; }
        public long rewardStartTime { get; set; }
        public long rewardEndTime { get; set; }
    }

    public class PostDailyBattleResultWithRewardResponse : BaseResponse
    {
        public long startTime { get; set; }
        public long endTime { get; set; }
        public BattlePair battlePair { get; set; }
        public BattleStatus battleStatus { get; set; }
        public bool rewardFlag { get; set; }
    }
    public class PostDailyBattleResultWithRewardNoRivalResponse : BaseResponse
    {
        public long startTime { get; set; }
        public long endTime { get; set; }
        public BattlePair battlePair { get; set; }
        public BattleStatus battleStatus { get; set; }
        public bool rewardFlag { get; set; }
        public long rewardStartTime { get; set; }
        public long rewardEndTime { get; set; }
    }
    public class PostDailyBattleResultWithRewardNoDataResponse : BaseResponse
    {
        public long startTime { get; set; }
        public long endTime { get; set; }
        public BattleStatus battleStatus { get; set; }
        public bool rewardFlag { get; set; }
        public long rewardStartTime { get; set; }
        public long rewardEndTime { get; set; }
    }

    public class GetDailyBattlePrizeResponse : BaseResponse
    {
        public OperatorScore[] dailyBattlePrizeList { get; set; }
    }

}
