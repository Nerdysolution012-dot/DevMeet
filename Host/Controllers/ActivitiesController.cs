using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Host.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ActivitiesController(AppDbContext _context) : ControllerBase
{
    //private readonly AppDbContext _context;
   //// public ActivitiesController()
   // {
   //     _context = context; 

   // }

    [HttpGet]
    public async Task<ActionResult<List<Activity>>> GetActivities()
    {
        var activities = await _context.Activities.ToListAsync();
        return Ok(activities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Activity>> GetActivityById(string id)
    {
        var activity = await _context.Activities.FindAsync(id);
        if (activity == null)
        {
            return NotFound();
        }
        return Ok(activity);
    }

    [HttpPost]
    public async Task<ActionResult<Activity>> CreateActivity(Activity activity)
    {
        _context.Activities.Add(activity);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetActivityById), new { id = activity.Id }, activity);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteActivity(string id)
    {
        var activity = await _context.Activities.FindAsync(id);
        if (activity == null)
        {
            return NotFound();
        }
        _context.Activities.Remove(activity);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateActivity(string id, Activity updatedActivity)
    {
        var existingActivity = await _context.Activities.FindAsync(id); 
        if (existingActivity == null)
        {
            return NotFound();
        }
        existingActivity.Title = updatedActivity.Title;
        existingActivity.Date = updatedActivity.Date;
        existingActivity.Description = updatedActivity.Description;
        existingActivity.Category = updatedActivity.Category;
        existingActivity.IsCancelled = updatedActivity.IsCancelled;
        existingActivity.City = updatedActivity.City;
        existingActivity.Venue = updatedActivity.Venue;
        existingActivity.Latitude = updatedActivity.Latitude;
        existingActivity.Longitude = updatedActivity.Longitude;
        _context.Activities.Update(existingActivity);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
