using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Movies.Application.Handlers;
using Movies.Core.Repositories;
using Movies.Core.Repositories.Base;
using Movies.Infrastructure.Data;
using Movies.Infrastructure.Repositories;
using Movies.Infrastructure.Repositories.Base;
using System.Reflection;

namespace Movies.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; } 

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddApiVersioning();
            services.AddDbContext<MovieContext>(m=>m.UseSqlServer(Configuration.GetConnectionString("MovieCollection")),ServiceLifetime.Singleton);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MovieApi Review", Version = "v1"});

            });
            services.AddAutoMapper(typeof(Startup));
            services.AddMediatR(x => x.RegisterServicesFromAssemblies(typeof(CreateMovieCommandHandler).Assembly));
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<IMovieRepository, MovieRepository>();

        }
        public void Configure(IApplicationBuilder application,IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                application.UseDeveloperExceptionPage();

            }
            application.UseHttpsRedirection();
            application.UseRouting();
            application.UseAuthorization();
            application.UseEndpoints(endpoints=>

            {
                endpoints.MapControllers();


            });
            application.UseSwagger();
            application.UseSwaggerUI(s =>
            {
                s.SwaggerEndpoint("./swagger/v1/swagger.json", "v1");
            });
        }
    }
}
