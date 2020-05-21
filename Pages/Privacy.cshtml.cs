using System;
using System.Threading.Tasks;
using EmployeePortal.Helper;
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

            if (!Request.Headers.ContainsKey("Authorization"))
                return RedirectToPage("Index");

            string authorizationHeader = Request.Headers["Authorization"];
            var verifytoken = new VerifyToken();

            var validatedToken = await verifytoken.ValidateToken_new(authorizationHeader);

            if (validatedToken.ValidTo < DateTime.UtcNow)
            {
                Console.WriteLine("Invalid token");
                return RedirectToPage("Index");
            }
            return Page(); 
        }

    }
}
