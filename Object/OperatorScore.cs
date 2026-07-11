using MySqlConnector;

namespace spikewall.Object
{
    public class OperatorScore
    {
        public ulong Operator { get; set; }
        public ulong Number { get; set; } //This is the placement of the League within your group
        public Item[] PresentList { get; set; } //This is the prize you get based on the number you are within your group

        public static void GenerateEndlessOperatorScores(MySqlConnection conn)
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
        public static void GenerateQuickOperatorScores(MySqlConnection conn)
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
