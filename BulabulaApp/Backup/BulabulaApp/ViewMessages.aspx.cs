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
    public partial class WebForm4 : System.Web.UI.Page
    {
        MessagesDAL test = new MessagesDAL();
        public string allgroups = null;
        public Member aMember = new Member();
        public string MessageString = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            Literal title = (Literal)Master.FindControl("pageTitle");
            title.Text = "Messages Single View";


            if (!IsPostBack)
            {

                string date = "";

                if (!String.IsNullOrEmpty(Session["selectedMessageID"].ToString()))
                {


                    int id = int.Parse(Session["selectedMessageID"].ToString());
                    // message.MessageId = id;
                    StringBuilder concatinater = new StringBuilder();
                    Message message = new Message(id);
                    DataTable aMessage = test.GetAMessage(message);

                    NotificationDAL notificationDAL = new NotificationDAL();

                    notificationDAL.UpdateMessageNotificationIsRead(message);

                    foreach (DataRow row in aMessage.Rows)
                    {
                        Reusable_Methods reusable_Methods = new Reusable_Methods();
                        date = reusable_Methods.FormatDateTime(DateTime.Parse((row["DateTime"]).ToString()));
                        //date = Convert.ToDateTime(row["DateTime"]).ToString("M");

                        //Building single message view box
                        concatinater.Append("<div class='SingleViewmsgeWrapper' id='");
                        concatinater.Append(row["messageID"].ToString());

                        //concatinater.Append("' ><div class='msgeHeading ui-corner-all' ><h2 class='ui-corner-all'> <a class='btnMemberProfile' id='");
                        concatinater.Append("' ><div class='msgeHeading ui-corner-all' ><h2 class='ui-corner-all'> <a href='#' class='btnMemberProfile replyMsge' style='font-weight:normal; font-size: 1.2em; ' id='");
                        concatinater.Append(row["FriendID"].ToString());
                        concatinater.Append("' >");
                        concatinater.Append(row["Friend"].ToString());
                        concatinater.Append("</a><span class='DateWithTime ui-corner-all floatright' style='font-size: 1.2em;' >  ");
                        concatinater.Append(date);
                        concatinater.Append("</span></h2> </div>");
                        concatinater.Append("<p class=''>");
                        concatinater.Append(row["MessageText"].ToString());
                        concatinater.Append("</p></div>");

                    }

                    MessageString = concatinater.ToString();

                }
            }
        }
    }
}