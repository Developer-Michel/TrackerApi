using Tracker_Api;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", builder =>
    {
        builder.WithOrigins("http://localhost:3000")  // Specify your client URL
               .AllowAnyMethod()
               .AllowAnyHeader()
               .AllowCredentials() // Required for credentials (cookies, headers)
               .WithExposedHeaders("AppName", "MachineName", "ReactVersion"); // If you want to expose custom headers
    });
});
ProgramService.AddDbContext(builder);
ProgramService.Add(builder);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
ProgramService.AddAppBuilder(app);
// Configure the HTTP request pipeline.
app.UseCors("AllowAllOrigins");
app.MapControllers();

app.Run();
