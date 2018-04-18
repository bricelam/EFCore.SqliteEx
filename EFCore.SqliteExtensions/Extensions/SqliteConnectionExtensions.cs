using System;

namespace Microsoft.Data.Sqlite
{
    public static class SqliteConnectionExtensions
    {
        public static SqliteConnection AddEFCoreExtensions(this SqliteConnection connection)
        {
            connection.CreateFunction("days", (TimeSpan value) => value.TotalDays);
            connection.CreateFunction("timespan", (double value) => TimeSpan.FromDays(value));

            return connection;
        }
    }
}
