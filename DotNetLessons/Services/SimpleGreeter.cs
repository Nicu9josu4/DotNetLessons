using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetLessons.Services
{
    public class SimpleGreeter : IGreeter
    {
        public string Greet(string name) => "Hello " + name;
    }
}
