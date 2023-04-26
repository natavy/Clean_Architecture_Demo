using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Movies.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Infrastructure.Data
{
    public class MovieContextSeed
    {
        public static async Task SeedAsync(MovieContext movieContext, ILoggerFactory loggerFactory, int? retry = 0)
        {
            int retryForAvailability = retry.Value;
            try
            {
                await movieContext.Database.EnsureCreatedAsync();
                if (movieContext.Movies.Any())
                {
                    movieContext.Movies.AddRange(entities: GetMovies());
                    await movieContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                if (retryForAvailability<3)
                {
                    retryForAvailability++;
                    var log =loggerFactory.CreateLogger<MovieContextSeed>();
                    log.LogError(message: $"Exception occured while connection: { ex.Message}");
                    await SeedAsync(movieContext, loggerFactory, retryForAvailability);
                }
                throw;
            }

        }
        public static  IEnumerable<Movie> GetMovies()
        {
            return new List<Movie>
            { new Movie{MovieName="Avatar", DirectorName="James Cameron", ReleaseYear="2009"},
              new Movie{MovieName="Green Way", DirectorName="Spilburg", ReleaseYear="2001"},
              new Movie{MovieName="Home", DirectorName="Francis ", ReleaseYear="2012"},
              new Movie{MovieName="The Note", DirectorName="Petar", ReleaseYear="2005"}
            
            };
        }
    }
}
