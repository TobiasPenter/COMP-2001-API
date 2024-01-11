using COMP2001_CW2.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace COMP2001_CW2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EndPoints : Controller
    {
        private readonly ContextForDB _context;

        public EndPoints(ContextForDB context)
        {
            _context = context;
        }

        //Runs my code  for ReadAccountDetails sql and returns the result or a relevant error message
        [HttpGet("Read Account Details")]
        public async Task<IActionResult> GetAccountDetails(string email)
        {
            try
            {
                var result = await _context.ReadAccountDetailsAsync(email);

                if (result != null && result.Any())
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound("No data found for the provided email.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        //Runs my code  for AllAccountDetailsAdmin sql and returns the result or a relevant error message
        [HttpGet("All Account Details Admin")]
        public async Task<IActionResult> GetAllAccountDetails()
        {
            try
            {
                var result = await _context.AllAccountDetailsAsync();

                if (result != null && result.Any())
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound("No data found for the provided email.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        //Runs my code for ReadAccountDetailsAdmin sql and returns the result or a relecant error message
        [HttpGet("Read Account Details Admin/Owner")]
        public async Task<IActionResult> GetAccountDetailsAdmin(string email)
        {
            try
            {
                var result = await _context.ReadAccountDetailsAdminAsync(email);

                if (result != null && result.Any())
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound("No data found for the provided email.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        //Runs my code for ReadAllActivities sql and returns the result or a relecant error message
        [HttpGet("Read All Activities")]
        public async Task<IActionResult> GetAllActivities()
        {
            try
            {
                var result = await _context.ReadAllActivitiesAsync();

                if (result != null && result.Any())
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound("No data found for the provided email.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        //Runs my code for ReadMeasurements sql and returns the result or a relecant error message
        [HttpGet("Read Measurements For Account")]
        public async Task<IActionResult> GetMeasurementsAsync(string email)
        {
            try
            {
                var result = await _context.ReadMeasurementsAsync(email);

                if (result != null && result.Any())
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound("No data found for the provided email.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        //Runs my code for ReadAccountActivities sql and returns the result or a relecant error message
        [HttpGet("Read Activities For Account")]
        public async Task<IActionResult> GetAccountActivities(string email)
        {
            try
            {
                var result = await _context.ReadAccountActivitiesAsync(email);

                if (result != null && result.Any())
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound("No data found for the provided email.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        //Adding a tag to an account
        [HttpPost("Add an Activity to Your Account")]
        public async Task<IActionResult> PostNewActivityAsync(string email, string activityName)
        {
            try
            {
                var activitiesInAccount = await _context.ReadAccountActivitiesAsync(email);
                if (activitiesInAccount.Any(a => a.ActivityName == activityName))
                {
                    return BadRequest("You already have this activity on the account");
                }
                else
                {
                    var activities = await _context.ReadAllActivitiesAsync();
                    var matchingActivity = activities.FirstOrDefault(a => a.ActivityName == activityName);
                    if (matchingActivity != null)
                    {
                        int id = matchingActivity.ActivityID;
                        _context.NewActivityLinkAsync(email, id);
                        return Ok("Activity saved");
                    }
                    else 
                    {
                        return BadRequest("Activity doesn't exist");
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}
