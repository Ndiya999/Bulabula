using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.SessionState;
using System.Text;


namespace BulabulaApp
{
    public partial class About : System.Web.UI.Page
    {
        public string allgroups = null;
        public Member aMember = new Member();
        public string FriendsOnlineString = null;


        protected void Page_Load(object sender, EventArgs e)
        {
            Literal title = (Literal)Master.FindControl("pageTitle");
            title.Text = "Compose Message";
          
            //string name = Page.RouteData.Values["Location"] as string;

           
          


        }

       
    }
}
