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
    public class FriendWebservice : System.Web.Services.WebService
    {

        #region Add as Friend
        [WebMethod(EnableSession = true)]
        public string InviteFriend(string friendID)
        {
            NotificationDAL notificationDAL = new NotificationDAL();

            //INVITE FRIEND

            FriendshipDAL dal = new FriendshipDAL();

            string memberId = Context.Session["memberID"].ToString();
            Member aMember = new Member(memberId);

            Member aFriend = new Member(friendID);

            if (dal.InviteExists(aMember, aFriend) == 1)
            {
                //An invitation has already been sent
                //MessageBox.Show("invite exists");
            }
            else if (dal.InviteExists(aMember, aFriend) == 0)
            {
                //invite friend
                dal.InviteFriend(aMember, aFriend);

                notificationDAL.InsertFriendRequestSentNotification(aMember, aFriend);
            }

            //UPDATE STATUS ON FORM
            //=====================================================================
            dal.GetInvitationStatus(aMember, aFriend);

            List<Member_Friend> statusList = new List<Member_Friend>();

            statusList = dal.GetInvitationStatus(aMember, aFriend);
            string InvitationStatus = statusList[0].InvitationStatus;

            return InvitationStatus;

        }
        #endregion

        #region Remove as Friend
        [WebMethod(EnableSession = true)]
        public string Unfriend(string friendID)
        {
            //FRIEND IGNORES YOUR INVITATION

            FriendshipDAL dal = new FriendshipDAL();


            string memberId = Context.Session["memberID"].ToString();
            Member aMember = new Member(memberId);

            string friendId = friendID;
            Member aFriend = new Member(friendId);

            dal.RemoveFriend(aMember, aFriend);

            return null;

        }
        #endregion

        #region Block a friend
        [WebMethod(EnableSession = true)]
        public bool Block(string friendID)
        {
            FriendshipDAL dal = new FriendshipDAL();

            string memberId = Context.Session["memberID"].ToString();
            Member aMember = new Member(memberId);

            string friendId = friendID;
            Member aFriend = new Member(friendId);

            if (dal.MemberIsActaullyTheMember(aMember, aFriend) == 1)
            {
                dal.BlockFriend(aMember, aFriend);
                return true;
            }
            else if (dal.FriendIsActaullyTheMember(aMember, aFriend) == 1)
            {
                dal.BlockMember(aMember, aFriend);
                return true;
            }
            else
            {
                return false;
            }


        }
        #endregion

        #region Unblock a friend
        [WebMethod(EnableSession = true)]
        public bool Unblock(string friendID)
        {
            FriendshipDAL dal = new FriendshipDAL();

            string memberId = Context.Session["memberID"].ToString();
            Member aMember = new Member(memberId);

            string friendId = friendID;
            Member aFriend = new Member(friendId);

            if (dal.MemberIsActaullyTheMember(aMember, aFriend) == 1)
            {
                dal.UnblockFriend(aMember, aFriend);
                return true;
            }
            else if (dal.FriendIsActaullyTheMember(aMember, aFriend) == 1)
            {
                dal.UnblockMember(aMember, aFriend);
                return true;
            }
            else
            {
                return false;
            }


        }
        #endregion

        #region Accept friend request
        [WebMethod(EnableSession = true)]
        public string Accept(string friendID)
        {
            NotificationDAL notificationDAL = new NotificationDAL();

            //FRIEND ACCEPTS YOU INVITATION

            FriendshipDAL dal = new FriendshipDAL();

            string memberId = Context.Session["memberID"].ToString();
            Member aMember = new Member(memberId);

            Member aFriend = new Member(friendID);

            dal.UpdateInvitationStatus(aMember, aFriend, "Accepted");

            notificationDAL.InsertFriendRequestAcceptedNotification(aMember, aFriend);

            //To update status on form
            //=====================================================================
            dal.GetInvitationStatus(aMember, aFriend);

            List<Member_Friend> statusList = new List<Member_Friend>();

            statusList = dal.GetInvitationStatus(aMember, aFriend);
            string InvitationStatus = statusList[0].InvitationStatus;
            return InvitationStatus;

        }
        #endregion

    }
}
