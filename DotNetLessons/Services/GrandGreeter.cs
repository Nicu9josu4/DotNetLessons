using System.Globalization;

namespace DotNetLessons.Services
{
    public class GrandGreeter : IGreeter
    {
        public string Greet(string name)
        {
            switch (CultureInfo.CurrentCulture.ToString())
            {
                case "ru-RU":
                    return "Привет " + name + ", ты мой друг";

                case "ro-MD":
                    return "Salut " + name + ", tu esti prietenul meu";

                default:
                    return "Hello " + name + ", you're my friend";
            }
        }
    }
}