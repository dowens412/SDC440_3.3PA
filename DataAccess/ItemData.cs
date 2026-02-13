using SQLite;
using SDC440_3._3PA.Models;

namespace SDC440_3._3PA.DataAccess
{
    public class ItemData
    {
        private SQLiteAsyncConnection? database;

        private async Task Init()
        {
            if (database is not null)
                return;

            database = new SQLiteAsyncConnection(DatabaseConstants.DatabasePath, DatabaseConstants.Flags);

            // Create table if it doesn't exist
            await database.CreateTableAsync<Item>();
        }

        public async Task<List<Item>> GetItemsAsync()
        {
            await Init();
            return await database!.Table<Item>().ToListAsync();
        }

        public async Task<Item?> GetItemAsync(int id)
        {
            await Init();
            return await database!.Table<Item>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public async Task<int> SaveItemAsync(Item item)
        {
            await Init();

            if (item.ID != 0)
                return await database!.UpdateAsync(item);

            return await database!.InsertAsync(item);
        }

        public async Task<int> DeleteItemAsync(Item item)
        {
            await Init();
            return await database!.DeleteAsync(item);
        }
    }
}
