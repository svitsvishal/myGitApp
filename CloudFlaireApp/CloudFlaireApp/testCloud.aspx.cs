using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CloudFlaireApp
{
    public partial class testCloud : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected async void btnSubmit_Click(object sender, EventArgs e)
        {
            string token = hfTurnstileResponse.Value;
            bool isValid = await VerifyTurnstileToken(token);
            if (isValid)
            {
                // Token is valid, proceed with form submission
                Response.Write("CAPTCHA validation successful.");
            }
            else
            {
                // Token is invalid
                Response.Write("CAPTCHA validation failed.");
            }
        }
        private async Task<bool> VerifyTurnstileToken(string token)
        {
            using (HttpClient client = new HttpClient())
            {
                var postData = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("secret", "0x4AAAAAAAfV6NFrFcJ1sMhFPT9_DBqvOn8"),
                    new KeyValuePair<string, string>("response", token)
                });

                HttpResponseMessage response = await client.PostAsync("https://challenges.cloudflare.com/turnstile/v0/siteverify", postData);
                string result = await response.Content.ReadAsStringAsync();
                // Deserialize and check the success status
                dynamic jsonResult = Newtonsoft.Json.JsonConvert.DeserializeObject(result);
                return jsonResult.success;
            }
        }
    }
}