using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;

public class MyDbContext : DbContext
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<MyDbContext> _logger;

    public MyDbContext(IConfiguration configuration, ILogger<MyDbContext> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    // Method to test connection explicitly, can be called during app startup
    public void TestConnection()
    {
        try
        {
            if (this.Database.CanConnect())
            {
                _logger.LogInformation("Connection to the database was successful.");
            }
            else
            {
                _logger.LogWarning("Could not connect to the database.");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"Database connection failed: {ex.Message}");
        }
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("MyDbContext"));
        }
    }

    public DbSet<Employee> Employees { get; set; }
    public DbSet<PublicHoliday> PublicHolidays { get; set; }
}
