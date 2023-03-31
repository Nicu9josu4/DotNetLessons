namespace DotNetLessons.Services
{
    public class GreeterWithPropertyService
    {
        public IGreeter? Greeter { get; set; }

        public void GreetUser(string name)
        {
            Console.WriteLine(Greeter?.Greet(name));
        }
    }
}