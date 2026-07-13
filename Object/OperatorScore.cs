using MySqlConnector;
using spikewall.Response;
using static spikewall.Object.LeagueData;

namespace spikewall.Object
{
    public class OperatorScore
    {
        public ulong Operator { get; set; } //In the server, this will tell whether the prizes are in a range (2) or just singluar (0)
        public ulong Number { get; set; } //This is the placement of the League within your group; for Daily Battles, it's based on your win streak
        public Item[] PresentList { get; set; } //This is the prize you get based on the number you are within your group; for Daily Battles, it's based on your win streak

        public static SRStatusCode GenerateEndlessLeagueHighScorePrizes(MySqlConnection conn, ref LeagueID leagueID, out OperatorScore[] endlessLeagueHighScorePrizeArray)
        {
            var generateEndlessLeaguePrizesSql = Db.GetCommand(@"SELECT * FROM `sw_endlessleaguehighscoreprizes` WHERE league_id = '{0}'", leagueID);
            var generateEndlessLeaguePrizesCmd = new MySqlCommand(generateEndlessLeaguePrizesSql, conn);
            var generateEndlessLeaguePrizesRdr = generateEndlessLeaguePrizesCmd.ExecuteReader();

            List<OperatorScore> endlessLeaguePrizeList = new List<OperatorScore>();
            List<Item> itemPrizeList = new List<Item>();
            while (generateEndlessLeaguePrizesRdr.Read())
            {            
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
                generateEndlessLeaguePrizesRdr.Read();
            }
            generateEndlessLeaguePrizesRdr.Close();
            endlessLeagueHighScorePrizeArray = endlessLeaguePrizeList.ToArray();
            return SRStatusCode.Ok;
        }

        public static SRStatusCode GenerateEndlessLeagueTotalScorePrizes(MySqlConnection conn, ref LeagueID leagueID, out OperatorScore[] endlessLeagueTotalScorePrizeArray)
        {
            var generateEndlessLeaguePrizesSql = Db.GetCommand(@"SELECT * FROM `sw_endlessleaguetotalscoreprizes` WHERE league_id = '{0}'", leagueID);
            var generateEndlessLeaguePrizesCmd = new MySqlCommand(generateEndlessLeaguePrizesSql, conn);
            var generateEndlessLeaguePrizesRdr = generateEndlessLeaguePrizesCmd.ExecuteReader();

            List<OperatorScore> endlessLeaguePrizeList = new List<OperatorScore>();
            List<Item> itemPrizeList = new List<Item>();

            while (generateEndlessLeaguePrizesRdr.Read())
            {
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
                generateEndlessLeaguePrizesRdr.Read();
            }
            generateEndlessLeaguePrizesRdr.Close();
            endlessLeagueTotalScorePrizeArray = endlessLeaguePrizeList.ToArray();
            return SRStatusCode.Ok;
        }
        public static SRStatusCode GenerateQuickLeagueHighScorePrizes(MySqlConnection conn, ref LeagueID leagueID, OperatorScore[] quickLeagueHighScorePrizeArray)
        {
            var generateQuickLeaguePrizesSql = Db.GetCommand(@"SELECT * FROM `sw_quickleaguehighscoreprizes` WHERE league_id = '{0}'", leagueID);
            var generateQuickLeaguePrizesCmd = new MySqlCommand(generateQuickLeaguePrizesSql, conn);
            var generateQuickLeaguePrizesRdr = generateQuickLeaguePrizesCmd.ExecuteReader();

            List<OperatorScore> quickLeaguePrizeList = new List<OperatorScore>();
            List<Item> itemPrizeList = new List<Item>();

            while (generateQuickLeaguePrizesRdr.Read())
            {

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
                generateQuickLeaguePrizesRdr.Read();
            }
            generateQuickLeaguePrizesRdr.Close();
            quickLeagueHighScorePrizeArray = quickLeaguePrizeList.ToArray();
            return SRStatusCode.Ok;
        }

        public static SRStatusCode GenerateQuickLeagueTotalScorePrizes(MySqlConnection conn, ref LeagueID leagueID, out OperatorScore[] quickLeagueTotalScorePrizeArray)
        {
            var generateQuickLeaguePrizesSql = Db.GetCommand(@"SELECT * FROM `sw_quickleaguetotalscoreprizes` WHERE league_id = '{0}'", leagueID);
            var generateQuickLeaguePrizesCmd = new MySqlCommand(generateQuickLeaguePrizesSql, conn);
            var generateQuickLeaguePrizesRdr = generateQuickLeaguePrizesCmd.ExecuteReader();

            List<OperatorScore> quickLeaguePrizeList = new List<OperatorScore>();
            List<Item> itemPrizeList = new List<Item>();

            while (generateQuickLeaguePrizesRdr.Read())
            {

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
                generateQuickLeaguePrizesRdr.Read();
            }
            generateQuickLeaguePrizesRdr.Close();
            quickLeagueTotalScorePrizeArray = quickLeaguePrizeList.ToArray();
            return SRStatusCode.Ok;
        }

        public static SRStatusCode GenerateDailyBattlePrizes(MySqlConnection conn, out OperatorScore[] dailyBattlePrizesArray)
        {
            var generateDailyBattlePrizesSql = Db.GetCommand(@"SELECT * FROM `sw_dailybattle`");
            var generateDailyBattlePrizesCmd = new MySqlCommand(generateDailyBattlePrizesSql, conn);
            var generateDailyBattlePrizesRdr = generateDailyBattlePrizesCmd.ExecuteReader();

            List<OperatorScore> dailyBattlePrizeList = new List<OperatorScore>();
            List<Item> itemPrizeList = new List<Item>();

            while (generateDailyBattlePrizesRdr.Read())
            {
                Item item1 = new()
                {
                    itemId = Convert.ToInt32(generateDailyBattlePrizesRdr["item"]),
                    numItem = Convert.ToInt64(generateDailyBattlePrizesRdr["item_count"])
                };

                itemPrizeList.Add(item1);

                if(Convert.ToInt32(generateDailyBattlePrizesRdr["item2"]) != null)
                {
                    Item item2 = new()
                    {
                        itemId = Convert.ToInt32(generateDailyBattlePrizesRdr["item2"]),
                        numItem = Convert.ToInt64(generateDailyBattlePrizesRdr["item2_count"])
                    };
                    itemPrizeList.Add(item2);
                }

                OperatorScore operatorScore = new()
                {
                    Operator = Convert.ToUInt64(generateDailyBattlePrizesRdr["operator"]),
                    Number = Convert.ToUInt64(generateDailyBattlePrizesRdr["number"]),
                    PresentList = itemPrizeList.ToArray()
                };

                dailyBattlePrizeList.Add(operatorScore);
                generateDailyBattlePrizesRdr.Read();
            }
            generateDailyBattlePrizesRdr.Close();

            dailyBattlePrizesArray = dailyBattlePrizeList.ToArray();
            return SRStatusCode.Ok;
        }
    }
}
