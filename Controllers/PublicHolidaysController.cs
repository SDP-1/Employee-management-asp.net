using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class PublicHolidaysController : ControllerBase
{
    private readonly PublicHolidayService _publicHolidayService;

    public PublicHolidaysController(PublicHolidayService publicHolidayService)
    {
        _publicHolidayService = publicHolidayService;
    }

    // Get all public holidays
    [HttpGet]
    public async Task<ActionResult<List<PublicHoliday>>> GetAllPublicHolidays()
    {
        var publicHolidays = await _publicHolidayService.GetAllPublicHolidaysAsync();
        return Ok(publicHolidays);
    }

    // Add a new public holiday
    [HttpPost]
    public async Task<ActionResult> AddPublicHoliday(PublicHoliday publicHoliday)
    {
        await _publicHolidayService.AddPublicHolidayAsync(publicHoliday);
        return CreatedAtAction(nameof(GetAllPublicHolidays), new { id = publicHoliday.Id }, publicHoliday);
    }

    // Update an existing public holiday
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdatePublicHoliday(int id, PublicHoliday publicHoliday)
    {
        if (id != publicHoliday.Id)
        {
            return BadRequest("Holiday ID mismatch");
        }

        await _publicHolidayService.UpdatePublicHolidayAsync(publicHoliday);
        return NoContent();
    }

    // Delete a public holiday
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeletePublicHoliday(int id)
    {
        await _publicHolidayService.DeletePublicHolidayAsync(id);
        return NoContent();
    }

    // Calculate the number of working days between two dates
    [HttpGet("CalculateWorkingDays")]
    public ActionResult<int> CalculateWorkingDays(DateTime startDate, DateTime endDate)
    {
        int workingDays = _publicHolidayService.CalculateWorkingDays(startDate, endDate);
        return Ok(workingDays);
    }
}
