using EmployeePortalV1.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EmployeePortal.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty] // Bind on Post
        public User loginData { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var isValid = (loginData.Username == "test" && loginData.Password == "test"); // TODO Validate the username and the password with your own logic

                if (!isValid)
                {
                    ModelState.AddModelError("", "username or password is invalid");
                    return Page();
                }
                //// Create the identity from the user info
                //var identity = new ClaimsIdentity(JwtBearerDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
                //identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, loginData.Username));
                //identity.AddClaim(new Claim(ClaimTypes.Name, loginData.Username));
                //// Authenticate using the identity
                //var principal = new ClaimsPrincipal(identity);
                
                //await HttpContext.SignInAsync(JwtBearerDefaults.AuthenticationScheme, principal);

                return RedirectToPage("loginsuccess");
            }
            else
            {
                ModelState.AddModelError("", "username or password is blank");
                return Page();
            }
        }

        //public class LoginData
        //{
        //    [Required]
        //    public string Username { get; set; }

        //    [Required, DataType(DataType.Password)]
        //    public string Password { get; set; }

        //    public bool RememberMe { get; set; }
        //}
    }
}