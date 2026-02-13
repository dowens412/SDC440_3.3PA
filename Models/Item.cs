using SQLite; // SQLite attributes like [PrimaryKey] and [AutoIncrement]

namespace SDC440_3._3PA.Models
{
    // This class represents the table structure in SQLite
    public class Item
    {
        // Primary key for the table (auto-incremented by SQLite)
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        // Item ID entered by the user (separate from the database primary key)
        public int ItemId { get; set; }

        // Item name entered by the user
        public string ItemName { get; set; } = string.Empty;

        // Item description entered by the user
        public string ItemDescription { get; set; } = string.Empty;
    }
}
