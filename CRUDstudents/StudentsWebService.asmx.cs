using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
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
    [System.Web.Script.Services.ScriptService]
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
        public void DelStudents(string Ids)
        {
            var idList = Ids.Split(',').Select(Int32.Parse).ToList();

            using (var con = new SqlConnection(conString))
            {
                var cmd = new SqlCommand();
                var sql = "DELETE FROM tblStudents WHERE ID IN ({0})";
                cmd.CommandText = sql;

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
                cmd.ExecuteReader();
            }

        }

        [WebMethod]
        public void AddStudent(Student student)
        {
            using (var con = new SqlConnection(conString))
            {
                var cmd = new SqlCommand();
                cmd.CommandText = string.Format("insert into tblStudents values('{0}', '{1}', '{2}')", student.Name, student.Gender, student.TotalMarks);
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;

                con.Open();
                cmd.ExecuteReader();
            }
        }

        [WebMethod]
        public void UpdateStudent(Student student, string ID)
        {
            using (var con = new SqlConnection(conString))
            {
                var cmd = new SqlCommand();
                cmd.CommandText = string.Format("UPDATE tblStudents SET Name = '{0}', Gender= '{1}', TotalMarks= {2} WHERE ID = {3}",
                    student.Name, student.Gender, student.TotalMarks, Convert.ToInt32(ID));
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;

                con.Open();
                cmd.ExecuteReader();
            }
        }


        [WebMethod]
        public Student GetStudentById(string Id)
        {
            string cs = conString;
            var student = new Student();
            using (var con = new SqlConnection(cs))
            {
                var cmd = new SqlCommand(string.Format("select * from tblStudents where ID = {0}", Id), con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    student.Id = Convert.ToInt32(reader["ID"]);
                    student.Name = reader["Name"].ToString();
                    student.Gender = reader["Gender"].ToString();
                    student.TotalMarks = Convert.ToInt32(reader["TotalMarks"]);
                }

            }
            return student;
        }

        [WebMethod]
        public void propagateStudents()
        {
            var rnd = new Random();

            for (int i = 0; i < 10; i++)
            {
                AddStudent(new Student
                {
                    Name = ((Path.GetRandomFileName()).Replace(".", "")).Substring(0, 8),
                    Gender = (new string[] { "Male", "Female" })[rnd.Next(0,2)],
                    TotalMarks = rnd.Next(0,1000)
                });
            }
        }

        [WebMethod]
        public void purgeStudents()
        {
            string cs = conString;
            using (var con = new SqlConnection(cs))
            {
                var cmd = new SqlCommand("delete tblStudents", con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                var reader = cmd.ExecuteReader();
            }
        }

    }
}
