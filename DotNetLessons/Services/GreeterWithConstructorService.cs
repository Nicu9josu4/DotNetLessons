namespace DotNetLessons.Services
{
    public class GreeterWithConstructorService
    {
        private IGreeter _greeter;

        public GreeterWithConstructorService(IGreeter greeter)
        {
            _greeter = greeter;
        }

        public void GreetUser(string name)
        {
            Console.WriteLine(_greeter.Greet(name));
        }
    }
}