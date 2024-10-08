﻿using COMP2001_CW2.Context;
using COMP2001_CW2.Models;
using Microsoft.AspNetCore.Mvc;

namespace COMP2001_CW2.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class EndPoints : Controller
    {
        //Hard coded admin (didn't know how else to do this).
        private static string adminAccount = "grace@plymouth.ac.uk";

        public static string loginStatus = "FALSE";

        public static string loginEmail = "";

        private readonly ContextForDB _context;

        public EndPoints(ContextForDB context)
        {
            _context = context;
        }

        //Runs my code  for ReadAccountDetails sql and returns the result or a relevant error message
        [HttpGet("Read_Account_Details")]
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
        [HttpGet("All_Account_Details_Admin")]
        public async Task<IActionResult> GetAllAccountDetails()
        {
            if (loginStatus == "TRUE")
            {
                if (loginEmail == adminAccount)
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
                return BadRequest("You don't have permision to run this command");
            }
            return BadRequest("Please Login");
        }

        //Runs my code for ReadAccountDetailsAdmin sql and returns the result or a relecant error message
        [HttpGet("Read_Account_Details_Admin_Owner")]
        public async Task<IActionResult> GetAccountDetailsAdmin(string email)
        {
            if (loginStatus == "TRUE")
            {
                if (email == loginEmail || loginEmail == adminAccount)
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
                return BadRequest("You don't have permision to run this command");
            }
            return BadRequest("Please Login");
        }

        //Runs my code for ReadAllActivities sql and returns the result or a relecant error message
        [HttpGet("Read_All_Activities")]
        public async Task<IActionResult> GetAllActivities()
        {
            if (loginStatus == "TRUE")
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
            return BadRequest("Please Login");
        }

        //Runs my code for ReadMeasurements sql and returns the result or a relecant error message
        [HttpGet("Read_Measurements_For_Account")]
        public async Task<IActionResult> GetMeasurementsAsync(string email)
        {
            if (loginStatus == "TRUE")
            {
                if (email == loginEmail || loginEmail == adminAccount)
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
                return BadRequest("You don't have permision to run this command");
            }
            return BadRequest("Please Login");
        }

        //Runs my code for ReadAccountActivities sql and returns the result or a relecant error message
        [HttpGet("Read_Activities_For_Account")]
        public async Task<IActionResult> GetAccountActivities(string email)
        {
            if (loginStatus == "TRUE")
            {
                if (email == loginEmail || loginEmail == adminAccount)
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
                return BadRequest("You don't have permision to run this command");
            }
            return BadRequest("Please Login");
        }

        //Adding a tag to an account
        [HttpPost("Add_an_Activity_to_Your_Account")]
        public async Task<IActionResult> PostNewActivityAsync(string email, string activityName)
        {
            if (loginStatus == "TRUE")
            {
                if (email == loginEmail || loginEmail == adminAccount)
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
                            var matchingActivity = activities.FirstOrDefault(a => a.ActivityName == (activityName));
                            if (matchingActivity != null)
                            {
                                int id = matchingActivity.ActivityID;
                                await _context.NewActivityLinkAsync(email, id);
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
                return BadRequest("You don't have permision to run this command");
            }
            return BadRequest("Please Login");
        }

        //Adding a new tag option
        [HttpPost("Add_an_Activity_to_the_Database_Admin")]
        public async Task<IActionResult> PostNewActivityNameAsync(string activityName)
        {
            if (loginStatus == "TRUE")
            {
                if (loginEmail == adminAccount)
                {
                    try
                    {
                        var activities = await _context.ReadAllActivitiesAsync();
                        var matchingActivity = activities.FirstOrDefault(a => a.ActivityName == activityName);
                        if (matchingActivity != null)
                        {
                            return BadRequest("Activity already exists");
                        }
                        else
                        {
                            await _context.NewActivityNameAsync(activityName);
                            return Ok("New activity made");
                        }
                    }
                    catch (Exception ex)
                    {
                        return StatusCode(500, $"Internal Server Error: {ex.Message}");
                    }
                }
                return BadRequest("You don't have permision to run this command");
            }
            return BadRequest("Please Login");
        }

        //Adding a new user
        [HttpPost("Create_an_Account")]
        public async Task<IActionResult> PostNewAccountAsync(string email, string username, string firstName, string lastName, string password, DateOnly newBirthday, string profilePicture, string aboutMe, string memberLocation, bool activitySpeedPacePreference, bool units, double weight, double height)
        {
            try
            {
                var accounts = await _context.AllAccountDetailsAsync();
                var takenAccount = accounts.FirstOrDefault(a => a.Email == email);
                if (takenAccount != null)
                {
                    return BadRequest("Email already in use");
                }
                else
                {

                    DateOnly unformatedDate = newBirthday;
                    string formatedDate = unformatedDate.ToString();

                    byte[] profilePictureBytes;
                    using (HttpClient client = new HttpClient()) { profilePictureBytes = await client.GetByteArrayAsync(profilePicture); }

                    await _context.NewUserAsync(email, firstName, lastName, username, password, profilePictureBytes, aboutMe, memberLocation, activitySpeedPacePreference, formatedDate);
                    await _context.NewMeasurementsAsync(email, units, weight, height);

                    return Ok("Account made");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        //Delete an account
        [HttpDelete("Delete_a_User_Admin")]
        public async Task<IActionResult> DeleteAccountAsync(string email)
        {
            if (loginStatus == "TRUE")
            {
                if (loginEmail == adminAccount)
                {
                    try
                    {
                        var accounts = await _context.AllAccountDetailsAsync();
                        var takenAccount = accounts.FirstOrDefault(a => a.Email == email);
                        if (takenAccount != null)
                        {
                            await _context.DeleteAccountAsync(email);
                            return Ok("Account deleted");
                        }
                        else
                        {
                            return BadRequest("The account doesn't exist");
                        }
                    }
                    catch (Exception ex)
                    {
                        return StatusCode(500, $"Internal Server Error: {ex.Message}");
                    }
                }
                return BadRequest("You don't have permision to run this command");
            }
            return BadRequest("Please Login");
        }

        //Delete an activity linked to an account
        [HttpDelete("Delete_an_Activity_From_Your_Account")]
        public async Task<IActionResult> DeleteActivityLinkAsync(string email, string activityName)
        {
            if (loginStatus == "TRUE")
            {
                if (email == loginEmail || loginEmail == adminAccount)
                {
                    try
                    {
                        var activitiesInAccount = await _context.ReadAccountActivitiesAsync(email);
                        if (activitiesInAccount.Any(a => a.ActivityName == activityName))
                        {
                            var activities = await _context.ReadAllActivitiesAsync();
                            var matchingActivity = activities.FirstOrDefault(a => a.ActivityName == activityName);
                            if (matchingActivity != null)
                            {
                                int id = matchingActivity.ActivityID;
                                await _context.DeleteActivityFromAccountAsync(email, id);
                                return Ok("Activity deleted");
                            }
                            else
                            {
                                return BadRequest("Activity doesn't exist");
                            }
                        }
                        else
                        {
                            return BadRequest("You don't have this activity on the account");
                        }
                    }
                    catch (Exception ex)
                    {
                        return StatusCode(500, $"Internal Server Error: {ex.Message}");
                    }
                }
                return BadRequest("You don't have permision to run this command");
            }
            return BadRequest("Please Login");
        }

        //Delete an activity linked to an account
        [HttpDelete("Delete_an_Activity_From_System_Admin")]
        public async Task<IActionResult> DeleteActivityAsync(string activityName)
        {
            if (loginStatus == "TRUE")
            {
                if (loginEmail == adminAccount)
                {
                    try
                    {
                        var activities = await _context.ReadAllActivitiesAsync();
                        var matchingActivity = activities.FirstOrDefault(a => a.ActivityName == activityName);
                        if (matchingActivity != null)
                        {
                            int id = matchingActivity.ActivityID;
                            await _context.DeleteActivityFromSystemAsync(id);
                            return Ok("Activity deleted");
                        }
                        else
                        {
                            return BadRequest("Activity doesn't exist");
                        }
                    }
                    catch (Exception ex)
                    {
                        return StatusCode(500, $"Internal Server Error: {ex.Message}");
                    }
                }
                return BadRequest("You don't have permision to run this command");
            }
            return BadRequest("Please Login");
        }

        //Update the about me of an account
        [HttpPut("Update_Your_About_Me")]
        public async Task<IActionResult> PutAboutMe(string email, string newAboutMe)
        {
            if (loginStatus == "TRUE")
            {
                try
                {
                    var accounts = await _context.AllAccountDetailsAsync();
                    var matchingAccounts = accounts.FirstOrDefault(a => a.Email == email);
                    if (matchingAccounts != null)
                    {
                        if (email == loginEmail)
                        {
                            await _context.UpdateAboutMeAsync(email, newAboutMe);
                            return Ok("About Me updated");
                        }
                        else
                        {
                            return BadRequest("You don't have permision to access this account");
                        }
                    }
                    {
                        return BadRequest("This account doesnt exist");
                    }
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Internal Server Error: {ex.Message}");
                }
            }
            return BadRequest("Please Login");
        }

        //Update the activity speed pace preference of an account
        [HttpPut("Update_Your_Activity_Speed_Pace_Preference")]
        public async Task<IActionResult> PutActivtiySpeedPacePreference(string email, bool newActivitySpeedPacePreference)
        {
            if (loginStatus == "TRUE")
            {
                try
                {
                    var accounts = await _context.AllAccountDetailsAsync();
                    var matchingAccounts = accounts.FirstOrDefault(a => a.Email == email);
                    if (matchingAccounts != null)
                    {
                        if (email == loginEmail)
                        {
                            await _context.UpdateActivitySpeedPacePreferenceAsync(email, newActivitySpeedPacePreference);
                            return Ok("Activity speed/pace preference updated");
                        }
                        else
                        {
                            return BadRequest("You don't have permision to access this account");
                        }
                    }
                    else
                    {
                        return BadRequest("This account doesnt exist");
                    }
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Internal Server Error: {ex.Message}");
                }
            }
            return BadRequest("Please Login");
        }

        //Update the birthday of an account
        [HttpPut("Update_Your_Birthday")]
        public async Task<IActionResult> PutBirthday(string email, DateOnly newBirthday)
        {
            if (loginStatus == "TRUE")
            {
                try
                {
                    var accounts = await _context.AllAccountDetailsAsync();
                    var matchingAccounts = accounts.FirstOrDefault(a => a.Email == email);
                    if (matchingAccounts != null)
                    {
                        if (email == loginEmail)
                        {
                            DateOnly unformatedDate = newBirthday;
                            string formatedDate = unformatedDate.ToString();

                            await _context.UpdateBirthdayAsync(email, formatedDate);
                            return Ok("Birthday updated");
                        }
                        else
                        {
                            return BadRequest("You don't have permision to access this account");
                        }
                    }
                    {
                        return BadRequest("This account doesnt exist");
                    }
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Internal Server Error: {ex.Message}");
                }
            }
            return BadRequest("Please Login");
        }

        //Update the email of an account
        [HttpPut("Update_Your_Email")]
        public async Task<IActionResult> PutEmail(string email, string newEmail)
        {
            if (loginStatus == "TRUE")
            {
                try
                {
                    var accounts = await _context.AllAccountDetailsAsync();
                    var matchingAccounts = accounts.FirstOrDefault(a => a.Email == email);
                    if (matchingAccounts != null)
                    {
                        if (email == loginEmail)
                        {
                            if (accounts.FirstOrDefault(a => a.Email == newEmail) == null)
                            {
                                await _context.UpdateEmailAsync(email, newEmail);
                                return Ok("Email updated");
                            }
                            else
                            {
                                return BadRequest("Email in use");
                            }
                        }
                        else
                        {
                            return BadRequest("You don't have permision to access this account");
                        }
                    }
                    {
                        return BadRequest("This account doesnt exist");
                    }
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Internal Server Error: {ex.Message}");
                }
            }
            return BadRequest("Please Login");
        }

        //Update the first name of an account
        [HttpPut("Update_Your_First_Name")]
        public async Task<IActionResult> PutFirstName(string email, string newFirstName)
        {
            try
            {
                var accounts = await _context.AllAccountDetailsAsync();
                var matchingAccounts = accounts.FirstOrDefault(a => a.Email == email);
                if (matchingAccounts != null)
                {
                    if (email == loginEmail || loginEmail == adminAccount)
                    {
                        await _context.UpdateFirstNameAsync(email, newFirstName);
                        return Ok("First name updated");
                    }
                    else
                    {
                        return BadRequest("You don't have permision to access this account");
                    }
                }
                {
                    return BadRequest("This account doesnt exist");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        //Update the height of an account
        [HttpPut("Update_Your_Height")]
        public async Task<IActionResult> PutHeight(string email, double newHeight)
        {
            if (loginStatus == "TRUE")
            {
                try
                {
                    var accounts = await _context.AllAccountDetailsAsync();
                    var matchingAccounts = accounts.FirstOrDefault(a => a.Email == email);
                    if (matchingAccounts != null)
                    {
                        if (email == loginEmail)
                        {
                            await _context.UpdateHeightAsync(email, newHeight);
                            return Ok("Height updated");
                        }
                        else
                        {
                            return BadRequest("You don't have permision to access this account");
                        }
                    }
                    {
                        return BadRequest("This account doesnt exist");
                    }
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Internal Server Error: {ex.Message}");
                }
            }
            return BadRequest("Please Login");
        }

        //Update the last name of an account
        [HttpPut("Update_Your_Last_Name")]
        public async Task<IActionResult> PutLastName(string email, string newLastName)
        {
            if (loginStatus == "TRUE")
            {
                try
                {
                    var accounts = await _context.AllAccountDetailsAsync();
                    var matchingAccounts = accounts.FirstOrDefault(a => a.Email == email);
                    if (matchingAccounts != null)
                    {
                        if (email == loginEmail || loginEmail == adminAccount)
                        {
                            await _context.UpdateLastNameAsync(email, newLastName);
                            return Ok("Last name updated");
                        }
                        else
                        {
                            return BadRequest("You don't have permision to access this account");
                        }
                    }
                    {
                        return BadRequest("This account doesnt exist");
                    }
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Internal Server Error: {ex.Message}");
                }
            }
            return BadRequest("Please Login");
        }

        //Update the member location of an account
        [HttpPut("Update_Your_Member_Location")]
        public async Task<IActionResult> PutMemberLocation(string email, string newMemberLocation)
        {
            if (loginStatus == "TRUE")
            {
                try
                {
                    var accounts = await _context.AllAccountDetailsAsync();
                    var matchingAccounts = accounts.FirstOrDefault(a => a.Email == email);
                    if (matchingAccounts != null)
                    {
                        if (email == loginEmail)
                        {
                            await _context.UpdateMemberLocationAsync(email, newMemberLocation);
                            return Ok("Member Location updated");
                        }
                        else
                        {
                            return BadRequest("You don't have permision to access this account");
                        }
                    }
                    {
                        return BadRequest("This account doesnt exist");
                    }
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Internal Server Error: {ex.Message}");
                }
            }
            return BadRequest("Please Login");
        }

        //Update the profile picture of an account
        [HttpPut("Update_Your_Profile_Picture")]
        public async Task<IActionResult> PutProfilePicture(string email, string newProfilePicture)
        {
            if (loginStatus == "TRUE")
            {
                try
                {
                    var accounts = await _context.AllAccountDetailsAsync();
                    var matchingAccounts = accounts.FirstOrDefault(a => a.Email == email);
                    if (matchingAccounts != null)
                    {
                        if (email == loginEmail)
                        {
                            byte[] profilePictureBytes;
                            using (HttpClient client = new HttpClient()) { profilePictureBytes = await client.GetByteArrayAsync(newProfilePicture); }

                            await _context.UpdateProfilePictureAsync(email, profilePictureBytes);
                            return Ok("Profile picture updated");
                        }
                        else
                        {
                            return BadRequest("You don't have permision to access this account");
                        }
                    }
                    {
                        return BadRequest("This account doesnt exist");
                    }
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Internal Server Error: {ex.Message}");
                }
            }
            return BadRequest("Please Login");
        }

        //Update the units of an account
        [HttpPut("Update_Your_Units")]
        public async Task<IActionResult> PutUnits(string email, bool newUnits)
        {
            if (loginStatus == "TRUE")
            {
                try
                {
                    var accounts = await _context.AllAccountDetailsAsync();
                    var matchingAccounts = accounts.FirstOrDefault(a => a.Email == email);
                    if (matchingAccounts != null)
                    {
                        if (email == loginEmail)
                        {
                            await _context.UpdateUnitsAsync(email, newUnits);
                            return Ok("Units updated");
                        }
                        else
                        {
                            return BadRequest("You don't have permision to access this account");
                        }
                    }
                    {
                        return BadRequest("This account doesnt exist");
                    }
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Internal Server Error: {ex.Message}");
                }
            }
            return BadRequest("Please Login");
        }

        //Update the username of an account
        [HttpPut("Update_Your_Username")]
        public async Task<IActionResult> PutUsername(string email, string newUsername)
        {
            if (loginStatus == "TRUE")
            {
                try
                {
                    var accounts = await _context.AllAccountDetailsAsync();
                    var matchingAccounts = accounts.FirstOrDefault(a => a.Email == email);
                    if (matchingAccounts != null)
                    {
                        if (email == loginEmail)
                        {
                            await _context.UpdateUsernameAsync(email, newUsername);
                            return Ok("Username updated");
                        }
                        else
                        {
                            return BadRequest("You don't have permision to access this account");
                        }
                    }
                    {
                        return BadRequest("This account doesnt exist");
                    }
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Internal Server Error: {ex.Message}");
                }
            }
            return BadRequest("Please Login");
        }

        //Update the Password of an account
        [HttpPut("Update_Your_Password")]
        public async Task<IActionResult> PutPassword(string email, string newUserPassword)
        {
            if (loginStatus == "TRUE")
            {
                try
                {
                    var accounts = await _context.AllAccountDetailsAsync();
                    var matchingAccounts = accounts.FirstOrDefault(a => a.Email == email);
                    if (matchingAccounts != null)
                    {
                        if (email == loginEmail)
                        {
                            await _context.UpdateUserPasswordAsync(email, newUserPassword);
                            return Ok("Password updated");
                        }
                        else
                        {
                            return BadRequest("You don't have permision to access this account");
                        }
                    }
                    {
                        return BadRequest("This account doesnt exist");
                    }
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Internal Server Error: {ex.Message}");
                }
            }
            return BadRequest("Please Login");
        }

        //Update the weight for an account
        [HttpPut("Update_Your_Weight")]
        public async Task<IActionResult> PutWeight(string email, double newWeight)
        {
            if (loginStatus == "TRUE")
            {
                try
                {
                    var accounts = await _context.AllAccountDetailsAsync();
                    var matchingAccounts = accounts.FirstOrDefault(a => a.Email == email);
                    if (matchingAccounts != null)
                    {
                        if (email == loginEmail)
                        {
                            await _context.UpdateWeightAsync(email, newWeight);
                            return Ok("Weight updated");
                        }
                        else
                        {
                            return BadRequest("You don't have permision to access this account");
                        }
                    }
                    {
                        return BadRequest("This account doesnt exist");
                    }
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Internal Server Error: {ex.Message}");
                }
            }
            return BadRequest("Please Login");
        }
    }
}
