using DotNetLessons.Classes;
using System.ComponentModel.DataAnnotations;

namespace Validation
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            TestGenerator.TestGen();

            //CreateUser("Tom", 37, "+1111-111-2345");
            //CreateUser("b", -4, "+11111112345");
            //CreateUser("", 130, "+0111-111-2345");
            //CreateUser("Bob", 18, "+1111-111-2345");
            //CreateUser("", 130, "+0111-111-2345");
        }

        private static void CreateUser(string name, int age, string phone)
        {
            User user = new(name, age, phone);
            var context = new ValidationContext(user);
            var results = new List<ValidationResult>();
            if (!Validator.TryValidateObject(user, context, results, true))
            {
                Console.WriteLine("Can't create this User");
                foreach (var error in results)
                {
                    Console.WriteLine(error.ErrorMessage);
                }
                Console.WriteLine();
            }
            else
                Console.WriteLine($"Object has been created. Name: {user.Name}\n PhoneNumber: {user.Phone}\n");
        }
    }
}