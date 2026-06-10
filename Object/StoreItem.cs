namespace spikewall.Object
{
    public class StoreItem
    {
        public string? StoreItemID { get; set; }
        public string? ItemID { get; set; }
        public string? PriceDisp { get; set; }
        public string? ProductID  { get; set; }
        public long NumItem { get; set; }
        public long Price { get; set;  }
        public Campaign[]? Campaign {  get; set; }
    }
}
