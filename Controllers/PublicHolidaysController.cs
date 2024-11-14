using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class PublicHolidaysController : Controller
{
    private readonly PublicHolidayService _publicHolidayService;

    public PublicHolidaysController(PublicHolidayService publicHolidayService)
    {
        _publicHolidayService = publicHolidayService;
    }

    // GET: PublicHolidays/Manage
    public async Task<IActionResult> Manage()
    {
        var holidays = await _publicHolidayService.GetAllPublicHolidaysAsync();
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
        return RedirectToAction("Manage");  
    }

    // Optional: Calculate working days (if needed)
    [HttpGet("CalculateWorkingDays")]
    public ActionResult<int> CalculateWorkingDays(DateTime startDate, DateTime endDate)
    {
        int workingDays = _publicHolidayService.CalculateWorkingDays(startDate, endDate);
        return Ok(workingDays);
    }
}
