using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeePortal.Helper
{
    public class SessionTimeoutAttribute : ActionFilterAttribute
    {
        //public override void OnActionExecuting(ActionExecutingContext filterContext)
        //{
        //    var correlationId = HttpContext.Request.Headers["x-correlation-id"].ToString();
        //    if (HttpContext.Session["ID"] == null)
        //    {
        //        filterContext.Result = new RedirectResult("~/Index");
        //        return;
        //    }
        //    base.OnActionExecuting(filterContext);
        //}
    }
}
