using Asp.Versioning;
using Microsoft.EntityFrameworkCore;
using Movies.API.Endpoints;
using Movies.Application;
using Movies.Domain.MovieAggregate;
using Movies.Infrastructure;
using Movies.Persistence;
using AssemblyReference = Movies.Application.AssemblyReference;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediatR(configuration =>
{
	configuration.RegisterServicesFromAssembly(AssemblyReference.Assembly);
	configuration.AddOpenBehavior(typeof(TransactionPipelineBehavior<,>));
});

builder.Services.AddDbContext<ApplicationDbContext>((_, optionsBuilder) =>
{
	optionsBuilder.UseNpgsql(builder.Configuration.GetConnectionString("Database"));
});

// Register Dapper connection factory
builder.Services.AddScoped<IDatabaseConnectionFactory, PostgresConnectionFactory>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IMovieRepository, MovieRepository>();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApiVersioning(options =>
	{
		options.ReportApiVersions = true;
		options.DefaultApiVersion = IdentityConfiguration.Default;
		options.AssumeDefaultVersionWhenUnspecified = true;
		options.ApiVersionReader =
			ApiVersionReader.Combine(new HeaderApiVersionReader("api-version"), new UrlSegmentApiVersionReader());
	})
	.AddApiExplorer(options =>
	{
		options.GroupNameFormat = "'v'VVV";
		options.SubstituteApiVersionInUrl = true;
	});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.MapMovieEndpoints();
app.UseHttpsRedirection();

app.Run();