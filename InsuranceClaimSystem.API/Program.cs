using AutoMapper;
using InsuranceClaimSystem.API.Database;
using InsuranceClaimSystem.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

ServicesExtension.ConfigureServices(builder.Services, builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

await using var serviceScope = app.Services.CreateAsyncScope();
var mapper = serviceScope.ServiceProvider.GetRequiredService<IMapper>();
mapper.ConfigurationProvider.AssertConfigurationIsValid();
mapper.ConfigurationProvider.CompileMappings();

// Seed data
await using var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
await dbContext.Database.EnsureCreatedAsync();

app.Run();
