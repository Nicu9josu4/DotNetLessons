using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
