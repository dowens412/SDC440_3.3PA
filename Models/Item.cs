using SQLite;

namespace SDC440_3._3PA.Models
{
    public class Item
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        // Item ID the user types
        public int ItemId { get; set; }

        public string ItemName { get; set; } = string.Empty;

        public string ItemDescription { get; set; } = string.Empty;
    }
}
