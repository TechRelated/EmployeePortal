using System;
using System.Threading.Tasks;
using EmployeePortal.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace EmployeePortal.Pages
{
    //[CustomAuthorization]
    //[Authorize]
    public class PrivacyModel : PageModel
    {
        private readonly ILogger<PrivacyModel> _logger;

        public PrivacyModel(ILogger<PrivacyModel> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> OnGetAsync()
        {

            if(!User.Identity.IsAuthenticated)
                return RedirectToPage("index");

            return Page();
            /* if (!Request.Headers.ContainsKey("Authorization"))
                   return RedirectToPage("Index");

             string authorizationHeader = Request.Headers["Authorization"];
             var verifytoken = new VerifyToken();
             var currenttoken = verifytoken.ValidateCurrentToken(authorizationHeader);

             if (!currenttoken)
             {
                 return RedirectToPage("Index");
             } 
              return Page();*/

            //if (!Request.Headers.ContainsKey("Authorization"))
            //    return RedirectToPage("Index");

            //string authorizationHeader = Request.Headers["Authorization"];
            //var validatedToken = await VerifyToken.ValidateToken_new(authorizationHeader);

            //if (validatedToken.ValidTo < DateTime.UtcNow)
            //{
            //    Console.WriteLine("Invalid token");
            //    HttpContext.Session.Remove("JWToken");
            //    return RedirectToPage("Index");
            //}
            //return Page(); 
        }

    }
}
