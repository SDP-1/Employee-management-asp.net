using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class PublicHolidaysController : Controller
{
    private readonly PublicHolidayService _publicHolidayService;
    private readonly IMemoryCache _cache;

    public PublicHolidaysController(PublicHolidayService publicHolidayService, IMemoryCache cache)
    {
        _publicHolidayService = publicHolidayService;
        _cache = cache;
    }

    // GET: PublicHolidays/Manage
    public async Task<IActionResult> Manage()
    {
        const string cacheKey = "PublicHolidaysList";

        // Try to get data from the cache
        if (!_cache.TryGetValue(cacheKey, out List<PublicHoliday> holidays))
        {
            // If the data is not in cache, get it from the service
            holidays = await _publicHolidayService.GetAllPublicHolidaysAsync();

            // Set cache options: 5-minute expiration
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromMinutes(5));

            // Store data in cache
            _cache.Set(cacheKey, holidays, cacheEntryOptions);
        }

        return View(holidays);
    }

    // GET: PublicHolidays/AddHoliday
    public IActionResult AddHoliday()
    {
        return View();
    }

    // POST: PublicHolidays/AddHoliday
    [HttpPost]
    public async Task<IActionResult> AddHoliday(PublicHoliday holiday)
    {
        if (ModelState.IsValid)
        {
            await _publicHolidayService.AddPublicHolidayAsync(holiday);

            // Clear cache after adding a new holiday to ensure fresh data
            _cache.Remove("PublicHolidaysList");

            return RedirectToAction("Manage");
        }
        return View(holiday);
    }

    // GET: PublicHolidays/Delete/5
    public async Task<IActionResult> Delete(int id)
    {
        var holiday = await _publicHolidayService.GetByIdAsync(id);
        if (holiday == null)
        {
            return NotFound();
        }
        return View(holiday);
    }

    // POST: PublicHolidays/Delete/5
    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _publicHolidayService.DeletePublicHolidayAsync(id);

        // Clear cache after deleting a holiday
        _cache.Remove("PublicHolidaysList");

        return RedirectToAction("Manage");
    }

    // GET: PublicHolidays/CalculateWorkingDays
    [HttpGet("CalculateWorkingDays")]
    public IActionResult CalculateWorkingDays(DateTime startDate, DateTime endDate)
    {
        string cacheKey = $"WorkingDays_{startDate:yyyyMMdd}_{endDate:yyyyMMdd}";

        if (!_cache.TryGetValue(cacheKey, out int workingDays))
        {
            // Calculate working days if not cached
            workingDays = _publicHolidayService.CalculateWorkingDays(startDate, endDate);

            // Set cache options with a 1-hour expiration for the calculated days
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromHours(1));

            _cache.Set(cacheKey, workingDays, cacheEntryOptions);
        }

        ViewBag.StartDate = startDate.ToString("yyyy-MM-dd");
        ViewBag.EndDate = endDate.ToString("yyyy-MM-dd");
        ViewBag.WorkingDays = workingDays;

        return View();
    }
}
