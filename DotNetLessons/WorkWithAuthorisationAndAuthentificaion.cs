using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DotNetLessons
{
    public class WorkWithAuthorisationAndAuthentificaion
    {
        internal static void BuildAuthorisationAndAuthentification(WebApplicationBuilder builder)
        {
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminRole", policy => policy.RequireRole("Admin"));
            });
            builder.Services.AddControllersWithViews();

            /// Adaugarea autentificatorului pe baza de cookie
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.Cookie.Name = "Authentication cookie";
                    options.LoginPath = "/Account/login";
                    options.LogoutPath = "/Account/logout";
                });

            /// Adaugarea autentificatorului pe baza de token
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
             .AddJwtBearer(options =>
             {
                 options.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuer = true,
                     ValidateAudience = true,
                     ValidateLifetime = true,
                     ValidateIssuerSigningKey = true,
                     ValidIssuer = AuthOptions.ISSUER,
                     ValidAudience = AuthOptions.AUDIENCE,
                     IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey()
                 };
             });
        }

        internal static void ApplicationAuthorization(WebApplication app)
        {
            app.UseAuthorization();
        }

        internal static void ApplicationAuthentication(WebApplication app)
        {
            app.UseAuthentication();

            app.Map("/Account/login", async context =>
            {
                context.Response.ContentType = "text/html";
                await context.Response.SendFileAsync(@"wwwroot/login.html");
            });
            app.Map("/login/{username}", (string username) =>
            {
                var claims = new List<Claim> { new Claim(ClaimTypes.Name, username) };
                var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    claims: claims,
                    expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

                return new JwtSecurityTokenHandler().WriteToken(jwt);
            });
            app.Map("/Accounts", [Authorize(Policy = "AdminRole")] () => new { Message = "Hello" });
        }
    }

    public class AuthOptions
    {
        public const string ISSUER = "MyAuthServer";  // издатель токена
        public const string AUDIENCE = "MyAuthClient";  // потребитель токена
        private const string KEY = "mysupersecret_secretkey!123";  // ключ для шифрации

        public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
    }
}