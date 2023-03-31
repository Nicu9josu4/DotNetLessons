namespace DotNetLessons.Services.MapRoutes
{
    public class SecretCodeConstraint : IRouteConstraint
    {
        private string _secretCode;    // допустимый код

        public SecretCodeConstraint(string secretCode)
        {
            _secretCode = secretCode;
        }

        public bool Match(HttpContext? httpContext, IRouter? route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            return values[routeKey]?.ToString() == _secretCode;
        }
    }
}