using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Bogus;
using Dapper;

namespace OldEFTest
{
    public class MovieGenerator
    {
        private readonly IDbConnection _dbConnection;
        private readonly List<Guid> _ids = new();
    
    private readonly Faker<Movie> _movieGenerator = new Faker<Movie>()
        .RuleFor(m => m.Id, f => f.Random.Guid())
        .RuleFor(m => m.Title, faker => faker.Name.FullName())
        .RuleFor(m => m.YearOfRelease, f => f.Random.Int(1990, 2023));

    public MovieGenerator(IDbConnection dbConnection,  Random random)
    {
        _dbConnection = dbConnection;
        Randomizer.Seed = random;
    }

    public async Task CleanupMovies()
    {
        _ids.Clear();
        _dbConnection.ExecuteAsync("DELETE FROM MOVIES WHERE 1 = 1");
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
            await _dbConnection.ExecuteAsync(@"
                INSERT INTO Movies (Id, Title, YearOfRelease)
                VALUES (@Id, @Title, @YearOfRelease)
                ", generatedMovie);
        }
    }
    }
}