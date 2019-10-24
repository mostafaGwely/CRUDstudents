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
        public  StudentsWebService Client = new StudentsWebService();
        protected void Page_Load(object sender, EventArgs e)
        {
            Label1.Text = getTable();
        }

        private string getTable()
        {
            return File.ReadAllText(@"c:\users\mostafa\documents\visual studio 2015\Projects\CRUDstudents\CRUDstudents\templets.txt");
        }
    }
}