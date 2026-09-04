using spikewall.Languages;
using System.Xml.Linq;

namespace spikewall.Object
{
    public class BattleData
    {
        public string? friendId { get; set; }
        public string? name { get; set; }
        public long? maxScore { get; set; }
        public long? league { get; set; }
        public long? loginTime { get; set; }
        public string? mainChaoId { get; set; }
        public long? mainChaoLevel { get; set; }
        public string? subChaoId { get; set; }
        public long? subChaoLevel { get; set; }
        public long? numRank { get; set; }
        public string? charaId { get; set; }
        public long? characterLevel { get; set; }
        public string? subCharaId { get; set; }
        public long? subCharaLevel { get; set; }
        public long? winStreak { get; set; }
        public bool? isEnergySent { get; set; }
        public long? language { get; set; }

        public static BattleData ConvertPlayerToBattleData(Player player)
        {
            var friendID = player.ID;
            var username = player.Username;
            long maxScore = 0;
            var league = player.PlayerState.quickRankingLeague;
            var loginTime = player.LastLogin;
            var mainCharaID = player.PlayerState.mainCharaID.ToString();
            var mainCharaLevel = 0;
            var subCharaID = player.PlayerState.subCharaID.ToString();
            var subCharaLevel = 0;
            var mainChaoID = player.PlayerState.mainChaoID.ToString();
            var mainChaoLevel = 0;
            var subChaoID = player.PlayerState.subChaoID.ToString();
            var subChaoLevel = 0;
            var language = Convert.ToInt64(Language.English);
            var rank = player.PlayerState.numRank;
            var winStreak = 0;
            var isEnergySent = 0;

            if (Character.FindCharacterInCharacterState(Convert.ToInt32(mainCharaID), player.CharacterState) != -1)
            {
                mainCharaLevel = player.CharacterState[Character.FindCharacterInCharacterState(Convert.ToInt32(mainCharaID), player.CharacterState)].level;
            }
            if (Character.FindCharacterInCharacterState(Convert.ToInt32(subCharaID), player.CharacterState) != -1)
            {
                mainCharaLevel = player.CharacterState[Character.FindCharacterInCharacterState(Convert.ToInt32(subCharaID), player.CharacterState)].level;
            }
            if (Chao.FindChaoInChaoState(Convert.ToInt32(mainChaoID), player.ChaoState) != -1)
            {
                mainChaoLevel = Convert.ToSByte(player.ChaoState[Chao.FindChaoInChaoState(Convert.ToInt32(mainChaoID), player.ChaoState)].level);
            }
            if (Chao.FindChaoInChaoState(Convert.ToInt32(subChaoID), player.ChaoState) != -1)
            {
                subChaoLevel = Convert.ToSByte(player.ChaoState[Chao.FindChaoInChaoState(Convert.ToInt32(mainChaoID), player.ChaoState)].level);
            }

            BattleData battleData = new BattleData()
            {
                friendId = friendID,
                name = username,
                numRank = rank,
                loginTime = loginTime,
                charaId = mainCharaID,
                characterLevel = mainCharaLevel,
                subCharaId = subCharaID,
                subCharaLevel = subCharaLevel,
                mainChaoId = mainChaoID,
                mainChaoLevel = mainChaoLevel,
                subChaoId = subChaoID,
                subChaoLevel = subChaoLevel,
                language = language,
                league = league,
                maxScore = maxScore
            };

            return battleData;
        }
    }

    public class BattlePair
    {
        public long? StartTime { get; set; }
        public long? EndTime { get; set; }
        public BattleData? BattleData { get; set; }
        public BattleData? RivalBattleData { get; set; }
    }
    public class RewardBattlePair : BattlePair
    {

    }

    public class BattleState
    {
        public bool? ScoreRecordedToday { get; set; }
        public long? DailyBattleHighScore { get; set; }
        public long? PrevDailyBattleHighScore { get; set; }
        public long? BattleStart { get; set; }
        public long? BattleEnd { get; set; }
        public bool? MatchedWithRival { get; set; }
        public string? RivalID { get; set; }
        public long? Wins { get; set; }
        public long? Losses { get; set; }
        public long? Failures { get; set; }
        public long? WinStreak { get; set; }
        public long? LossStreak { get; set; }
        public BattlePair[]? BattleHistory { get; set; }
        public bool? PendingReward { get; set; }
        public RewardBattlePair? PendingRewardData { get; set; }
    }

    public class BattleStatus
    {
        public long? Wins { get; set; }
        public long? Losses { get; set; }
        public long? Ties { get; set; }
        public long? Failures { get; set; }
        public long? WinStreak { get; set; }
        public long? LossStreak { get; set; }
    }
}
