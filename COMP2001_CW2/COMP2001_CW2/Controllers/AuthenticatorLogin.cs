using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace COMP2001_CW2.Controllers
{

    [ApiController]
    [Route("/Admin_Check")]
    public class AuthenticatorLogin : Controller
    {

        [HttpPost]
        public async Task<IActionResult> AdminCheck()
        {
            //Make a reader for API
            StreamReader reading = new StreamReader(Request.Body);
            string data = await reading.ReadToEndAsync();

            //Set request layout
            RequestLayout account = JsonSerializer.Deserialize<RequestLayout>(data);
            EndPoints.loginEmail = account.Email;
            using (HttpClient client = new HttpClient())
            {
                //Send request for data
                HttpRequestMessage request = new HttpRequestMessage();
                request.RequestUri = new Uri("https://web.socem.plymouth.ac.uk/COMP2001/auth/api/users");
                request.Method = HttpMethod.Post;
                request.Content = new StringContent(JsonSerializer.Serialize<RequestLayout>(account), Encoding.UTF8, "application/json");

                //Get data back
                HttpResponseMessage responce = await client.SendAsync(request);
                string responceString = await responce.Content.ReadAsStringAsync();
                string[] responceArray = JsonSerializer.Deserialize<string[]>(responceString);

                //Check data
                if (responceArray != null)
                {
                    return StatusCode(400, "Bad Request");
                }
                else
                {
                    EndPoints.loginStatus = responceArray[1].ToUpper();
                    return Ok();
                }
            }
        }

        public struct RequestLayout
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }

    }
}
