using System.Threading;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore.Sqlite.Storage.Internal;
using Microsoft.EntityFrameworkCore.Storage;

namespace Bricelam.EntityFrameworkCore.Sqlite
{
    class SqliteExRelationalConnection : SqliteRelationalConnection
    {
        public SqliteExRelationalConnection(
            RelationalConnectionDependencies dependencies,
            IRawSqlCommandBuilder rawSqlCommandBuilder)
            : base(dependencies, rawSqlCommandBuilder)
        {
        }

        public bool Open(bool errorsExpected)
        {
            if (!base.Open(errorsExpected))
                return false;

            ((SqliteConnection)DbConnection).AddEFCoreExtensions();

            return true;
        }

        public async Task<bool> OpenAsync(CancellationToken cancellationToken, bool errorsExpected)
        {
            if (!await base.OpenAsync(cancellationToken, errorsExpected))
                return false;

            ((SqliteConnection)DbConnection).AddEFCoreExtensions();

            return true;
        }
    }
}
