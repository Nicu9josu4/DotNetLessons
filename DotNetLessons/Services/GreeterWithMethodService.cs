namespace DotNetLessons.Services
{
    public class GreeterWithMethodService
    {
        public void GreetUser(IGreeter greeter, string name)
        {
            Console.WriteLine(greeter.Greet(name));
        }
    }
}