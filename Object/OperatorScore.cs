using MySqlConnector;

namespace spikewall.Object
{
    public class OperatorScore
    {
        public ulong Operator { get; set; } //In the server, this will tell whether the prizes are in range (2) or just singluar (0)
        public ulong Number { get; set; } //This is the placement of the League within your group; for Daily Battles, it's based on your win streak
        public Item[] PresentList { get; set; } //This is the prize you get based on the number you are within your group; for Daily Battles, it's based on your win streak

        public static void GenerateEndlessOperatorPrizes(MySqlConnection conn)
        {
            var generateOperatorPrizesSql = Db.GetCommand(@"SELECT * FROM `sw_endlessleaguehighscoreprizes` WHERE league_id = '0'");
            var generateOperatorPrizesCmd = new MySqlCommand(generateOperatorPrizesSql, conn);
            var generateOperatorPrizesRdr = generateOperatorPrizesCmd.ExecuteReader();

            while (generateOperatorPrizesRdr.Read())
            {
                //Placeholder
            }
            generateOperatorPrizesRdr.Close();
        }
        public static void GenerateQuickOperatorPrizes(MySqlConnection conn)
        {
            var generateOperatorPrizesSql = Db.GetCommand(@"SELECT * FROM `sw_quickleaguehighscoreprizes` WHERE league_id = '0'");
            var generateOperatorPrizesCmd = new MySqlCommand(generateOperatorPrizesSql, conn);
            var generateOperatorPrizesRdr = generateOperatorPrizesCmd.ExecuteReader();

            while (generateOperatorPrizesRdr.Read())
            {
                //Placeholder
            }
            generateOperatorPrizesRdr.Close();
        }
    }
}
