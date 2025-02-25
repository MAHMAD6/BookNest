using SQLite;

namespace BookNest
{
    public static class AppSettings
    {
        // Administration Credentials
        public const string AdminUserName = "admin";
        public const string AdminPassword = "123";
        public const string AdminFullName = "Nazir";
        public const decimal PerDayFineRate = 1.2M;


        // Database Credentials
        private const string DbFilename = "HData.SQLite.db3";
        public const SQLiteOpenFlags DbFlags = SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.SharedCache;
        public static string DbPath
        {
            get
            {
                return Path.Combine(FileSystem.AppDataDirectory, DbFilename);
            }
        }

    }
}
