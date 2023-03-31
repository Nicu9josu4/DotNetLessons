namespace DotNetLessons.Services
{
    public class SimpleGreeter : IGreeter
    {
        public string Greet(string name) => "Hello " + name;
    }
}