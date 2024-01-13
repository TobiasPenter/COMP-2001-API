using COMP2001_CW2.Context;
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
        public async Task<IActionResult> GetAccountDetails([FromBody] UserInputs inputs)
        {
            try
            {
                var result = await _context.ReadAccountDetailsAsync((string)inputs.email);

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
        public async Task<IActionResult> GetAccountDetailsAdmin([FromBody] UserInputs inputs)
        {
            if (loginStatus == "TRUE")
            {
                if ((string)inputs.email == loginEmail || loginEmail == adminAccount)
                {
                    try
                    {
                        var result = await _context.ReadAccountDetailsAdminAsync((string)inputs.email);

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
        public async Task<IActionResult> GetMeasurementsAsync([FromBody] UserInputs inputs)
        {
            if (loginStatus == "TRUE")
            {
                if ((string)inputs.email == loginEmail || loginEmail == adminAccount)
                {
                    try
                    {
                        var result = await _context.ReadMeasurementsAsync((string)inputs.email);

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
        public async Task<IActionResult> GetAccountActivities([FromBody] UserInputs inputs)
        {
            if (loginStatus == "TRUE")
            {
                if ((string)inputs.email == loginEmail || loginEmail == adminAccount)
                {
                    try
                    {
                        var result = await _context.ReadAccountActivitiesAsync((string)inputs.email);

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
        public async Task<IActionResult> PostNewActivityAsync([FromBody] UserInputs inputs)
        {
            if (loginStatus == "TRUE")
            {
                if ((string)inputs.email == loginEmail || loginEmail == adminAccount)
                {
                    try
                    {
                        var activitiesInAccount = await _context.ReadAccountActivitiesAsync((string)inputs.email);
                        if (activitiesInAccount.Any(a => a.ActivityName == (string)inputs.activityName))
                        {
                            return BadRequest("You already have this activity on the account");
                        }
                        else
                        {
                            var activities = await _context.ReadAllActivitiesAsync();
                            var matchingActivity = activities.FirstOrDefault(a => a.ActivityName == (string)inputs.activityName);
                            if (matchingActivity != null)
                            {
                                int id = matchingActivity.ActivityID;
                                await _context.NewActivityLinkAsync((string)inputs.email, id);
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
        public async Task<IActionResult> PostNewActivityNameAsync([FromBody] UserInputs inputs)
        {
            if (loginStatus == "TRUE")
            {
                if (loginEmail == adminAccount)
                {
                    try
                    {
                        var activities = await _context.ReadAllActivitiesAsync();
                        var matchingActivity = activities.FirstOrDefault(a => a.ActivityName == (string)inputs.activityName);
                        if (matchingActivity != null)
                        {
                            return BadRequest("Activity already exists");
                        }
                        else
                        {
                            await _context.NewActivityNameAsync((string)inputs.activityName);
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
        public async Task<IActionResult> PostNewAccountAsync([FromBody] UserInputs inputs)
        {
            try
            {
                var accounts = await _context.AllAccountDetailsAsync();
                var takenAccount = accounts.FirstOrDefault(a => a.Email == (string)inputs.email);
                if (takenAccount != null)
                {
                    return BadRequest("Email already in use");
                }
                else
                {

                    DateOnly unformatedDate = (DateOnly)inputs.newBirthday;
                    string formatedDate = unformatedDate.ToString();

                    byte[] profilePictureBytes;
                    using (HttpClient client = new HttpClient()) { profilePictureBytes = await client.GetByteArrayAsync((string)inputs.profilePicture); }

                    await _context.NewUserAsync((string)inputs.email, (string)inputs.firstName, (string)inputs.lastName, (string)inputs.username, (string)inputs.password, profilePictureBytes, (string)inputs.aboutMe, (string)inputs.memberLocation, (bool)inputs.activitySpeedPacePreference, formatedDate);
                    await _context.NewMeasurementsAsync((string)inputs.email, (bool)inputs.units, (double)inputs.weight, (double)inputs.height);

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
        public async Task<IActionResult> DeleteAccountAsync([FromBody] UserInputs inputs)
        {
            if (loginStatus == "TRUE")
            {
                if (loginEmail == adminAccount)
                {
                    try
                    {
                        var accounts = await _context.AllAccountDetailsAsync();
                        var takenAccount = accounts.FirstOrDefault(a => a.Email == (string)inputs.email);
                        if (takenAccount != null)
                        {
                            await _context.DeleteAccountAsync((string)inputs.email);
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
        public async Task<IActionResult> DeleteActivityLinkAsync([FromBody] UserInputs inputs)
        {
            if (loginStatus == "TRUE")
            {
                if ((string)inputs.email == loginEmail || loginEmail == adminAccount)
                {
                    try
                    {
                        var activitiesInAccount = await _context.ReadAccountActivitiesAsync((string)inputs.email);
                        if (activitiesInAccount.Any(a => a.ActivityName == (string)inputs.activityName))
                        {
                            var activities = await _context.ReadAllActivitiesAsync();
                            var matchingActivity = activities.FirstOrDefault(a => a.ActivityName == (string)inputs.activityName);
                            if (matchingActivity != null)
                            {
                                int id = matchingActivity.ActivityID;
                                await _context.DeleteActivityFromAccountAsync((string)inputs.email, id);
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
        public async Task<IActionResult> DeleteActivityAsync([FromBody] UserInputs inputs)
        {
            if (loginStatus == "TRUE")
            {
                if (loginEmail == adminAccount)
                {
                    try
                    {
                        var activities = await _context.ReadAllActivitiesAsync();
                        var matchingActivity = activities.FirstOrDefault(a => a.ActivityName == (string)inputs.activityName);
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
        public async Task<IActionResult> PutAboutMe([FromBody] UserInputs inputs)
        {
            if (loginStatus == "TRUE")
            {
                try
                {
                    var accounts = await _context.AllAccountDetailsAsync();
                    var matchingAccounts = accounts.FirstOrDefault(a => a.Email == (string)inputs.email);
                    if (matchingAccounts != null)
                    {
                        if ((string)inputs.email == loginEmail)
                        {
                            await _context.UpdateAboutMeAsync((string)inputs.email, (string)inputs.newAboutMe);
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
        public async Task<IActionResult> PutActivtiySpeedPacePreference([FromBody] UserInputs inputs)
        {
            if (loginStatus == "TRUE")
            {
                try
                {
                    var accounts = await _context.AllAccountDetailsAsync();
                    var matchingAccounts = accounts.FirstOrDefault(a => a.Email == (string)inputs.email);
                    if (matchingAccounts != null)
                    {
                        if ((string)inputs.email == loginEmail)
                        {
                            await _context.UpdateActivitySpeedPacePreferenceAsync((string)inputs.email, (bool)inputs.newActivitySpeedPacePreference);
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
        public async Task<IActionResult> PutBirthday([FromBody] UserInputs inputs)
        {
            if (loginStatus == "TRUE")
            {
                try
                {
                    var accounts = await _context.AllAccountDetailsAsync();
                    var matchingAccounts = accounts.FirstOrDefault(a => a.Email == (string)inputs.email);
                    if (matchingAccounts != null)
                    {
                        if ((string)inputs.email == loginEmail)
                        {
                            DateOnly unformatedDate = (DateOnly)inputs.newBirthday;
                            string formatedDate = unformatedDate.ToString();

                            await _context.UpdateBirthdayAsync((string)inputs.email, formatedDate);
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
        public async Task<IActionResult> PutEmail([FromBody] UserInputs inputs)
        {
            if (loginStatus == "TRUE")
            {
                try
                {
                    var accounts = await _context.AllAccountDetailsAsync();
                    var matchingAccounts = accounts.FirstOrDefault(a => a.Email == (string)inputs.email);
                    if (matchingAccounts != null)
                    {
                        if ((string)inputs.email == loginEmail)
                        {
                            if (accounts.FirstOrDefault(a => a.Email == (string)inputs.newEmail) == null)
                            {
                                await _context.UpdateEmailAsync((string)inputs.email, (string)inputs.newEmail);
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
        public async Task<IActionResult> PutFirstName([FromBody] UserInputs inputs)
        {
            try
            {
                var accounts = await _context.AllAccountDetailsAsync();
                var matchingAccounts = accounts.FirstOrDefault(a => a.Email == (string)inputs.email);
                if (matchingAccounts != null)
                {
                    if ((string)inputs.email == loginEmail || loginEmail == adminAccount)
                    {
                        await _context.UpdateFirstNameAsync((string)inputs.email, (string)inputs.newFirstName);
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
        public async Task<IActionResult> PutHeight([FromBody] UserInputs inputs)
        {
            if (loginStatus == "TRUE")
            {
                try
                {
                    var accounts = await _context.AllAccountDetailsAsync();
                    var matchingAccounts = accounts.FirstOrDefault(a => a.Email == (string)inputs.email);
                    if (matchingAccounts != null)
                    {
                        if ((string)inputs.email == loginEmail)
                        {
                            await _context.UpdateHeightAsync((string)inputs.email, (double)inputs.newHeight);
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
        public async Task<IActionResult> PutLastName([FromBody] UserInputs inputs)
        {
            if (loginStatus == "TRUE")
            {
                try
                {
                    var accounts = await _context.AllAccountDetailsAsync();
                    var matchingAccounts = accounts.FirstOrDefault(a => a.Email == (string)inputs.email);
                    if (matchingAccounts != null)
                    {
                        if ((string)inputs.email == loginEmail || loginEmail == adminAccount)
                        {
                            await _context.UpdateLastNameAsync((string)inputs.email, (string)inputs.newLastName);
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
        public async Task<IActionResult> PutMemberLocation([FromBody] UserInputs inputs)
        {
            if (loginStatus == "TRUE")
            {
                try
                {
                    var accounts = await _context.AllAccountDetailsAsync();
                    var matchingAccounts = accounts.FirstOrDefault(a => a.Email == (string)inputs.email);
                    if (matchingAccounts != null)
                    {
                        if ((string)inputs.email == loginEmail)
                        {
                            await _context.UpdateMemberLocationAsync((string)inputs.email, (string)inputs.newMemberLocation);
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
        public async Task<IActionResult> PutProfilePicture([FromBody] UserInputs inputs)
        {
            if (loginStatus == "TRUE")
            {
                try
                {
                    var accounts = await _context.AllAccountDetailsAsync();
                    var matchingAccounts = accounts.FirstOrDefault(a => a.Email == (string)inputs.email);
                    if (matchingAccounts != null)
                    {
                        if ((string)inputs.email == loginEmail)
                        {
                            byte[] profilePictureBytes;
                            using (HttpClient client = new HttpClient()) { profilePictureBytes = await client.GetByteArrayAsync((string)inputs.newProfilePicture); }

                            await _context.UpdateProfilePictureAsync((string)inputs.email, profilePictureBytes);
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
        public async Task<IActionResult> PutUnits([FromBody] UserInputs inputs)
        {
            if (loginStatus == "TRUE")
            {
                try
                {
                    var accounts = await _context.AllAccountDetailsAsync();
                    var matchingAccounts = accounts.FirstOrDefault(a => a.Email == (string)inputs.email);
                    if (matchingAccounts != null)
                    {
                        if ((string)inputs.email == loginEmail)
                        {
                            await _context.UpdateUnitsAsync((string)inputs.email, (bool)inputs.newUnits);
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
        public async Task<IActionResult> PutUsername([FromBody] UserInputs inputs)
        {
            if (loginStatus == "TRUE")
            {
                try
                {
                    var accounts = await _context.AllAccountDetailsAsync();
                    var matchingAccounts = accounts.FirstOrDefault(a => a.Email == (string)inputs.email);
                    if (matchingAccounts != null)
                    {
                        if ((string)inputs.email == loginEmail)
                        {
                            await _context.UpdateUsernameAsync((string)inputs.email, (string)inputs.newUsername);
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
        public async Task<IActionResult> PutPassword([FromBody] UserInputs inputs)
        {
            if (loginStatus == "TRUE")
            {
                try
                {
                    var accounts = await _context.AllAccountDetailsAsync();
                    var matchingAccounts = accounts.FirstOrDefault(a => a.Email == (string)inputs.email);
                    if (matchingAccounts != null)
                    {
                        if ((string)inputs.email == loginEmail)
                        {
                            await _context.UpdateUserPasswordAsync((string)inputs.email, (string)inputs.newUserPassword);
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
        public async Task<IActionResult> PutWeight([FromBody] UserInputs inputs)
        {
            if (loginStatus == "TRUE")
            {
                try
                {
                    var accounts = await _context.AllAccountDetailsAsync();
                    var matchingAccounts = accounts.FirstOrDefault(a => a.Email == (string)inputs.email);
                    if (matchingAccounts != null)
                    {
                        if ((string)inputs.email == loginEmail)
                        {
                            await _context.UpdateWeightAsync((string)inputs.email, (double)inputs.newWeight);
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
