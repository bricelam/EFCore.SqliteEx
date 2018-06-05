using System;
using System.Runtime.InteropServices;

namespace Microsoft.Data.Sqlite
{
    public static class SqliteConnectionExtensions
    {
        public static SqliteConnection AddEFCoreExtensions(this SqliteConnection connection)
        {
            // TimeSpan
            connection.CreateFunction("days", (TimeSpan value) => value.TotalDays);
            connection.CreateFunction("timespan", (double value) => TimeSpan.FromDays(value));

            // NetTopologySuite
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT load_extension('mod_spatialite');";

                // Let the CLR load the library from NuGet package assets
                var temp = spatialite_version();

                connection.EnableExtensions();
                command.ExecuteNonQuery();
                connection.EnableExtensions(false);
            }

            return connection;
        }

        [DllImport("mod_spatialite")]
        private static extern IntPtr spatialite_version();
    }
}
