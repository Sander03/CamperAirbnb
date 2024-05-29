using CamperAirbnb.Database.UserDatabase;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    private readonly IUserContext _context;


    public BasicAuthenticationHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock,
        IUserContext context
    ) : base(options, logger, encoder, clock)
    {
        _context = context;

    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {

        Response.Headers.Add("WWW-Authenticate", "Basic");

        if (!Request.Headers.ContainsKey("Authorization"))
        {
            return Task.FromResult(AuthenticateResult.Fail("Authorization header missing."));
        }

        var authorizationHeader = Request.Headers["Authorization"].ToString();
        var authHeaderRegex = new Regex(@"Basic (.*)");

        if (!authHeaderRegex.IsMatch(authorizationHeader))
        {
            return Task.FromResult(AuthenticateResult.Fail("Authorization code not formatted properly."));
        }

        var authBase64 = Encoding.UTF8.GetString(Convert.FromBase64String(authHeaderRegex.Replace(authorizationHeader, "$1")));
        var authSplit = authBase64.Split(Convert.ToChar(":"), 2);
        if (authSplit.Length <= 1)
        {
            return Task.FromResult(AuthenticateResult.Fail("Authorization code not formatted properly."));
        }
        var authUsername = authSplit[0];
        var authPassword = authSplit.Length > 1 ? authSplit[1] : throw new Exception("Unable to get password");
        var user = _context.GetAll().FirstOrDefault(u => u.Email == authUsername && u.Password == authPassword);

        if (user is null)
        {
            return Task.FromResult(AuthenticateResult.Fail("The username or password is not correct."));
        }

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, authUsername),
            new Claim(ClaimTypes.Role, "admin"),
            new Claim(ClaimTypes.Sid, user.Id.ToString())
        };

        var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims, "Custom"));

        return Task.FromResult(AuthenticateResult.Success(new AuthenticationTicket(claimsPrincipal, Scheme.Name)));
    }
}
