using System.Data;
using Bogus;
using Dapper;
using DuckDB.NET.Data;

namespace EFCoreVsDapper;

public class MovieGenerator
{
    private readonly IDbConnection _dbConnection;
    private readonly DuckDBConnection _duckDbConnection;
    private readonly List<Guid> _ids = new();
    
    private readonly Faker<Movie> _movieGenerator = new Faker<Movie>()
        .RuleFor(m => m.Id, f => f.Random.Guid())
        .RuleFor(m => m.Title, faker => faker.Name.FullName())
        .RuleFor(m => m.YearOfRelease, f => f.Random.Int(1990, 2023));

    public MovieGenerator(IDbConnection dbConnection, DuckDBConnection duckDbConnection, Random random)
    {
        _dbConnection = dbConnection;
        _duckDbConnection = duckDbConnection;
        Randomizer.Seed = random;
    }

    public async Task CleanupMovies()
    {
        _ids.Clear();
        _dbConnection.ExecuteAsync("""
                DELETE FROM MOVIES WHERE 1 = 1 
                """);

        if (_duckDbConnection != null)
        {
            var command = _duckDbConnection.CreateCommand();

            command.CommandText = "DELETE FROM MOVIES WHERE 1 = 1";
            await command.ExecuteNonQueryAsync();
        }
    }
    public async Task GenerateMovies(int count)
    {
        var generatedMovies = _movieGenerator.Generate(count);
        int i = 0;
        foreach (var movie in generatedMovies)
        {
            movie.Title = "Movie " + i++;
            
        }
        foreach (var generatedMovie in generatedMovies)
        {
            _ids.Add(generatedMovie.Id);
            await _dbConnection.ExecuteAsync("""
                INSERT INTO Movies (Id, Title, YearOfRelease)
                VALUES (@Id, @Title, @YearOfRelease)
                """, generatedMovie);

            if (_duckDbConnection != null)
            {
                var command = _duckDbConnection.CreateCommand();

                command.CommandText =
                    $"INSERT INTO Movies (Id, Title, YearOfRelease) values ('{generatedMovie.Id.ToString()}', '{generatedMovie.Title}', {generatedMovie.YearOfRelease})";
                await command.ExecuteNonQueryAsync();
            }
        }
    }
}