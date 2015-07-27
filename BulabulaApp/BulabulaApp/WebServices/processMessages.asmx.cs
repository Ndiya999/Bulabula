using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Web.Script.Services;
using System.Text;
using System.Data;

namespace BulabulaApp.WebServices
{
    /// <summary>
    /// Summary description for processMessages
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ScriptService]
    [System.ComponentModel.ToolboxItem(false)]

    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class processMessages : System.Web.Services.WebService
    {



        MessagesDAL test = new MessagesDAL();
        DataTable inbox;
        Member m = new Member();

        #region Send mutiple messages
        [WebMethod(EnableSession = true)]
        public string SendMultiMessage(string messageTxt, string friendList)
        {




            Member member = new Member();
            Member friend = new Member();
            bool isSent;
            string[] temp = friendList.Split(',');
            member.MemberId = Context.Session["memberID"].ToString();
            for (int i = 0; i < temp.Length; ++i)
                 {
                     if (temp[i] != "")
                     {
                         friend.MemberId = temp[i];
                         Messages message = new Messages(messageTxt);



                         isSent = test.AddMessage(message, member, friend);
                     }
                 }
           
            //  System.Threading.Thread.Sleep(500);


            //Chech if message is really sent
            isSent = true;


            if (isSent == true)
            {

                return "<div class='msgeSentNotification blueFontTextColor BorberRad3'>Your Message(s) was sent successfully.</div>";

            }
            else
            {


                return "<div class='errorNotification BorberRad3'>Error. Your message was not sent.</div>";


            }


        }
        #endregion

        #region Send a message
        [WebMethod(EnableSession = true)]
        public string SendMessage(string messageTxt, string friendID)
        {




            Member member = new Member();
            Member friend = new Member();
            bool isSent;

            member.MemberId = Context.Session["memberID"].ToString();
            friend.MemberId = friendID;
            Messages message = new Messages(messageTxt);



            isSent = test.AddMessage(message, member, friend);
            //  System.Threading.Thread.Sleep(500);


            //Chech if message is really sent
            isSent = true;


            if (isSent == true)
            {

                return "<div class='msgeSentNotification blueFontTextColor BorberRad3'>Your Message was sent successfully.</div>";

            }
            else
            {


                return "<div class='errorNotification BorberRad3'>Error. Your message was not sent.</div>";


            }


        }
        #endregion


        #region get nxt or prev  Message
        [WebMethod(EnableSession = true)]
        public string[] GetNxtPre(int messageID, string condition)
        {
            MessagesDAL test = new MessagesDAL();
            Member member = new Member();
            member.MemberId = Context.Session["memberID"].ToString();
            m.MemberId = Session["memberID"].ToString();
            inbox = test.getInboxList(m);

            Messages message = new Messages();



            message.MessageId = messageID;
            int[] index = test.GetMessageIndex(message, member);
            //int[] index = { 0, 0 };



            if (condition == "pre")
            {

                inbox = test.GetPreviousMessage(message, member);
            }
            else
            {
                inbox = test.GetNextMessage(message, member);
            }

            string concatinater = BuildSingleMessage(inbox);

            if (concatinater == "")
            {
                Message msg = new Message(messageID);

                inbox = test.GetAMessage(msg);
                concatinater = BuildSingleMessage(inbox);


            }

            string[] singleView = new string[] { index[0].ToString(), index[1].ToString(), concatinater };

            return singleView;

        }
        #endregion

        #region Build a Single Message
        private string BuildSingleMessage(DataTable table)
        {

            string date = null;
            StringBuilder concatinater = new StringBuilder();

            foreach (DataRow row in table.Rows)
            {
                Reusable_Methods reusable_Methods = new Reusable_Methods();
                date = reusable_Methods.FormatDateTime(DateTime.Parse((row["DateTime"]).ToString()));
                //date = Convert.ToDateTime(row["DateTime"]).ToString("M");

                //Building single message view box
                concatinater.Append("<div class='SingleViewmsgeWrapper' id='");
                concatinater.Append(row["messageID"].ToString());

                ////concatinater.Append("' ><div class='msgeHeading ui-corner-all' ><h2 class='ui-corner-all'> <a class='btnMemberProfile' id='");
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
            return concatinater.ToString();

        }
        #endregion

        #region GetCompletionList
        [WebMethod]
        public static string[] GetCompletionList(string prefixText, int count)
        {
            string encrypFriendID = null;

            if (count == 0)
            {

                count = 10;

            }

            MessagesDAL member = new MessagesDAL();
            List<Member> members = member.SearchMember(prefixText);

            List<string> items = new List<string>(count);


            foreach (var element in members)
            {
                encrypFriendID = element.MemberId;
                // items.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(element.DisplayName, encrypFriendID));
            }



            return items.ToArray();

        }
        #endregion


        #region Delete a message
        [WebMethod]
        public static string DeleteMessage(int messageID)
        {
            Messages message = new Messages(messageID);

            MessagesDAL test = new MessagesDAL();
            test.DeleteMessage(message);


            return "true";



        }

        #endregion

        #region Delete multiple messages
        [WebMethod]
        public void DeleteMessages(string messageIDs)
        {
            string[] tempArray = messageIDs.Split('_');

            MessagesDAL test = new MessagesDAL();

            for (int x = 1; x < tempArray.Length; ++x)
            {
                Messages message = new Messages(int.Parse(tempArray[x]));
                test.DeleteMessage(message);
            }
           



        }

        #endregion


        

    }
}
