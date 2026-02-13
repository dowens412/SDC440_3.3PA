using System.IO; // Path.Combine

namespace SDC440_3._3PA.Models
{
    // Stores shared database settings (filename, flags, and full path)
    public static class DatabaseConstants
    {
        // Database file name stored in the app’s data directory
        public const string DatabaseFilename = "ItemsSQLite.db3";

        // SQLite flags control how the database is opened
        public const SQLite.SQLiteOpenFlags Flags =
            SQLite.SQLiteOpenFlags.ReadWrite   // open in read/write mode
            | SQLite.SQLiteOpenFlags.Create    // create the database if it doesn't exist
            | SQLite.SQLiteOpenFlags.SharedCache; // allow multi-threaded access

        // Full file path to the database inside the app’s storage location
        public static string DatabasePath =>
            Path.Combine(FileSystem.AppDataDirectory, DatabaseFilename);
    }
}
