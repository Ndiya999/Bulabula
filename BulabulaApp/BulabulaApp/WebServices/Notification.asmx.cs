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
    /// Summary description for processComments
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ScriptService]
    [System.ComponentModel.ToolboxItem(false)]
    public class Notification : System.Web.Services.WebService
    {

        [WebMethod(EnableSession = true)]
        public string PostRatingNotification()
        {
            Member aMember = new Member(Context.Session["memberID"].ToString());

            Reusable_Methods reusable_Methods = new Reusable_Methods(aMember);
            return reusable_Methods.PostRatingNotifications();
        }

        [WebMethod(EnableSession = true)]
        public string CommentRatingNotification()
        {
            Member aMember = new Member(Context.Session["memberID"].ToString());

            Reusable_Methods reusable_Methods = new Reusable_Methods(aMember);
            return reusable_Methods.CommentRatingNotifications();
        }

        [WebMethod(EnableSession = true)]
        public string TagNotification()
        {
            Member aMember = new Member(Context.Session["memberID"].ToString());

            Reusable_Methods reusable_Methods = new Reusable_Methods(aMember);
            return reusable_Methods.TagNotifications();
        }

        [WebMethod(EnableSession = true)]
        public string FriendRequestSentNotification()
        {
            Member aMember = new Member(Context.Session["memberID"].ToString());

            Reusable_Methods reusable_Methods = new Reusable_Methods(aMember);
            return reusable_Methods.FriendRequestSentNotifications();
        }

        [WebMethod(EnableSession = true)]
        public string FriendRequestAcceptedNotification()
        {
            Member aMember = new Member(Context.Session["memberID"].ToString());

            Reusable_Methods reusable_Methods = new Reusable_Methods(aMember);
            return reusable_Methods.FriendRequestAcceptedNotifications();
        }

        [WebMethod(EnableSession = true)]
        public string CommentNotification()
        {
            Member aMember = new Member(Context.Session["memberID"].ToString());

            Reusable_Methods reusable_Methods = new Reusable_Methods(aMember);
            return reusable_Methods.CommentNotifications();
        }

        [WebMethod(EnableSession = true)]
        public string EventNotification()
        {
            Member aMember = new Member(Context.Session["memberID"].ToString());

            Reusable_Methods reusable_Methods = new Reusable_Methods(aMember);
            return reusable_Methods.EventNotifications();
        }

        [WebMethod(EnableSession = true)]
        public string MessageNotification()
        {
            Member aMember = new Member(Context.Session["memberID"].ToString());

            Reusable_Methods reusable_Methods = new Reusable_Methods(aMember);
            return reusable_Methods.MessageNotifications();
        }

        [WebMethod(EnableSession = true)]
        public int[] GetAllNotifications()
        {
            Member aMember = new Member(Context.Session["memberID"].ToString());
            Reusable_Methods reusable_Methods = new Reusable_Methods(aMember);

            int messages = reusable_Methods.CountMessageNotifications();
            int friendRequests = reusable_Methods.CountFriendRequestSentNotifications();
            int tags = reusable_Methods.CountTagNotifications();
            int events = reusable_Methods.CountEventNotifications();



            int[] notifications = new int[10];
            notifications[0] = messages;
            notifications[1] = friendRequests;
            notifications[2] = tags;
            notifications[3] = events;

            return notifications;
        }



    }
}
