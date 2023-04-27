using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFrameworkDB.Models
{
    public class Users
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Column("USERNAME")]
        public string? Username { get; set; }

        [Column("PASSWORD")]
        public string? Password { get; set; }

        [Column("FIRST_NAME")]
        public string? FirstName { get; set; }

        [Column("LAST_NAME")]
        public string? LastName { get; set; }

        [Column("EMAIL")]
        public string? Email { get; set; }

        [Column("ROLEID")]
        public int RoleId { get; set; }

        [Column("STARTDATA")]
        public DateTime StartDate { get; set; }

        [Column("ENDDATA")]
        public DateTime EndDate { get; set; }
    }
}