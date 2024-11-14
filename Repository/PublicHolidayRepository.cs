using Microsoft.EntityFrameworkCore;

public class PublicHolidayRepository
{
    private readonly MyDbContext _context;  // Assuming you're using Entity Framework

    public PublicHolidayRepository(MyDbContext context)
    {
        _context = context;
    }

    // Get all public holidays
    public async Task<List<PublicHoliday>> GetAllPublicHolidaysAsync()
    {
        return await _context.PublicHolidays.ToListAsync();  // Fetch all holidays
    }

    // Get a public holiday by Id
    public async Task<PublicHoliday> GetByIdAsync(int id)
    {
        return await _context.PublicHolidays.FindAsync(id);  // Fetch holiday by Id
    }

    // Add a new public holiday
    public async Task AddPublicHolidayAsync(PublicHoliday publicHoliday)
    {
        _context.PublicHolidays.Add(publicHoliday);
        await _context.SaveChangesAsync();
    }

    // Update an existing public holiday
    public async Task UpdatePublicHolidayAsync(PublicHoliday publicHoliday)
    {
        _context.PublicHolidays.Update(publicHoliday);
        await _context.SaveChangesAsync();
    }

    // Delete a public holiday
    public async Task DeletePublicHolidayAsync(int id)
    {
        var holiday = await _context.PublicHolidays.FindAsync(id);
        if (holiday != null)
        {
            _context.PublicHolidays.Remove(holiday);
            await _context.SaveChangesAsync();
        }
    }
}