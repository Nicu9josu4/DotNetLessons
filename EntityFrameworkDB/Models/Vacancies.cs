using System.ComponentModel.DataAnnotations;

namespace EntityFrameworkDB.Models
{
    public class Vacancy
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }

    }
}
