using MySqlConnector;
using static spikewall.Object.LeagueData;

namespace spikewall.Object
{
    public class OperatorScore
    {
        public ulong Operator { get; set; } //In the server, this will tell whether the prizes are in a range (2) or just singluar (0)
        public ulong Number { get; set; } //This is the placement of the League within your group; for Daily Battles, it's based on your win streak
        public Item[] PresentList { get; set; } //This is the prize you get based on the number you are within your group; for Daily Battles, it's based on your win streak

        public static OperatorScore[] GenerateEndlessLeaguePrizes(MySqlConnection conn, ref LeagueID leagueID)
        {
            var generateEndlessLeaguePrizesSql = Db.GetCommand(@"SELECT * FROM `sw_endlessleaguehighscoreprizes` WHERE league_id = '{0}'", leagueID);
            var generateEndlessLeaguePrizesCmd = new MySqlCommand(generateEndlessLeaguePrizesSql, conn);
            var generateEndlessLeaguePrizesRdr = generateEndlessLeaguePrizesCmd.ExecuteReader();

            List<OperatorScore> endlessLeaguePrizeList = new List<OperatorScore>();
            List<Item> itemPrizeList = new List<Item>();

            if (generateEndlessLeaguePrizesRdr.HasRows)
            {
                generateEndlessLeaguePrizesRdr.Read();

                Item item = new()
                {
                    itemId = Convert.ToInt32(generateEndlessLeaguePrizesRdr["item"]),
                    numItem = Convert.ToInt64(generateEndlessLeaguePrizesRdr["item_count"])
                };

                itemPrizeList.Add(item);

                OperatorScore operatorScore = new()
                {
                    Operator = Convert.ToUInt64(generateEndlessLeaguePrizesRdr["operator"]),
                    Number = Convert.ToUInt64(generateEndlessLeaguePrizesRdr["number"]),
                    PresentList = itemPrizeList.ToArray()
                };

                endlessLeaguePrizeList.Add(operatorScore);

            }
            generateEndlessLeaguePrizesRdr.Close();

            OperatorScore[] endlessLeaguePrizeArray = endlessLeaguePrizeList.ToArray();
            return endlessLeaguePrizeArray;
        }
        public static OperatorScore[] GenerateQuickLeaguePrizes(MySqlConnection conn, ref LeagueID leagueID)
        {
            var generateQuickLeaguePrizesSql = Db.GetCommand(@"SELECT * FROM `sw_quickleaguehighscoreprizes` WHERE league_id = '{0}'", leagueID);
            var generateQuickLeaguePrizesCmd = new MySqlCommand(generateQuickLeaguePrizesSql, conn);
            var generateQuickLeaguePrizesRdr = generateQuickLeaguePrizesCmd.ExecuteReader();

            List<OperatorScore> quickLeaguePrizeList = new List<OperatorScore>();
            List<Item> itemPrizeList = new List<Item>();

            if (generateQuickLeaguePrizesRdr.HasRows)
            {
                
                generateQuickLeaguePrizesRdr.Read();

                Item item = new()
                {
                    itemId = Convert.ToInt32(generateQuickLeaguePrizesRdr["item"]),
                    numItem = Convert.ToInt64(generateQuickLeaguePrizesRdr["item_count"])
                };

                itemPrizeList.Add(item);

                OperatorScore operatorScore = new()
                {
                    Operator = Convert.ToUInt64(generateQuickLeaguePrizesRdr["operator"]),
                    Number = Convert.ToUInt64(generateQuickLeaguePrizesRdr["number"]),
                    PresentList = itemPrizeList.ToArray()
                };

                quickLeaguePrizeList.Add(operatorScore);
            }
            generateQuickLeaguePrizesRdr.Close();
            OperatorScore[] quickLeaguePrizeArray = quickLeaguePrizeList.ToArray();
            return quickLeaguePrizeArray;
        }

        public static OperatorScore[] GenerateDailyBattlePrizes(MySqlConnection conn)
        {
            var generateDailyBattlePrizesSql = Db.GetCommand(@"SELECT * FROM `sw_dailybattle`");
            var generateDailyBattlePrizesCmd = new MySqlCommand(generateDailyBattlePrizesSql, conn);
            var generateDailyBattlePrizesRdr = generateDailyBattlePrizesCmd.ExecuteReader();

            List<OperatorScore> dailyBattlePrizeList = new List<OperatorScore>();
            List<Item> itemPrizeList = new List<Item>();

            if (generateDailyBattlePrizesRdr.HasRows)
            {
                generateDailyBattlePrizesRdr.Read();

                Item item = new()
                {
                    itemId = Convert.ToInt32(generateDailyBattlePrizesRdr["item"]),
                    numItem = Convert.ToInt64(generateDailyBattlePrizesRdr["item_count"])
                };

                itemPrizeList.Add(item);

                OperatorScore operatorScore = new()
                {
                    Operator = Convert.ToUInt64(generateDailyBattlePrizesRdr["operator"]),
                    Number = Convert.ToUInt64(generateDailyBattlePrizesRdr["number"]),
                    PresentList = itemPrizeList.ToArray()
                };

                dailyBattlePrizeList.Add(operatorScore);
            }
            generateDailyBattlePrizesRdr.Close();

            OperatorScore[] dailyBattlePrizeArray = dailyBattlePrizeList.ToArray();
            return dailyBattlePrizeArray;
        }
    }
}
