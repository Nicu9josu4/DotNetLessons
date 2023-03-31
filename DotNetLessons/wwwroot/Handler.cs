using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Web;
using System.Xml.Linq;
using Microsoft.AspNetCore.Http;

namespace DotNetLessons
{
    public interface IHttpHandler
    {
        // true if the System.Web.IHttpHandler instance is reusable; otherwise, false.
        bool IsReusable { get; }
        void ProcessRequest(HttpContext context);
    }
    [HttpPost]
    public class Handler : IHttpHandler
    {
        private object _response;
        private string _json;
        private string _fileName;
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["orcldb"].ConnectionString;

        public class Vacancies
        {
            public int ID { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public string StartDate { get; set; }
            public string EndDate { get; set; }
        }
        public class Hiring
        {
            public int ID { get; set; }
            public string Candidat { get; set; }
            public string Users { get; set; }
            public string Status { get; set; }
            public string Vacancy { get; set; }
            public string StatusDate { get; set; }
            public string Commentary { get; set; }
        }
        public class User
        {
            public int ID { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string RoleID { get; set; }
            public string StartDate { get; set; }
            public string EndDate { get; set; }
        }
        public class Dictionary
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public string Type { get; set; }
            public string Description { get; set; }
        }
        public class Employees
        {
            public int ID { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Phone { get; set; }
            public string Email { get; set; }
            public string Department { get; set; }
            public string Position { get; set; }
            public string Hiring { get; set; }
            public string StartDate { get; set; }
            public string EndDate { get; set; }
        }

        public void ProcessRequest(HttpContext context)
        {
            if (context.Request.Files.Count > 0)
            {
                HttpPostedFile postedFile = context.Request.Files[0];
                //Set the Folder Path.
                string folderPath = context.Server.MapPath("/Uploads/");
                //Set the File Name.
                _fileName = Path.GetFileName(postedFile.FileName);
                //Save the File in Folder.
                postedFile.SaveAs(folderPath + _fileName);
            }

            switch (context.Request["action"])
            {
                #region Work with vacancies

                case "showVacancies":
                    {
                        response = ShowVacancies();
                        break;
                    }
                case "getVacancyV":
                    {
                        string Title = context.Request["Title"] ?? string.Empty;
                        response = GetVacancyForPopUpWindow(Title);
                        break;
                    }
                case "SetNewVacancy":
                    {
                        string Title = context.Request["Title"] ?? string.Empty;
                        string Description = context.Request["Description"] ?? string.Empty;
                        response = SetNewVacancy(Title, Description);
                        break;
                    }
                case "EditVacancy":
                    {
                        string Title = context.Request["Title"] ?? string.Empty;
                        string Description = context.Request["Description"] ?? string.Empty;
                        int ID = Convert.ToInt32(context.Request["ID"] ?? string.Empty);
                        response = EditVacancy(ID, Title, Description);
                        break;
                    }
                case "EditVacancyMenu":
                    {
                        int ID = Convert.ToInt32(context.Request["ID"] ?? string.Empty);
                        response = GetVacancyData(ID);
                        break;
                    }
                case "DeleteVacancy":
                    {
                        int ID = Convert.ToInt32(context.Request["ID"] ?? string.Empty);
                        response = DeleteVacancy(ID);
                        break;
                    }

                #endregion Work with vacancies

                case "setCandidat":
                    {
                        string VacancyTitle = context.Request["VacancyTitle"] ?? string.Empty;
                        string Name = context.Request["Name"] ?? string.Empty;
                        string Surname = context.Request["Surname"] ?? string.Empty;
                        string Email = context.Request["Email"] ?? string.Empty;
                        string Phone = context.Request["Phone"] ?? string.Empty;
                        string FilePath = "/Uploads/" + _fileName;
                        //string LinkCV;// = context.Request["LinkCV"] ?? string.Empty;
                        //HttpPostedFile postedFile = context.Request.Files["LinkCV"];

                        //Send File details in a JSON Response.
                        //LinkCV = JsonConvert.SerializeObject(fileName, Formatting.Indented);
                        response = AddCandidat(VacancyTitle, Name, Surname, Email, Phone, FilePath);
                        break;
                    }
                case "GetComponents":
                    {
                        string target = context.Request["target"] ?? string.Empty;
                        response = ShowComponents(target);
                        break;
                    }
                case "checkUser":
                    {
                        string userName = context.Request["uname"] ?? string.Empty;
                        string userPass = context.Request["pname"] ?? string.Empty;
                        response = CheckUser(userName, userPass);
                        break;
                    }
            }
            context.Response.Write(response);
        }

        #region Work with vacancies

        public string DeleteVacancy(int ID)
        {
            using (OracleConnection conn = new OracleConnection(connectionString))
            {
                conn.Open();
                using (OracleCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Delete_Vacancy";
                    cmd.Parameters.Add("P_ID", OracleDbType.Int32).Value = ID;
                    cmd.ExecuteNonQuery();

                    return "Success";
                }
            }
        }
        public string GetVacancyData(int ID)
        {
            using (OracleConnection conn = new OracleConnection(connectionString))
            {
                conn.Open();
                using (OracleCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Edit_VacancyMenu";
                    cmd.Parameters.Add("Res", OracleDbType.Clob, ParameterDirection.ReturnValue);
                    cmd.Parameters.Add("P_ID", OracleDbType.Int32).Value = ID;
                    cmd.ExecuteNonQuery();

                    OracleClob myClob = (OracleClob)cmd.Parameters["Res"].Value;
                    var valuesFromClob = myClob.Value;
                    var dataFromClob = JObject.Parse(valuesFromClob);
                    _json = JsonConvert.SerializeObject(dataFromClob);
                    return _json;
                }
            }
        }
        public string EditVacancy(int ID, string VacancyTitle, string Description)
        {
            using (OracleConnection conn = new OracleConnection(connectionString))
            {
                conn.Open();
                using (OracleCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Edit_Vacancy";
                    cmd.Parameters.Add("P_ID", OracleDbType.Int32).Value = ID;
                    cmd.Parameters.Add("P_VacancyTitle", OracleDbType.Varchar2).Value = VacancyTitle;
                    cmd.Parameters.Add("P_Description", OracleDbType.Varchar2).Value = Description;
                    cmd.ExecuteScalar();
                    return "Success";
                }
            }
        }
        public string SetNewVacancy(string VacancyTitle, string Description)
        {
            using (OracleConnection conn = new OracleConnection(connectionString))
            {
                conn.Open();
                using (OracleCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Set_Vacancy";
                    cmd.Parameters.Add("P_VacancyTitle", OracleDbType.Varchar2).Value = VacancyTitle;
                    cmd.Parameters.Add("P_Description", OracleDbType.Varchar2).Value = Description;
                    cmd.ExecuteScalar();
                    return "Success";
                }
            }
        }
        public string ShowVacancies()
        {
            string cmdStr = "select * from Vacancies";
            List<Vacancies> VacancyList = new List<Vacancies>();
            using (OracleConnection conn = new OracleConnection(connectionString))
            {
                OracleCommand cmd = new OracleCommand(cmdStr, conn);
                conn.Open();
                OracleDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    Vacancies Vacancy = new Vacancies();
                    Vacancy.ID = Convert.ToInt32(read["id"]);
                    Vacancy.Title = read["Title"].ToString();
                    Vacancy.Description = read["Description"].ToString();
                    Vacancy.StartDate = Convert.ToDateTime(read["StartDate"]).ToString("dd.MM.yyyy");
                    Vacancy.EndDate = Convert.ToDateTime(read["EndDate"]).ToString("dd.MM.yyyy");
                    VacancyList.Add(Vacancy);
                }
                read.Close();
                string json = JsonConvert.SerializeObject(VacancyList);
                return (json);
            }
        }
        public string GetVacancyForPopUpWindow(string Title)
        {
            string cmdStr = "select distinct * from Vacancies where Title = '" + Title + "'";
            List<Vacancies> VacancyList = new List<Vacancies>();
            using (OracleConnection conn = new OracleConnection(connectionString))
            {
                OracleCommand cmd = new OracleCommand(cmdStr, conn);
                conn.Open();
                OracleDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    Vacancies Vacancy = new Vacancies();
                    Vacancy.Title = read["Title"].ToString();
                    Vacancy.Description = read["Description"].ToString();
                    VacancyList.Add(Vacancy);
                }
                read.Close();
                _json = JsonConvert.SerializeObject(VacancyList);
            }
            return (_json);
        }

        #endregion Work with vacancies

        public string AddCandidat(string VacancyTitle, string Name, string Surname, string Email, string Phone, string LinkCV)
        {
            using (OracleConnection conn = new OracleConnection(connectionString))
            {
                conn.Open();
                using (OracleCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Set_Candidat";
                    cmd.Parameters.Add("P_VacancyTitle", OracleDbType.Varchar2).Value = VacancyTitle;
                    cmd.Parameters.Add("P_Name", OracleDbType.Varchar2).Value = Name;
                    cmd.Parameters.Add("P_Surname", OracleDbType.Varchar2).Value = Surname;
                    cmd.Parameters.Add("P_Email", OracleDbType.Varchar2).Value = Email;
                    cmd.Parameters.Add("P_Phone", OracleDbType.Varchar2).Value = Phone;
                    cmd.Parameters.Add("P_LinkCV", OracleDbType.Varchar2).Value = LinkCV;
                    cmd.ExecuteScalar();
                    return ("Success");
                }
            }
        }
        public string ShowComponents(string target)
        {
            switch (target)
            {
                case "GetVacancy":
                    {
                        string cmdStr = "select * from Vacancies";
                        List<Vacancies> VacancyList = new List<Vacancies>();
                        using (OracleConnection conn = new OracleConnection(connectionString))
                        {
                            OracleCommand cmd = new OracleCommand(cmdStr, conn);
                            conn.Open();
                            OracleDataReader read = cmd.ExecuteReader();
                            while (read.Read())
                            {
                                Vacancies Vacancy = new Vacancies();
                                Vacancy.ID = Convert.ToInt32(read["id"]);
                                Vacancy.Title = read["Title"].ToString();
                                Vacancy.Description = read["Description"].ToString();
                                Vacancy.StartDate = Convert.ToDateTime(read["StartDate"]).ToString("dd.MM.yyyy");
                                Vacancy.EndDate = Convert.ToDateTime(read["EndDate"]).ToString("dd.MM.yyyy");
                                VacancyList.Add(Vacancy);
                            }
                            read.Close();
                            _json = JsonConvert.SerializeObject(VacancyList);
                        }
                        break;
                    }
                case "GetHiring":
                    {
                        string cmdStr = "SELECT h.id,c.first_name || ' ' || c.last_name AS candidat,u.first_name || ' ' || u.last_name AS USERS,d.name AS status,v.title AS vacancy,h.statusdate,h.comm from HIRING h LEFT JOIN candidat c ON h.candidat = c.id LEFT JOIN USERS u ON h.users = u.id LEFT JOIN DICTIONARY d ON h.status = d.id LEFT JOIN vacancies v ON h.vacancy = v.id ORDER BY h.id";
                        //string cmdStr = "select * from Hiring";
                        List<Hiring> HiringList = new List<Hiring>();
                        using (OracleConnection conn = new OracleConnection(connectionString))
                        {
                            OracleCommand cmd = new OracleCommand(cmdStr, conn);
                            conn.Open();
                            OracleDataReader read = cmd.ExecuteReader();
                            while (read.Read())
                            {
                                Hiring Hiring = new Hiring();
                                Hiring.ID = Convert.ToInt32(read["id"]);
                                Hiring.Candidat = read["Candidat"].ToString();
                                Hiring.Users = read["Users"].ToString();
                                Hiring.Status = read["Status"].ToString();
                                Hiring.Vacancy = read["Vacancy"].ToString();
                                Hiring.StatusDate = Convert.ToDateTime(read["StatusDate"]).ToString("dd.MM.yyyy");
                                Hiring.Commentary = read["COMM"].ToString();
                                HiringList.Add(Hiring);
                            }
                            read.Close();
                            _json = JsonConvert.SerializeObject(HiringList);
                        }
                        break;
                    }
                case "GetUsers":
                    {
                        //string enddate;
                        string cmdStr = "SELECT u.id, u.username, u.password, u.first_name, u.last_name, u.email, d.name AS RoleId, u.startdata, u.enddata FROM USERS u LEFT JOIN DICTIONARY d ON u.roleid = d.id ORDER BY u.id";
                        //string cmdStr = "select * from Users";
                        List<User> UserList = new List<User>();
                        using (OracleConnection conn = new OracleConnection(connectionString))
                        {
                            OracleCommand cmd = new OracleCommand(cmdStr, conn);
                            conn.Open();
                            OracleDataReader read = cmd.ExecuteReader();
                            while (read.Read())
                            {
                                User User = new User();
                                User.ID = Convert.ToInt32(read["id"]);
                                User.Username = read["Username"].ToString();
                                User.Password = read["Password"].ToString();
                                User.FirstName = read["First_Name"].ToString();
                                User.LastName = read["Last_Name"].ToString();
                                User.Email = read["Email"].ToString();
                                User.RoleID = read["RoleID"].ToString();
                                User.StartDate = Convert.ToDateTime(read["StartData"]).ToString("dd.MM.yyyy");
                                User.EndDate = read["EndData"].ToString();
                                UserList.Add(User);
                            }
                            read.Close();
                            _json = JsonConvert.SerializeObject(UserList);
                        }
                        break;
                    }
                case "GetEmployees":
                    {
                        string cmdStr = "select e.id,e.first_name,e.last_name,e.phone,e.email, dd.name AS Department, d.name AS position,  c.first_name || ' ' || c.last_name AS hiring ,e.startdate, e.enddate from EMPLOYEE e LEFT JOIN DICTIONARY d ON e.position = d.id LEFT JOIN DICTIONARY dd ON e.department = dd.id LEFT JOIN hiring h ON e.hiring = h.id LEFT JOIN candidat c ON c.id = h.candidat ORDER BY e.id";
                        //string cmdStr = "select * from Users";
                        List<Employees> EmployeesList = new List<Employees>();
                        using (OracleConnection conn = new OracleConnection(connectionString))
                        {
                            OracleCommand cmd = new OracleCommand(cmdStr, conn);
                            conn.Open();
                            OracleDataReader read = cmd.ExecuteReader();
                            while (read.Read())
                            {
                                Employees Employees = new Employees();
                                Employees.ID = Convert.ToInt32(read["id"]);
                                Employees.FirstName = read["First_Name"].ToString();
                                Employees.LastName = read["Last_Name"].ToString();
                                Employees.Phone = read["Phone"].ToString();
                                Employees.Email = read["Email"].ToString();
                                Employees.Department = read["Department"].ToString();
                                Employees.Position = read["Position"].ToString();
                                Employees.Hiring = read["Hiring"].ToString();
                                Employees.StartDate = Convert.ToDateTime(read["StartDate"]).ToString("dd.MM.yyyy");
                                Employees.EndDate = read["EndDate"].ToString();
                                EmployeesList.Add(Employees);
                            }
                            read.Close();
                            _json = JsonConvert.SerializeObject(EmployeesList);
                        }
                        break;
                    }
                case "GetDictionary":
                    {
                        string cmdStr = "select * from Dictionary";
                        List<Dictionary> DictionaryList = new List<Dictionary>();
                        using (OracleConnection conn = new OracleConnection(connectionString))
                        {
                            OracleCommand cmd = new OracleCommand(cmdStr, conn);
                            conn.Open();
                            OracleDataReader read = cmd.ExecuteReader();
                            while (read.Read())
                            {
                                Dictionary Dictionary = new Dictionary();
                                Dictionary.ID = Convert.ToInt32(read["id"]);
                                Dictionary.Name = read["Name"].ToString();
                                Dictionary.Type = read["Grupa"].ToString();
                                Dictionary.Description = read["Description"].ToString();
                                DictionaryList.Add(Dictionary);
                            }
                            read.Close();
                            _json = JsonConvert.SerializeObject(DictionaryList);
                        }
                        break;
                    }
            }
            return (_json);
        }

        public string CheckUser(string userName, string userPass)
        {
            string cmdStr = "select * from users where Username = '" + userName + "' and Password = '" + userPass + "'";
            List<User> UserList = new List<User>();
            using (OracleConnection conn = new OracleConnection(connectionString))
            {
                OracleCommand cmd = new OracleCommand(cmdStr, conn);
                conn.Open();
                OracleDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    User webUser = new User();
                    webUser.ID = Convert.ToInt32(read["ID"]);
                    webUser.Username = read["Username"].ToString();
                    webUser.Password = read["Password"].ToString();
                    webUser.RoleID = Convert.ToInt32(read["RoleID"]).ToString();
                    UserList.Add(webUser);
                }
                read.Close();
                string json = JsonConvert.SerializeObject(UserList);
                return (json);
            }
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}