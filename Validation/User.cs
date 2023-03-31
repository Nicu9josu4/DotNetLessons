using System.ComponentModel.DataAnnotations;

namespace DotNetLessons.Classes
{
    public class User
    {
        [Required(ErrorMessage = "Trebuie sa introduci un nume")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name is not valid")]
        public string Name { get; set; }

        [Range(1, 100)]
        public int Age { get; set; }

        [RegularExpression(@"^\+[1-9]\d{3}-\d{3}-\d{4}$")]
        public string Phone { get; set; }

        public User(string name, int age, string phone)
        {
            Name = name;
            Age = age;
            Phone = phone;
        }
    }
}