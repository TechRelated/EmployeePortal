using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using EmployeePortalV1.Entities;
using EmployeePortalV1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace EmployeePortal.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;


        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostLoginAsync()
        { 
            try
            {
                AuthenticateModel userModel = new AuthenticateModel();
                //userModel.Username = "User1";
                //userModel.Password = "pass$word";

                userModel.Username = "test";
                userModel.Password = "test";

                //if (!ModelState.IsValid)
                //{
                //    return Page();
                //}

                // Verification.  
                //if (ModelState.IsValid)
                //{
                User ValidUser = await Login(userModel);
                
                // Verification. 
                if (ValidUser != null)
                {
                    //Save token in session object
                    HttpContext.Session.SetString("JWToken", ValidUser.Token.ToString());
                    // HttpContext.Session.SetString("JWToken", userToken.ToString());

                    // return new RedirectToPageResult("~/Employee/Dashboard");
                    // return RedirectToPage("Employee/Dashboard");

                    return RedirectToPage("loginsuccess");

                   // ViewBag.Message = "User logged in successfully!";
                }
                else
                {
                    // Setting.  
                    ModelState.AddModelError(string.Empty, "Invalid username or password.");
                }
                //}
            }
            catch (Exception ex)
            {
                // Info  
                Console.Write(ex);
            }
            return Page();
        }

        public async Task<User> Login(AuthenticateModel user)
        {
            User validuser = null;

            string baseUrl = "http://localhost:64558";

            using (var httpClient = new HttpClient())
            {
                HttpContent content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync(baseUrl+ "/Users/authenticate", content))
                {

                    if (!response.IsSuccessStatusCode)
                    {
                        throw new Exception("user not found");
                    }

                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        return null;
                    }

                    validuser = JsonConvert.DeserializeObject<User>(await response.Content.ReadAsStringAsync());
                }
            }
            return await Task.Run(() => validuser);
        }

        public IActionResult OnPostLogoutAsync()
        {
            HttpContext.Session.Remove("JWToken");

           // ViewBag.Message = "User logged out successfully!";
            return RedirectToPage("Index");
        }
        /*   public async Task<User> LoginUser(AuthenticateModel user)
           {
               User validuser = null;

               using (var httpClient = new HttpClient())
               {
                   HttpContent content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

                   using (var response = await httpClient.PostAsync("http://localhost:64466/Users/authenticate", content))
                   {

                       if (!response.IsSuccessStatusCode)
                       {
                           throw new Exception("user not found");
                       }

                       if (response.StatusCode == HttpStatusCode.Unauthorized)
                       {
                           return null;
                       }

                       validuser = JsonConvert.DeserializeObject<User>(await response.Content.ReadAsStringAsync());
                   }
               }
               return await Task.Run(() => validuser);
           }*/
    }
}
