namespace EntityFrameworkDB.Models
{
    public class Users
    {
        public int ID { get; set; }
        public string USERNAME { get; set; }
        public string PASSWORD { get; set; }
        public string FIRST_NAME { get; set; }
        public string LAST_NAME { get; set; }
        public string EMAIL { get; set; }
        public int ROLEID { get; set; }
        public DateTime STARTDATA { get; set; }
        public DateTime ENDDATA { get; set; }
    }
}
