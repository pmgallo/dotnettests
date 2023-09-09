using System.Data;
using BenchmarkDotNet.Attributes;
using Dapper;
using DuckDB.NET.Data;
using EFCoreVsDapper.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace EFCoreVsDapper;

[MemoryDiagnoser(false)]
public class Benchmarks
{
    private MoviesContext _moviesContext = null!;
    private IDbConnection _dbConnection = null!;
    private DuckDBConnection _duckDbConnection = null;
    
    private Random _random = null!;
    private Movie _testMovie = null!;
    private MovieGenerator _movieGenerator = null!;

    [GlobalSetup]
    public async Task Setup()
    {
        _random = new Random(420);
        var dbConnectionFactory =
            new SqliteConnectionFactory(
                """Data Source=G:\code\dotnettests\dotnettests\Playground666\EFCoreVsDapper\movies.db""");

        
        _duckDbConnection = new DuckDBConnection(@"Data Source=G:\code\dotnettests\dotnettests\Playground666\EFCoreVsDapper\movies.duck.db");
        _duckDbConnection.Open();

        /*
        var command = _duckDbConnection.CreateCommand();
        command.CommandText = "CREATE INDEX IDX_YEAROFRELEASE ON Movies( YearOfRelease )";
        await command.ExecuteNonQueryAsync();
        */

        _dbConnection = await dbConnectionFactory.CreateConnectionAsync();
        _movieGenerator = new MovieGenerator(_dbConnection, _duckDbConnection, _random);
        _testMovie = new Movie()
        {
            Id = Guid.NewGuid(),
            Title = "Godzilla vs Mazinger Z",
            YearOfRelease = _random.Next()
        };
        
        await InternalCleanup();
        await _movieGenerator.GenerateMovies(100);
        
        await _dbConnection.ExecuteAsync("""
                INSERT INTO MOVIES (Id, Title, YearOfRelease)
                VALUES (@Id, @Title, @YearOfRelease)
                """, _testMovie);
        _moviesContext = new(_dbConnection);
    }

    private async Task InternalCleanup()
    {
        await _movieGenerator.CleanupMovies();
        await _dbConnection.ExecuteAsync("""
                                DELETE FROM Movies WHERE Id=@Id
                                """, _testMovie);
    }

    [GlobalCleanup]
    public async Task Cleanup()
    {
        await InternalCleanup();
    }

    // [Benchmark]
    // public async Task<Movie> EF_Find()
    // {
    //     return await _moviesContext.Movies.FindAsync(_testMovie.Id);
    // }
    //
    // [Benchmark]
    // public async Task<Movie?> EF_Single()
    // {
    //     return await _moviesContext.Movies.SingleOrDefaultAsync(x => x.Id == _testMovie.Id);
    // }
    //
    private static readonly Func<MoviesContext, Guid, Task<Movie?>> SingleMovieAsync  =
        EF.CompileAsyncQuery(
            (MoviesContext context, Guid id) => context
                .Movies.SingleOrDefault(x => x.Id == id));
    
    [Benchmark]
    public async Task<Movie?> EF_Single_Compiled()
    {
        return await SingleMovieAsync(_moviesContext, _testMovie.Id);
    }
    
    private static readonly Func<MoviesContext, Guid, Task<Movie?>> FirstMovieAsync =
        EF.CompileAsyncQuery(
            (MoviesContext context, Guid id) => context
                .Movies.FirstOrDefault(x => x.Id == id));
    
    // [Benchmark()]
    // public async Task<Movie> EF_First_Compiled()
    // {
    //     return await FirstMovieAsync(_moviesContext, _testMovie.Id);
    // }
    //
    // [Benchmark()]
    // public async Task<Movie> EF_First()
    // {
    //     return await _moviesContext.Movies.FirstOrDefaultAsync(x => x.Id == _testMovie.Id);
    // }
    //
    // [Benchmark()]
    // public async Task<Movie> Dapper_GetById()
    // {
    //     return await _dbConnection.QuerySingleOrDefaultAsync<Movie>("SELECT * FROM Movies where Id = @Id LIMIT 1",
    //         new { _testMovie.Id });
    // }

//     [Benchmark()]
//     public async Task<Movie> EF_Add_Delete()
//     {
//         var movie = new Movie
//         {
//             Id = Guid.NewGuid(),
//             Title = "Salamandra II",
//             YearOfRelease = 2013
//         };
//
//         await _moviesContext.Movies.AddAsync(movie);
//         await _moviesContext.SaveChangesAsync();
//
//         _moviesContext.Movies.Remove(movie);
//         await _moviesContext.SaveChangesAsync();
//         return movie;
//     }
//
//     [Benchmark()]
//     public async Task<Movie> Dapper_Add_Delete()
//     {
//         var movie = new Movie()
//         {
//             Id = Guid.NewGuid(),
//             Title = "Salamandra II",
//             YearOfRelease = 2013
//         };
//
//         await _dbConnection.ExecuteAsync("""
// INSERT INTO Movies (Id, Title, YearOfRelease)
// VALUES (@Id, @Title, @YearOfRelease)
// """, movie);
//
//         await _dbConnection.ExecuteAsync("""
// DELETE FROM Movies WHERE ID = @Id
// """, new { movie.Id });
//         return movie;
//     }

// [Benchmark()]
//     public async Task<Movie> EF_Update()
//     {
//         _testMovie.YearOfRelease = _random.Next();
//         _moviesContext.Movies.Update(_testMovie);
//         await _moviesContext.SaveChangesAsync();
//         return _testMovie;
//     }
//
//     [Benchmark()]
//     public async Task<Movie> Dapper_Update()
//     {
//         _testMovie.YearOfRelease = _random.Next();
//         await _dbConnection.ExecuteAsync("""
// UPDATE Movies SET Title = @Title, YearOfRelease = @YearOfRelease
// WHERE Id = @Id
// """, _testMovie);
//         return _testMovie;
//     }

    private static readonly Func<MoviesContext, int, IAsyncEnumerable<Movie>> GetMoviesAsync  =
        EF.CompileAsyncQuery(
            (MoviesContext context, int year) => context.Movies
                .Where(x => x.YearOfRelease == year));
    
    [Benchmark()]
    public async Task<List<Movie>> EF_Filter_Compiled()
    {
        var result = new List<Movie>();
        await foreach (var item in GetMoviesAsync(_moviesContext, 1993))
        {
            result.Add((item));
        }

        return result;
    }
    
    [Benchmark()]
    public async Task<List<Movie>> EF_Filter()
    {
        return await _moviesContext.Movies
            .Where(x => x.YearOfRelease == 1993)
            .ToListAsync();
    }

    [Benchmark()]
    public async Task<List<Movie>> Dapper_Filter()
    {
        var result = await _dbConnection
            .QueryAsync<Movie>("""
select * from Movies where YearOfRelease = @YearOfRelease
""", new {YearOfRelease = 1993});
        return result.ToList();
    }
    
    // [Benchmark()]
    // public async Task<List<Movie>> DuckDB_Filter()
    // {
    //     using var command = _duckDbConnection.CreateCommand();
    //     command.CommandText = "SELECT * from Movies where YearOfRelease = ?;";
    //     command.Parameters.Add(new DuckDBParameter(1993));
    //     using var reader = await command.ExecuteReaderAsync();
    //     List<Movie> movies = new List<Movie>();
    //     while (reader.Read())
    //     {
    //         string storedGuid = reader.GetString(0);
    //         movies.Add(new Movie(){ Id = Guid.Parse(reader.GetString(0)), Title = reader.GetString(1), YearOfRelease = reader.GetInt32(2)});
    //     }
    //
    //     return movies;
    //}
}