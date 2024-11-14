using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class PublicHolidayRepository
{
    private readonly MyDbContext _context;

    public PublicHolidayRepository(MyDbContext context)
    {
        _context = context;
    }

    // Get all public holidays
    public async Task<List<PublicHoliday>> GetAllPublicHolidaysAsync()
    {
        return await _context.PublicHolidays.ToListAsync();
    }

    // Add a new public holiday
    public async Task AddPublicHolidayAsync(PublicHoliday publicHoliday)
    {
        await _context.PublicHolidays.AddAsync(publicHoliday);
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
        var publicHoliday = await _context.PublicHolidays.FindAsync(id);
        if (publicHoliday != null)
        {
            _context.PublicHolidays.Remove(publicHoliday);
            await _context.SaveChangesAsync();
        }
    }

    internal HashSet<DateTime?>? GetPublicHolidays()
    {
        throw new NotImplementedException();
    }
}
