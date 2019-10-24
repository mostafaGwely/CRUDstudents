using System;
using System.Collections.Generic;
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
        }


    }
}