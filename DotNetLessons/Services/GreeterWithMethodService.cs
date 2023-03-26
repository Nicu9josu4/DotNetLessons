using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
