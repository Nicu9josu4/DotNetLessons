using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFrameworkDB.Models
{
    public class Vacancy
    {
        [ForeignKey("UserId")]
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string TestColoumn { get; set; }
        public User User { get; set; }
    }
}