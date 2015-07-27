using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Collections;
using System.Drawing;
using System.Text;

namespace BulabulaApp
{
    public partial class Home : System.Web.UI.Page
    {
        private Post aPost;
        ArrayList ArrayPosts;
        ArrayList ArrayMembers;
        int countMembers;
        public string newsfeed = null;
        public string allgroups = null;
        public StringBuilder concatinater = new StringBuilder();
        public Member aMember = new Member();
        LoginDAL loginDAL = new LoginDAL();
        Member_Status member_Status;
        public string FriendsOnlineString = null;
        public string NotificationsString = null;
        Reusable_Methods reusable_Methods = new Reusable_Methods();

        protected void Page_Load(object sender, EventArgs e)
        {
            Literal title = (Literal)Master.FindControl("pageTitle");
            title.Text = "Home";


            MessagesDAL member = new MessagesDAL();

            if (Session["memberID"] == null)
            {

                Response.Redirect("~/Login.aspx");

            }
            else if (loginDAL.GetMemberStatus(member_Status = new Member_Status(Session["memberID"].ToString())))
            {
                if (Request.Cookies["login"] != null)
                {
                    var c = new HttpCookie("login");
                    c.Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies.Add(c);
                    Session.Abandon();
                }

                Response.Redirect("~/Login.aspx");
            }
            else
            {

                if (!IsPostBack)
                {

                    #region NEWS FEED

                    string TabName = "Posts";

                    BulabulaApp.WebServices.postsWebservice posts = new WebServices.postsWebservice();

                    newsfeed = posts.GetTabContents(TabName, -1, -1);
                    #endregion


                }//END MAIN if
                else//ELSE THIS IS A POSTBACK
                {


                    if (ThisFileToDownload.Value.ToString() != "")
                    {

                        // Get the file from the database
                        PostDAL postDAL = new PostDAL();

                        DataTable file = postDAL.GetAFile(int.Parse(ThisFileToDownload.Value));
                        ThisFileToDownload.Value = "";
                        DataRow row = file.Rows[0];

                        string name = (string)row["FileName"];
                        string contentType = (string)row["FileCaption"];
                        Byte[] data = (Byte[])row["File"];

                        // Send the file to the browser
                        Response.AddHeader("Content-type", contentType);
                        Response.AddHeader("Content-Disposition", "attachment; filename=" + name);
                        Response.BinaryWrite(data);
                        Response.Flush();
                        Response.End();


                        //Refreshing file id hidden control


                    }

                }//END ELSE
            }





        }//END PAGE_LOAD


    }
}