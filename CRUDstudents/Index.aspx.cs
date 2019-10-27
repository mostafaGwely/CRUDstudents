using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CRUDstudents
{
    public partial class Index : System.Web.UI.Page
    {
        public StudentsWebService Client = new StudentsWebService();
        protected void Page_Load(object sender, EventArgs e)
        {
                Label1.Text = getTable(new string[] { "Id", "Name", "Gender", "TotalMarks" });
        }

        private string getTable(string[] Columns)
        {
            var header = "";
            foreach (var columnName in Columns)
            {
                header += string.Format("<th scope='col'>{0}</th>", columnName);
            }

            var rows = "";
            foreach (var student in Client.GetStudents())
            {
                rows += "<tr>";
                rows += string.Format("<th scope='row'>{0}</th>", student.Id);
                rows += string.Format("<td>{0}</td>", student.Name);
                rows += string.Format("<td>{0}</td>", student.Gender);
                rows += string.Format("<td>{0}</td>", student.TotalMarks);

                rows += string.Format("<td>{0}</td>",
                   string.Format(@"<div class=""form-check""  >
                                        <input type=""checkbox"" class=""form-check-input checkbox"" id=""checkboxForId{0}"" name=""checkBox"">
                                          <label class=""form-check-label"" for=""checkboxForId{0}"" >Edit</label>
                                    </div>", student.Id));


                //rows += string.Format(
                //    @"<asp:CheckBox ID=""{0}"" runat=""server"" OnCheckedChanged=""CheckBox1_CheckedChanged"" />",
                //    student.Id);

                rows += "</tr>";
            }


            var old = File.ReadAllText(
                @"c:\users\mostafa\documents\visual studio 2015\Projects\CRUDstudents\CRUDstudents\templets.txt");

            old = old.Replace("{$header}", header);
            old = old.Replace("{$rows}", rows);

            return old;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("test.aspx");
        }
    }
}