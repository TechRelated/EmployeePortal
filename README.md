At the moment, its very simple structure and would improve the as I move ahead.
Please suggest the best practices, if you can.

Goal is
- to use industry best practice to write scalable, efficient web applicaton
- Should be able to publish to Azure

Architecture I am using is as below :

ASP.NET Core C#
----------------
Web API (Repository Pattern) - Dapper for stored procs and may be EF for simple CRUD operations

1. Integrate swagger to API
2. Add Global Exception handling

Razor Pages (Later use AdminLTE templates for UI)
SQL Server (Backend)

Login Module
  1. Login page with JWT token based authentication.

Completed
  1. Login page with basic validation
  2. Retrieve Token from Web API for valid user (at the moment, user details are hardcoded)
  3. Redirect user to login if token expires (not sure, what's the best approach)

To-Do
    1. Save the Jwt token for subsequent HtttpRequest and should redirect user to login page
    (at the moment, saving the token in httpsession and saving in http header in startup class of front end application)
    2. Saving the jwt token related settings, front end application and web api, don't think its best approach
    3. Log off button for logged in user
    4. Secure pages with Authorize attribute if not allowed as per permissions and role
    5. Dynamic menu based on user's role and permissions
    6. Error page redirection from the pages and recoding errors
    7. Encrypt password and decrypt while login
