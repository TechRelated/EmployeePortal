using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using EmployeePortalV1.Entities;
using EmployeePortalV1.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace EmployeePortal.Pages
{
    public class IndexModel1 : PageModel
    {
        private readonly ILogger<IndexModel1> _logger;
        [BindProperty] // Bind on Post
        public User loginData { get; set; }

        public IndexModel1(ILogger<IndexModel1> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostLoginAsync()
        {
            //if (ModelState.IsValid)
            //{
                //var isValid = (loginData.Username == "test" && loginData.Password == "test"); // TODO Validate the username and the password with your own logic

                //if (!isValid)
                //{
                //    ModelState.AddModelError("", "username or password is invalid");
                //    return Page();
                //}
                //// Create the identity from the user info
                //var identity = new ClaimsIdentity(JwtBearerDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
                //identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, loginData.Username));
                //identity.AddClaim(new Claim(ClaimTypes.Name, loginData.Username));
                //// Authenticate using the identity
                //var principal = new ClaimsPrincipal(identity);

                //await HttpContext.SignInAsync(JwtBearerDefaults.AuthenticationScheme, principal);

                try
                {
                    AuthenticateModel userModel = new AuthenticateModel();
                    userModel.Username = loginData.Username;//"test";
                    userModel.Password = loginData.Password;

                if (!ModelState.IsValid)
                {
                    return Page();
                }

                // Verification.  
                //if (ModelState.IsValid)
                //{
                User ValidUser = await Login(userModel);

                    // Verification. 
                    if (ValidUser != null)
                    {

                    //Save token in session object
                    HttpContext.Session.SetString("JWToken", ValidUser.Token.ToString());
                    // Create the identity from the user info
                    var identity = new ClaimsIdentity(JwtBearerDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
                    identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, loginData.Username));
                    identity.AddClaim(new Claim(ClaimTypes.Name, loginData.Username));
                    // Authenticate using the identity
                    var principal = new ClaimsPrincipal(identity);

                    await HttpContext.SignInAsync(JwtBearerDefaults.AuthenticationScheme, principal,
                            new AuthenticationProperties
                            {
                                IsPersistent = true,
                                ExpiresUtc = DateTime.UtcNow.AddSeconds(20)
                            });

                    /* Use this if wish to use cookie authentication*/
                    //var claims = new List<Claim>
                    //{
                    //    new Claim(ClaimTypes.Name, loginData.Username)
                    //};
                    //var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    //await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity),
                    //    new AuthenticationProperties
                    //    {
                    //        IsPersistent = true,
                    //        ExpiresUtc = DateTime.UtcNow.AddSeconds(20)
                    //    });
                    /* Use this if wish to use cookie authentication*/
                    return RedirectToPage("loginsuccess");
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

            //}
            //else
            //{
            //    ModelState.AddModelError("", "username or password is blank");
            //    return Page();
            //}

        }

        public async Task<User> Login(AuthenticateModel user)
        {
            User validuser = null;

            string baseUrl = "http://localhost:64558";

            //HttpClient client = new HttpClient();
            //client.SetBearerToken(tokenResponse.AccessToken);

            //var response = await client.GetAsync("http://localhost:52037/api/values");
            //var content = await response.Content.ReadAsStringAsync();
            //Console.WriteLine(JArray.Parse(content));

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

        //[Authorize]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(
            CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToPage("index");
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
