using MySqlConnector;

namespace spikewall.Object
{
    public class OperatorScore
    {
        public ulong Operator { get; set; }
        public ulong Number { get; set; }
        public Item[] PresentList { get; set; }

        public static void GenerateEndlessOperatorScores(MySqlConnection conn)
        {
            var generateOperatorPrizesSql = Db.GetCommand(@"SELECT * FROM `sw_endlessleaguehighscoreprizes` WHERE league_id = '0'");
            var generateOperatorPrizesCmd = new MySqlCommand(generateOperatorPrizesSql, conn);
            var generateOperatorPrizesRdr = generateOperatorPrizesCmd.ExecuteReader();

            while (generateOperatorPrizesRdr.Read())
            {
                //Placeholder
            }
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
        }
    }
}
