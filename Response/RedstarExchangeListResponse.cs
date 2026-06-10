namespace spikewall.Response
{
    /// <summary>
    /// Response containing red star exchange list to trade Red Star Rings for Rings and Revive Tokens
    /// as well as money to purchase Red Star Rings
    /// </summary>
    public class RedstarExchangeListResponse : BaseResponse
    {
        // FIXME: Messages shouldn't actually be strings, set up "StoreItem" object
        public string[]? itemList { get; set; }
        public long? totalItems { get; set; }
        public long? monthPurchase { get; set; }
        public string? birthday { get; set; }

        public RedstarExchangeListResponse()
        {
            itemList = Array.Empty<string>();
            totalItems = 0;
            monthPurchase = 0;
            birthday = "1900-1-1";
        }
    }
}
