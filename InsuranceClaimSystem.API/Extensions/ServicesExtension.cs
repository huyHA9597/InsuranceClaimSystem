using AutoMapper;
using FluentValidation;
using InsuranceClaimSystem.API.Database;
using Microsoft.EntityFrameworkCore;

namespace InsuranceClaimSystem.API.Extensions
{
    public static class ServicesExtension
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // In-memory database configuration
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase("InMemICS"));

            // CORS configuration
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                    builder.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod());

            });

            // MediatR configuration
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

            // Validation configuration
            services.AddValidatorsFromAssembly(typeof(Program).Assembly);

            // AutoMapper configuration
            services.AddSingleton<IMapper>(new Mapper(new MapperConfiguration(cfg => cfg.AddMaps(typeof(Program).Assembly))));

            // Other service configurations...
            services.AddProblemDetails();
            services.AddControllers();
            services.AddAuthorization();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;
        }
    }
}
