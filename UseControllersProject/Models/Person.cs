using System.ComponentModel.DataAnnotations;

namespace UseControllersProject.Models
{
    public class Person
    {
        [Required]
        public int Id { get; set; }

        public string? Name { get; set; }
        public int Age { get; set; }

        public Person()
        {
        }

        public Person(string name, int age)
        {
            Random rnd = new Random();
            Id = rnd.Next(0, 10000);
            Name = name;
            Age = age;
        }

        public Person(int id, string name, int age)
        {
            Id = id;
            Name = name;
            Age = age;
        }

        public string PrintInfo()
        {
            return $"{Id}. {Name} ({Age})";
        }
    }
}