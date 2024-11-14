using Microsoft.EntityFrameworkCore;
 // Assuming your repositories are in this namespace

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Register DbContext with SQL Server
builder.Services.AddDbContext<MyDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyDbContext")));

// Check if the connection string is available
Console.WriteLine(builder.Configuration.GetConnectionString("MyDbContext"));

// Register application services and repositories
builder.Services.AddScoped<EmployeeRepository>();
builder.Services.AddScoped<EmployeeService>();
builder.Services.AddScoped<PublicHolidayRepository>();
builder.Services.AddScoped<PublicHolidayService>();

// Add controllers with views
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Check the database connection at startup
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<MyDbContext>();
    try
    {
        if (dbContext.Database.CanConnect())
        {
            Console.WriteLine("Connection to the database was successful.");
        }
        else
        {
            Console.WriteLine("Could not connect to the database.");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Database connection failed: {ex.Message}");
    }
}

// Configure the HTTP request pipeline for different environments
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

// Map controllers and views, including default routing
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Enable static assets like CSS, JS, etc.
app.MapFallbackToFile("index.html");

app.Run();
