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
    public partial class SiteMaster : System.Web.UI.MasterPage
    {



      //  private Post aPost;
      //  ArrayList ArrayPosts;
      //  ArrayList ArrayMembers;
      //  int countMembers;
        public string newsfeed = null;
        public string allgroups = null;
        public StringBuilder concatinater = new StringBuilder();
        public Member aMember = new Member();
        LoginDAL loginDAL = new LoginDAL();
        Member_Status member_Status;
        public string FriendsOnlineString = null;
        public string NotificationsString = null;
        public string defaultEmailAddress = null;
        public int ?year = null;
        DateTime now =  DateTime.Today;
       
        
      //  Reusable_Methods reusable_Methods;
        public string actualMemberDisplayName = null;
        string ex;
        public Literal PageTitleProperty
        {
            get { return pageTitle; }
         
            set { pageTitle = value; }
        }

        public HiddenField Expand
        {
            get { return caption; }
            set { caption = value; }
        }

      

        public void SetCurrentOageTitle(string title)
        {
            pageTitle.Text = title;
        }

        public void SetCaption(string title)
        {
            caption.Value = title;
        }



        protected void Page_Load(object sender, EventArgs e)
        {
            //To use for notifications
            SessionMemberID.Value = Session["memberID"].ToString();

            year = now.Year;
            if (Session["stayEx"] != null )
            {
                if (Session["stayEx"].ToString() == "expanded")
                { StayExpanded.Value = "expanded"; }

            }

           if (StayExpanded.Value == "expanded")
            {
                Session["stayEx"] = "expanded";
            }
            else { Session["stayEx"] = ""; }

         
          

            MessagesDAL member = new MessagesDAL();
            if (!IsPostBack)
            {

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

                defaultEmailAddress = SSTCryptographer.Decrypt(Session["memberID"].ToString())+"@live.nmmu.ac.za";

            }//END MAIN if LOADING FOR THE FIRST TIME
            else if (profileFriendID.Value != "")
            {
                #region REDIRECT TO CLICKED PROFILE
                //REDIRECT TO CLICKED PROFILE===================================================================
                if (profileFriendID.Value != null)
                {
                    Session["profileFriendID"] = profileFriendID.Value;
                    profileFriendID.Value = null;


                    Response.Redirect("~/Profile.aspx");

                }
                //=============================================================================================
                #endregion
            }
            else if (GoToGroupPageID.Value != "")
            {
                Session["GroupInfo"] = GoToGroupPageID.Value;
                Response.Redirect("~/Group.aspx");

            }
              else if (SelectedMessageID.Value != "")
            {
                Session["selectedMessageID"] = SelectedMessageID.Value;
                Response.Redirect("~/ViewMessages.aspx");

            }
            

            if (Session["memberID"] == null)
            {

                Response.Redirect("~/Login.aspx");

            }
            else
            {
                string memberId = Context.Session["memberID"].ToString();
                thisMemberID.Value = memberId;
                aMember.MemberId = memberId;

                #region GO ONLINE
                MemberInfoDAL memberInfoDAL = new MemberInfoDAL();
                memberInfoDAL.UpdateToOnline(aMember);
                #endregion

                #region MAIN ACTUAL DISPLAY NAME
                memberInfoDAL = new MemberInfoDAL();
                aMember = memberInfoDAL.GetActualDisplayName(aMember);

                actualMemberDisplayName = aMember.DisplayName;
                #endregion

                #region RIGHT COLUMN GET 5 JOINEDGROUPS
                //JOINED GROUPS==============================================================================

                Reusable_Methods reusable_Methods = new Reusable_Methods(aMember);
                allgroups = reusable_Methods.RightColumnGet5JoinedGroups();
                //===========================================================================================
                #endregion

                #region RIGHT COLUMN FRIENDS ONLINE
                //FRIENDS ONLINE==============================================================================

                memberId = Context.Session["memberID"].ToString();
                aMember.MemberId = memberId;

                reusable_Methods = new Reusable_Methods(aMember);
                FriendsOnlineString = reusable_Methods.RightColumnGetFriendsOnline();

                //===========================================================================================
                #endregion

                #region NOTIFICATIONS
                NotificationsString = reusable_Methods.RightColumnNotifications();

                #endregion

            }

        }//END PAGE_LOAD




        protected void btnLogout_Click(object sender, EventArgs e)
        {
            //GO OFFLINE
            aMember.MemberId = Context.Session["memberID"].ToString();
            MemberInfoDAL memberInfoDAL = new MemberInfoDAL();
            memberInfoDAL.UpdateToOffline(aMember);

            Session["memberID"] = null;

            if (Request.Cookies["login"] != null)
            {
                var c = new HttpCookie("login");
                c.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(c);
                Session.Abandon();
                Session.Contents.RemoveAll();

            }

            Response.Redirect("~/Login.aspx");
        }






    }
}
