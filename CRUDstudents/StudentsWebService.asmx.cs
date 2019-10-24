using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Configuration;
using System.Web;
using System.Web.Services;

namespace CRUDstudents
{
    /// <summary>
    /// Summary description for StudentsWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
    // [System.Web.Script.Services.ScriptService]
    public class StudentsWebService : System.Web.Services.WebService
    {
        public string conString =
            @"Data Source=DESKTOP-7KF1QC4\SEMITE;Initial Catalog=werbServices;Persist Security Info=True;User ID=sa;Password=hello123";

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public List<Student> GetStudents()
        {
            var Students = new List<Student>();
            string cs = conString;
            using (var con = new SqlConnection(cs))
            {
                var cmd = new SqlCommand("select * from tblStudents", con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var student = new Student();
                    student.Id = Convert.ToInt32(reader["ID"]);
                    student.Name = reader["Name"].ToString();
                    student.Gender= reader["Gender"].ToString();
                    student.TotalMarks=Convert.ToInt32(reader["TotalMarks"]);

                    Students.Add(student);
                }

            }
            return Students;
        }

        [WebMethod]
        public List<Student> DelStudents(string Ids)
        {
            var idList = Ids.Split(',').Select(Int32.Parse).ToList();


            var Students = new List<Student>();
            using (var con = new SqlConnection(conString))
            {
                var cmd = new SqlCommand();
                var sql = "SELECT * FROM tblStudents WHERE ID IN ({0})";

                //var idList = new int[] { 2,3,4};
                var idParameterList = new List<string>();
                var index = 0;
                foreach (var id in idList)
                {
                    var paramName = "@idParam" + index;
                    cmd.Parameters.AddWithValue(paramName, id);
                    idParameterList.Add(paramName);
                    index++;
                }

                cmd.CommandText = String.Format(sql, string.Join(",", idParameterList));
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                con.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var student = new Student();
                    student.Id = Convert.ToInt32(reader["ID"]);
                    student.Name = reader["Name"].ToString();
                    student.Gender = reader["Gender"].ToString();
                    student.TotalMarks = Convert.ToInt32(reader["TotalMarks"]);

                    Students.Add(student);
                }

            }
            return Students;

        }

    }
}
