

public class PublicHolidayRepository
{
    private readonly MyDbContext _context;

    public PublicHolidayRepository(MyDbContext context)
    {
        _context = context;
    }

    public IEnumerable<PublicHoliday> GetPublicHolidays() => _context.PublicHolidays.ToList();
}
