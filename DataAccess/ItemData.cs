using SQLite;                   // SQLiteAsyncConnection and query helpers
using SDC440_3._3PA.Models;      // Item model and DatabaseConstants

namespace SDC440_3._3PA.DataAccess
{
    // Handles all database actions for Items (CRUD operations)
    public class ItemData
    {
        // Connection object for SQLite (async so it doesn't block the UI)
        private SQLiteAsyncConnection? database;

        // Initializes the database connection and creates the Item table if needed
        private async Task Init()
        {
            if (database is not null)
                return; // Already initialized

            // Create connection using the shared constants
            database = new SQLiteAsyncConnection(DatabaseConstants.DatabasePath, DatabaseConstants.Flags);

            // Create table if it doesn't exist
            await database.CreateTableAsync<Item>();
        }

        // Returns all items stored in the Item table
        public async Task<List<Item>> GetItemsAsync()
        {
            await Init();
            return await database!.Table<Item>().ToListAsync();
        }

        // Returns one item by primary key ID (example method)
        public async Task<Item?> GetItemAsync(int id)
        {
            await Init();
            return await database!.Table<Item>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        // Inserts a new item or updates an existing one
        public async Task<int> SaveItemAsync(Item item)
        {
            await Init();

            // If it already has an ID, update it; otherwise insert a new row
            if (item.ID != 0)
                return await database!.UpdateAsync(item);

            return await database!.InsertAsync(item);
        }

        // Deletes an item row from the database (example method)
        public async Task<int> DeleteItemAsync(Item item)
        {
            await Init();
            return await database!.DeleteAsync(item);
        }
    }
}
