using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<MyDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyDbContext")));
Console.WriteLine(builder.Configuration.GetConnectionString("MyDbContext"));

// Register services
builder.Services.AddScoped<EmployeeRepository>();
builder.Services.AddScoped<EmployeeService>();
builder.Services.AddScoped<PublicHolidayRepository>();
builder.Services.AddScoped<PublicHolidayService>();

// Add controllers with views
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Check database connection at startup
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

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
