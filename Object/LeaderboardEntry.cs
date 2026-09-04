using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;
using spikewall.Languages;
using spikewall.Request;
using spikewall.Response;

namespace spikewall.Object
{
    public class LeaderboardEntry
    {
        public string? friendId { get; set; }
        public string? name { get; set; }
        public string? url { get; set; }
        public long? grade { get; set; }
        public long? exposeOnline { get; set; }
        public ulong? rankingScore { get; set; }
        public long? rankChanged { get; set; }
        public long? energyFlg { get; set; }
        public long? expireTime { get; set; }
        public long? numRank { get; set; }
        public long? loginTime { get; set; }
        public string? charaId { get; set; }
        public long? characterLevel { get; set; }
        public string? subCharaId { get; set; }
        public long? subCharaLevel { get; set; }
        public string? mainChaoId { get; set; }
        public long? mainChaoLevel { get; set; }
        public string? subChaoId { get; set; }
        public long? subChaoLevel { get; set; }
        public long? language { get; set; }
        public ulong? league { get; set; }
        public ulong? maxScore { get; set; }

        public static LeaderboardEntry PlayerToLeaderboardEntry(Player player, long mode)
        {
            var friendID = player.ID;
            var name = player.Username;
            var url = player.ID + "_findme";
            var grade = 1;
            var exposeOnline = 0;
            ulong rankingScore = 0;
            var rankChanged = 0;
            var energyFlg = 0;
            var expireTime = Convert.ToInt64(DateTime.Now.AddDays(7));
            var numRank = player.PlayerState.numRank;
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

            ulong league = 0;
            ulong maxScore = 0;
            if (mode == 0)
            {
                rankingScore = Convert.ToUInt64(player.PlayerState.totalHighScore);
                league = (ulong)player.PlayerState.rankingLeague;
                maxScore = (ulong)player.PlayerState.totalHighScore;
            }
            if (mode == 1)
            {
                rankingScore = Convert.ToUInt64(player.PlayerState.quickTotalHighScore);
                league = (ulong)player.PlayerState.quickRankingLeague;
                maxScore = (ulong)player.PlayerState.quickTotalHighScore;
            }
            LeaderboardEntry leaderboardEntry = new LeaderboardEntry()
            {
                friendId = friendID,
                name = name,
                url = url,
                grade = grade,
                exposeOnline = exposeOnline,
                rankingScore = rankingScore,
                rankChanged = rankChanged,
                energyFlg = energyFlg,
                expireTime = expireTime,
                numRank = numRank,
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
            return leaderboardEntry;
        }
    }
}
