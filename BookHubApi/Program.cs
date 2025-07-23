using BookHubApi.Data;
using Microsoft.EntityFrameworkCore;
using BookHubApi.Services;
using BookHubApi.Mappings;

var builder = WebApplication.CreateBuilder(args);

// Add PostgreSQL EF Core context
builder.Services.AddDbContext<BookDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddAutoMapper(typeof(MappingProfile));

// ? CORS configuration: allow both Angular and Live Server clients
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllClients", policy =>
    {
        policy.WithOrigins(
                "http://localhost:4200",         // Angular CLI
                "http://127.0.0.1:5500"          // Live Server (optional)
            )
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// Add MVC services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Enable Swagger in development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// ? Apply CORS policy middleware
app.UseCors("AllowAllClients");

app.UseAuthorization();

app.MapControllers();

app.Run();
