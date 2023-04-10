namespace DotNetLessons
{
    public class WorkWithExceptions
    {
        internal static void BuildException(WebApplicationBuilder? builder)
        {

        }
        internal static void ApplicationException(WebApplication app)
        {
            //app.Environment.EnvironmentName = "Development";

            /// Using exception handling
            //app.UseDeveloperExceptionPage(); // Catch an exception when Environment is Development
            app.UseStatusCodePages(); // Utilizarea paginilor pentru informarea utilizatorului despre status code si ce reprezinta ele.
                                      // Regula principala este ca ea sa fie amplasata inaintea la UseStaticFiles sau a oricarui Map
                                      //app.UseStatusCodePagesWithRedirects("/error/{0}"); // Va transmite la pagina aratata dar cu status code deja gasit
                                      //app.UseStatusCodePagesWithReExecute("/error/{0}"); // Va transmite la pagina cu exceptia dar cu status codul exceptiei

            if (!app.Environment.IsDevelopment())
            {
                //app.UseExceptionHandler(app => app.Run(async context =>
                //{
                //    context.Response.StatusCode = 500;
                //    await context.Response.WriteAsync("Error 500. DivideByZeroException occured!");
                //}));
                app.UseExceptionHandler(app => app.Run(async context =>
                {
                    await context.Response.WriteAsync(" ");
                }));
            }
            app.Map("/error/{statusCode}", (int statusCode) => $"Error. Status Code: {statusCode}");

            /// Use Exception handler methods
            app.Map("/getException", async context =>
            {
                int a = 10;
                int b = 0;
                await context.Response.WriteAsync($"{a / b}");
            });
        }
    }
}
