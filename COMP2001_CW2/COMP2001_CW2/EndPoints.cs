using COMP2001_CW2.Context;
using COMP2001_CW2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using COMP2001_CW2.Models.Tables;

namespace COMP2001_CW2
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


        [HttpGet("readAccountDetails")]
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
    }
}
