using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Security.Cryptography;
using System.Web.Script.Serialization;

namespace BulabulaApp.Ajax_Processors
{
    public partial class ProcessData : System.Web.UI.Page
    {
        MessagesDAL test = new MessagesDAL();
    //  Messages message;
        protected void Page_Load(object sender, EventArgs e)
        {



            #region Get the Next/Previous Message
            //if (!String.IsNullOrEmpty(Request.Form["msgeID"].ToString()) && !String.IsNullOrEmpty(Request.Form["con"].ToString()))
            //{
            //    StringBuilder concatinater = new StringBuilder();
            //    string date = "";
            //    string con = Request.Form["con"].ToString();

            //    int i = int.Parse(Request.Form["msgeID"].ToString());
            //    { 

            //    if(i == 272)
            //        Response.Write("<div class='LoginUserValidationSummary BorberRad3'>Error. Your message was not sent.</div>");

            //    }
            //    //   message.MessageId = id;
            //    Messages message = new Messages();
            //    message.MessageId = int.Parse(Request.Form["msgeID"].ToString());

            //    DataTable aMessage = test.GetNxtPreMessage(message, con);

            //    foreach (DataRow row in aMessage.Rows)
            //    {
            //        date = Convert.ToDateTime(row["DateTime"]).ToString("D");

            //        //Building single message view box
            //        concatinater.Append("<div class='SingleViewmsgeWrapper' id='");
            //        concatinater.Append(row["messageID"].ToString());
            //        concatinater.Append("'><div class='msgeHeading' ><h2> <a href='#' class=' '>");
            //        concatinater.Append(row["Friend"].ToString());
            //        concatinater.Append("</a><span class='DateWithTime floatright'>");
            //        concatinater.Append(date);
            //        concatinater.Append("</span></ h2> </div>");
            //        concatinater.Append("<p class='width100%'>");
            //        concatinater.Append(row["MessageText"].ToString());
            //        concatinater.Append("</p></div>");


            //    }
            //    Response.Write(concatinater.ToString());
            //} 
            #endregion


            #region Delete a Message

            if (Request.Form["msgeID"] != null)
            {


                Messages message = new Messages(int.Parse(Request.Form["msgeID"].ToString()));
                MessagesDAL test = new MessagesDAL();
                test.DeleteMessage(message);
                Response.Write("<div class='msgeSentNotification blueFontTextColor BorberRad3'>Your message was deleted successfully.</div>");

            }
            #endregion


            #region Get Friend Profile
            if (Request.Form["encrypMemberID"] != null)
            {

                string frnd = Request.Form["encrypMemberID"].ToString();
                Member friend = new Member(frnd);
                Response.Write(Request.Form["encrypMemberID"].ToString());
                Response.Redirect("~/Profile.aspx");

              

            }


            #endregion

            //Changed to webmethod
            #region Insert A Comment
            if (Request.Form["ID"] != null && Request.Form["pstTxt"] != null)
            {

              
                
               MessagesDAL m = new MessagesDAL();
               Comments comment = new Comments();
               comment.MemberId = Session["memberID"].ToString();
               comment.CommentText = Request.Form["pstTxt"].ToString();
               comment.PostId = int.Parse(Request.Form["ID"].ToString());
               m.InsertComment(comment);

                //Refreshing the Comment count
                Post post = new Post();
                post.PostId = comment.PostId;
                
              //  Response.Write(m.CountComments(post));

                var comentArray = new string[] { m.CountComments(post).ToString(), };

                //JavaScriptSerializer serializer = new JavaScriptSerializer();
                //Response.Write(serializer.Serialize(loc));
                //.Response.ContentType = "application/json";
                //End Refresh
            }
            #endregion


            #region get all Comments
            if (Request.Form["ID"] != null && Request.Form["pstTxt"] == null)
            {

                Post post = new Post();
                Comments comment = new Comments();
                MessagesDAL m = new MessagesDAL();
                string date = "";
              
                post.PostId = int.Parse(Request.Form["ID"].ToString());


                StringBuilder comnt = new StringBuilder();
                DataTable comments = m.GetAllComments(post);
                
                foreach (DataRow row in comments.Rows)
                {
                    date = Convert.ToDateTime(row["CreateDate"]).ToString("D");

                    //Building single message view box
                    comnt.Append("<div id='");
                    comnt.Append((row["CommentID"]).ToString());
                    comnt.Append("' class='aComment'><div class='deleteComment floatright'><a class='btnDleteComment'>X</a></div><h5><a style='font-weight:normal;' ");
                    comnt.Append("id='" + (row["MemberID"]).ToString() + "'> ");
                    //comnt.Append("' href='#'> ");
                    comnt.Append(row["Friend"].ToString());
                    comnt.Append("</a><span class='DateWithTime ui-corner-all floatright'>");
                    comnt.Append(date);
                    comnt.Append("</span></h5><p>");
                    comnt.Append((row["CommentText"]).ToString());
                    comnt.Append("</p></div>");
                   


                }

                Response.Write(comnt.ToString());
                //End Refresh
            }
            #endregion




        }



    }
}