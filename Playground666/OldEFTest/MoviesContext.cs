using System.Data;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace OldEFTest
{
    public class MoviesContext : DbContext
    {
        private readonly IDbConnection _dbConnection;
        public DbSet<Movie> Movies { get; set; }
    
        public MoviesContext(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            /*
            var moviesdDBPath = """G:\code\dotnettests\dotnettests\Playground666\EFCoreVsDapper\movies.db""";
            builder.UseSqlite($"Data Source={moviesdDBPath}"); */
            builder.UseSqlite((DbConnection)_dbConnection);
        }
    }
}