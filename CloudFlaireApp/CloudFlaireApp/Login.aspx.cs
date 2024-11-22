using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.UI;

namespace CloudFlaireApp
{
    public partial class Login : Page
    {
        private const string SecretKey = "0x4AAAAAAAfV6NFrFcJ1sMhFPT9_DBqvOn8";

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected async void btnLogin_Click(object sender, EventArgs e)
        {
            var turnstileResponse = Request.Form["cf-turnstile-response"];

            // Log the response for debugging purposes
            System.Diagnostics.Debug.WriteLine("Turnstile response: " + turnstileResponse);

            if (string.IsNullOrEmpty(turnstileResponse))
            {
             
                lblMessage.Text = "CAPTCHA response is missing. Please complete the CAPTCHA.";
                return;
            }

            var isValidCaptcha = await ValidateTurnstile(turnstileResponse);

            if (!isValidCaptcha)
            {
                lblMessage.Text = "Invalid CAPTCHA. Please try again.";
                return;
            }

            // Simulate login logic here (for example, check against a database)
            if (  txtUsername.Text == "admin" && txtPassword.Text == "password")
            {
                // Successful login
                Response.Redirect("Default.aspx");//new changes 
            }
            else
            {
                // Invalid login
                lblMessage.Text = "Invalid username or password.";
            }
        }

        private async Task<bool> ValidateTurnstile(string response)
        {
            using (var client = new HttpClient())
            {
                var values = new Dictionary<string, string>
                {
                    { "secret", SecretKey },
                    { "response", response }
                };

                var content = new FormUrlEncodedContent(values);
                var responseMessage = await client.PostAsync("https://challenges.cloudflare.com/turnstile/v0/siteverify", content);
                var responseString = await responseMessage.Content.ReadAsStringAsync();
                dynamic jsonResponse = Newtonsoft.Json.JsonConvert.DeserializeObject(responseString);

                return jsonResponse.success == true;
            }
        }
    }
}