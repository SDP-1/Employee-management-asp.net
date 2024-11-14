using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class PublicHolidayService
{
    private readonly PublicHolidayRepository _publicHolidayRepository;

    public PublicHolidayService(PublicHolidayRepository publicHolidayRepository)
    {
        _publicHolidayRepository = publicHolidayRepository;
    }

    // Get all public holidays
    public async Task<List<PublicHoliday>> GetAllPublicHolidaysAsync()
    {
        return await _publicHolidayRepository.GetAllPublicHolidaysAsync();
    }

    // Add a new public holiday
    public async Task AddPublicHolidayAsync(PublicHoliday publicHoliday)
    {
        await _publicHolidayRepository.AddPublicHolidayAsync(publicHoliday);
    }

    // Update an existing public holiday
    public async Task UpdatePublicHolidayAsync(PublicHoliday publicHoliday)
    {
        await _publicHolidayRepository.UpdatePublicHolidayAsync(publicHoliday);
    }

    // Delete a public holiday
    public async Task DeletePublicHolidayAsync(int id)
    {
        await _publicHolidayRepository.DeletePublicHolidayAsync(id);
    }

    // Calculate working days between two dates (excluding weekends and public holidays)
    public int CalculateWorkingDays(DateTime startDate, DateTime endDate)
    {
        // Adjust the start date to the next Monday if it's a weekend
        if (startDate.DayOfWeek == DayOfWeek.Saturday)
        {
            startDate = startDate.AddDays(2);
        }
        else if (startDate.DayOfWeek == DayOfWeek.Sunday)
        {
            startDate = startDate.AddDays(1);
        }

        // Initialize variables
        int workingDaysCount = 0;
        List<PublicHoliday> publicHolidays = _publicHolidayRepository.GetAllPublicHolidaysAsync().Result;

        // Loop through the dates between the start date and the end date
        for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
        {
            if (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday &&
                !publicHolidays.Any(ph => ph.Date == date.Date))
            {
                workingDaysCount++;
            }
        }

        return workingDaysCount;
    }
}
