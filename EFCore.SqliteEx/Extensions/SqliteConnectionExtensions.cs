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
                // Workaround ericsink/SQLitePCL.raw#225
                var file = "mod_spatialite";
                if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    file += ".dylib";
                }
                else if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    file += ".so";
                }

                command.CommandText = "SELECT load_extension('" + file + "');";

                // Let the CLR load from NuGet package assets. SQLite won't look there
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
