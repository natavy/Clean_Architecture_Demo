using Movies.Infrastructure.Data;

namespace Movies.API
{
    public class Program
    {

        public static async Task Main(string[] args)
        {

            var host = CreateHostBuilder(args).Build();
            await CreateAndSeedDb(host);
            host.Run();
        }

        private static async Task CreateAndSeedDb(IHost host)
        {
            using(var scope= host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logerFactory = services.GetRequiredService<ILoggerFactory>();
                try
                {
                    var movieContext = services.GetRequiredService<MovieContext>();
                    await MovieContextSeed.SeedAsync(movieContext, logerFactory);
                }
                catch (Exception ex)
                {

                    var loger = logerFactory.CreateLogger<Program>();
                    loger.LogError($"Exception occured in{ex.Message}");
                }
            }
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        => Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
    }
    
}