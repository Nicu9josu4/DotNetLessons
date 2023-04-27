namespace DotNetLessons
{
    public class WorkWithResultsAPI
    {
        internal static void ApplicationResult(WebApplication app)
        {
            /// Using a ResultsAPI
            app.Map("/result/text", () => Results.Text("Results Text"));
            app.Map("/result/content", () => Results.Content("Text", "text/plain", System.Text.Encoding.Unicode)); // Sau fara ultimii 2 parametri
            app.Map("/result/json/tom", () => Results.Json(new { Name = "Tom", Password = "Password" }));
            app.Map("/result/error", () => Results.Json(new { Message = "Unexpected error" }, statusCode: 500));
            app.Map("/result/NotFound", () => Results.NotFound("Error 404. Invalid address"));
            app.Map("/result/Unauthorized", () => Results.Unauthorized());
            app.Map("/result/Ok", () => Results.Ok("This is Success"));
            app.Map("/result/Ok", () => Results.Forbid());

            app.Map("/result/oldsite", () => Results.LocalRedirect("/result/newsite")); // Transmiterea userului pe o adresa locala
            //app.Map("/result/oldsite", () => Results.LocalRedirect("https://metanit.com/sharp/aspnet6/10.3.php")); // Error InvalidOperationException: The supplied URL is not local.
            app.Map("/result/newsite", () => "New Site");

            app.Map("/result/newAddress", () => Results.Redirect("https://metanit.com/sharp/aspnet6/10.3.php"));

            app.Map("/result/badRequest/{age:int}", (int age) =>
            {
                if (age < 18)
                    return Results.BadRequest(new { message = "Invalid Age" });
                else
                    return Results.Content("Access is available");
            });
        }
    }
}