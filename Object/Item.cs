namespace spikewall.Object
{
    public class Item
    {
        public long? itemId { get; set; }
        public long? numItem { get; set; }

        /// <summary>
        /// Enum that contains the names 
        /// and IDs of all relevant items.
        /// </summary>
        public enum ItemID
        {
            None = -1,

            NormalEgg,
            RareEgg,
            SuperRareEgg,
            CharacterEgg = 100,

            ScoreBoost = 110000,
            SupportSpring,
            SubCharacter,

            Invincible = 120000,
            Shield,
            Magnet,
            Spring,
            ComboBonus,
            Laser,
            Drill,
            Asteroid,
            RingBonus,
            DistanceBonus,
            AnimalBonus,

            // If this is won on rank 3, it is a jackpot
            ItemRouletteRankUp = 200000,

            SpecialEgg = 220000,
            PremiumRouletteTicket = 230000,
            ItemRouletteTicket = 240000,

            RedStarRing = 900000,
            Ring = 910000,
            PowerRing = 960000
        }

        public Item(long itemId, long numItem)
        {
            this.itemId = itemId;
            this.numItem = numItem;
        }

        public Item()
        {
            this.itemId = 0;
            this.numItem = 0;
        }
    }

    public class ConsumedItem : Item
    {
        public long? consumedItemId { get; set; }
    }

    public class CostItem : Item
    {
        public long ItemStock { get; set; }
    }

    public class GiftBoxItem : Item
    {
        public long? AdditionalInfo1 { get; set; }
        public long? AdditionalInfo2 { get; set; }
    }

    public class MessageItem : Item
    {
        public long? AdditionalInfo1 { get; set; }
        public long? AdditionalInfo2 { get; set; }
    }
}
