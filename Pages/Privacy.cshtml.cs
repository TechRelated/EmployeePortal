using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EmployeePortal.Helper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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

        public IActionResult OnGet()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
                  return RedirectToPage("Index");

            string authorizationHeader = Request.Headers["Authorization"];
            var verifytoken = new VerifyToken();
            var currenttoken = verifytoken.ValidateCurrentToken(authorizationHeader);

            if (!currenttoken)
            {
                return RedirectToPage("Index");
            } 
             return Page();
        }
    }
}
