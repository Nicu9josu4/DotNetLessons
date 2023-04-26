using System.ComponentModel.DataAnnotations;

namespace EntityFrameworkDB.Models
{
    public class User
    {
        [Key]
        public int idUser { get; set; }
        public string Token { get; set; }
    }
}
