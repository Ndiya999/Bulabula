using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

namespace BulabulaApp
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        public string FriendsOnlineString = null;
        public string allgroups = null;
        public Member aMember = new Member();
        Reusable_Methods reusable_Methods;

        public string items = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            Literal title = (Literal)Master.FindControl("pageTitle");
            title.Text = "Inbox";
            Page.RouteData.Values["Id"] = "Inbox";


            if (!IsPostBack)
            {
                if (Session["memberID"] == null)
                {

                    Response.Redirect("~/Login.aspx");

                }

                else
                {
                    #region ##
                    if (IsPostBack)
                    {
                        if (Request["__EVENTARGUMENT"] != "")
                        {
                            try
                            {
                                string s = Request["__EVENTARGUMENT"];

                                Session["Data"] = s.TrimEnd(',');

                                Response.Redirect("~/ViewMessages.aspx");
                            }
                            catch (Exception ex)
                            {
                                ex.ToString();
                            }
                        }
                    }
                    #endregion




                }
            }



            MessagesDAL DAL = new MessagesDAL();
            string date = "";
            string shrtMessage = "";
            DataTable inboxList = DAL.getInboxList(new Member(Session["memberID"].ToString()));

            StringBuilder concatinater = new StringBuilder();



            foreach (DataRow row in inboxList.Rows)
            {
                Message message = new Message(int.Parse(row["messageID"].ToString()));
                
                
                //date = Convert.ToDateTime(row["DateTime"]).ToString("D");

                reusable_Methods = new Reusable_Methods();

                date = reusable_Methods.FormatDateTime(DateTime.Parse(row["DateTime"].ToString()));


                if (row["MessageText"].ToString().Length > 45)
                {
                    shrtMessage = row["MessageText"].ToString().Substring(0, 45) + "...";

                }
                else
                {
                    shrtMessage = row["MessageText"].ToString();

                }

                //Building single message view box
                concatinater.Append("<div class='msgeWrapper ");

                if(!DAL.IsMessageRead(message))
                {
                //isRead
                    concatinater.Append("isNotRead");
                }




                concatinater.Append("' id='");
                concatinater.Append(row["messageID"].ToString());

                concatinater.Append("'> <div class='checkBox'><input type='checkbox'  /> </div><div class='msge' ><div><span class='sendingFriend blueFontTextColor' id='");
                concatinater.Append(row["FriendID"].ToString());
                concatinater.Append("' >");
                concatinater.Append(row["FriendDisplayName"].ToString());
                concatinater.Append("</a><span class='DateWithTime floatright'>");
                concatinater.Append(date);
                concatinater.Append("</span></div><div class='slideInMenu floatright' style='visibility:hidden;' ><span class='floatright BorberRad3' id='deleteMsge' >Delete</span>");
                concatinater.Append(" <span class='floatright BorberRad3' BorberRad3'>Reply</span></div><div class='msgeText floatright'>");
                concatinater.Append(shrtMessage);
                concatinater.Append("</div></div> <div style='clear:both;'></div></div>");


            }

            items = concatinater.ToString();



        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Write("Welcome to  Student Academic Blog");

        }

    }
}