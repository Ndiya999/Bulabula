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
    public partial class Profile : System.Web.UI.Page
    {

        FriendshipDAL FriendshipDAL = new FriendshipDAL();
        public string memberDataMain = null;
        public string heading = null;
        public string mostRecentPostDate = null;
        public string profileButtons = null;
        StringBuilder btnProfile = new StringBuilder();
        public string allgroups = null;
        public Member aMember = new Member();
        public Member aFriend = new Member();
        //   string friendID;
        LoginDAL loginDAL = new LoginDAL();
        Member_Status member_Status;
        public string actualMemberDisplayName = null;

        public string memberInfo = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            pageTitle.Text = "Profile";

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
                else if (Context.Session["profileFriendID"] != Context.Session["memberID"] && Context.Session["profileFriendID"] != null)
                {
                    aFriend.MemberId = Context.Session["profileFriendID"].ToString();

                    aMember.MemberId = Context.Session["memberID"].ToString();

                    GenerateProfile(aFriend);

                    #region BUILD PROFILE BUTTONS
                    //Building profile buttons

                    //To update status on form
                    //=====================================================================
                    FriendshipDAL.GetInvitationStatus(aMember, aFriend);

                    List<Member_Friend> statusList = new List<Member_Friend>();

                    statusList = FriendshipDAL.GetInvitationStatus(aMember, aFriend);
                    FriendshipDAL dal = new FriendshipDAL();
                    if (statusList.Count == 0)
                    {
                        btnProfile.Append("<a class='button  big addfriend' id='addfriend' >Add as friend</a><br />");
                    }
                    else
                    {
                        string InvitationStatus = statusList[0].InvitationStatus;
                        //=====================================================================

                        if (InvitationStatus == "Pending")
                        {
                            if (dal.MemberIsActaullyTheMember(aMember, aFriend) == 1)
                            {
                                btnProfile.Append("<a class='button  big disabledBtn' id='requestPending'  >Request Pending..</a><br />");
                                btnProfile.Append("<a class='button  big ' id='blockfriend' >Block this Person</a><br />");
                            }
                            else if (dal.FriendIsActaullyTheMember(aMember, aFriend) == 1)
                            {
                                btnProfile.Append("<a class='button  big ' id='acceptRequest' >Accept Request</a><br />");
                                btnProfile.Append("<a class='button  big ' id='blockfriend' >Block this Person</a><br />");
                            }
                        }

                        else if (InvitationStatus == "Ignored")
                        {
                            btnProfile.Append("<a class='button  big ' id='Ignored' >Ignored</a><br />");
                            btnProfile.Append("<a class='button  big ' id='blockfriend' >Block this Person</a><br />");
                        }
                        else
                        {
                            //Accepted

                            btnProfile.Append("<a class='button  big ' id='reportfriend' >Report this Person</a><br />");

                            #region GET BLOCKED STATUS
                            dal = new FriendshipDAL();


                            if (dal.MemberIsActaullyTheMember(aMember, aFriend) == 1)
                            {
                                List<Member_Friend> BlockedList = new List<Member_Friend>();

                                BlockedList = dal.GetFriendBlockedMemberStatus(aMember, aFriend);

                                if (BlockedList.Count != 0)
                                {
                                    if (BlockedList[0].FriendBlockedMember == false)
                                    {
                                        btnProfile.Append("<a class='button  big ' id='sendMessage' >Send Message</a><br />");

                                        //CHECK IF I BLOCKED THE FRIEND
                                        List<Member_Friend> IBlockedFriend = new List<Member_Friend>();
                                        IBlockedFriend = dal.GetMemberBlockedFriendStatus(aFriend, aMember);

                                        if (IBlockedFriend.Count != 0)
                                        {
                                            if (IBlockedFriend[0].MemberBlockedFriend == false)
                                            {
                                                btnProfile.Append("<a class='button  big ' id='blockfriend' >Block this Person</a><br />");
                                            }
                                            else
                                            {
                                                btnProfile.Append("<a class='button  big ' id='blockfriend' >Unblock this Person</a><br />");
                                            }

                                            btnProfile.Append("<a class='button  big removeFriend'  id='addfriend'>Remove Friend</a><br />");
                                        }
                                    }
                                }
                            }
                            else if (dal.FriendIsActaullyTheMember(aMember, aFriend) == 1)
                            {

                                List<Member_Friend> BlockedList = new List<Member_Friend>();

                                BlockedList = dal.GetMemberBlockedFriendStatus(aMember, aFriend);

                                if (BlockedList.Count != 0)
                                {
                                    if (BlockedList[0].MemberBlockedFriend == false)
                                    {
                                        btnProfile.Append("<a class='button  big ' id='sendMessage' >Send Message</a><br />");

                                        //CHECK IF I BLOCKED THE FRIEND
                                        List<Member_Friend> IBlockedFriend = new List<Member_Friend>();
                                        IBlockedFriend = dal.GetFriendBlockedMemberStatus(aFriend, aMember);

                                        if (IBlockedFriend.Count != 0)
                                        {
                                            if (IBlockedFriend[0].FriendBlockedMember == false)
                                            {
                                                btnProfile.Append("<a class='button  big ' id='blockfriend' >Block this Person</a><br />");
                                            }
                                            else
                                            {
                                                btnProfile.Append("<a class='button  big ' id='blockfriend' >Unblock this Person</a><br />");
                                            }

                                            btnProfile.Append("<a class='button  big removeFriend'  id='addfriend'>Remove Friend</a><br />");
                                        }
                                    }
                                }
                            }

                            //btnProfile.Append("<a class='button  big ' id='blockfriend' >Block this Person</a><br />");

                            //               <a class="button  big " id="reportfriend" >Report this Person</a><br />
                            //               <a class="button  big " id="blockfriend" >Block this Person</a> 
                            #endregion
                        }

                    }
                    profileButtons = btnProfile.ToString();
                    #endregion

                }
                else
                {
                    aMember.MemberId = Context.Session["memberID"].ToString();
                    GenerateProfile(aMember);
                }




            }//END IF ITS NOT POSTBACK
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


            if (Context.Session["profileFriendID"] != Context.Session["memberID"] && Context.Session["profileFriendID"] != null)
            {
                heading = aFriend.FirstName + " " + aFriend.LastName;
            }
            else
            {
                heading = aMember.FirstName + " " + aMember.LastName;
            }
            profileName.Value = heading;

        }



        private void GenerateProfile(Member aMember)
        {

            #region RIGHT COLUMN GET ALL JOINED GROUPS

            //JOINED GROUPS==============================================================================

            Reusable_Methods reusable_Methods = new Reusable_Methods(aMember);

            allgroups = reusable_Methods.RightColumnGetAllJoinedGroups();
            //============================================================================================
            #endregion

            #region RIGHT COLUMN MEMBER INFORMATION
            //MEMBER INFORMATION ON RIGHT COLUMN==========================================================================

            //MEMBER INFO AND PROFILE



            MemberInfoDAL dal = new MemberInfoDAL();


            //  Member member = new Member();
            aMember = dal.GetAllMemberInfo(aMember);

            profilesMemnerID.Value = aMember.MemberId;



            //Set the ImageUrl to the path of the handler with the querystring value
            ProfileImage.ImageUrl = "ProfilePicHandler.ashx?id=" + SSTCryptographer.Decrypt(aMember.MemberId);
            //call the method to get the image information and display it in Label Control


            //=======================================================================================================
            #endregion

            #region MAIN COLUMN MEMBER INFORMATION
            //MEMBER INFORMATION ON MAIN DISPLAY==========================================================================
            StringBuilder memberData = new StringBuilder();

            memberData.Append("<tr><td style='font-weight: bold;' > ");
            memberData.Append("First Name:</td><td>");
            memberData.Append(aMember.FirstName);
            memberData.Append("</td></tr> <tr><td style='font-weight: bold;'>");

            memberData.Append("Last Name:</td><td>");
            memberData.Append(aMember.LastName);
            memberData.Append("</td></tr> <tr><td style='font-weight: bold;' > ");

            memberData.Append("Display Name:</td><td>");
            memberData.Append(aMember.DisplayName);
            memberData.Append("</td></tr> <tr><td style='font-weight: bold;' > ");

            memberData.Append("Email:</td><td>");
            memberData.Append(aMember.Email);
            memberData.Append("</td></tr> <tr><td style='font-weight: bold;' > ");

            memberData.Append("Description:</td><td>");
            memberData.Append(aMember.Description);
            memberData.Append("</td></tr> <tr><td style='font-weight: bold;' > ");

            memberData.Append("Campus:</td><td>");
            memberData.Append(aMember.Campus);
            memberData.Append("</td></tr> <tr><td style='font-weight: bold;' > ");

            memberData.Append("Account Type:</td><td>");
            memberData.Append(aMember.MemberType);
            memberData.Append("</td></tr> <tr><td style='font-weight: bold;' > ");


            if (aMember.IsOnline == false)
            {
                memberData.Append("Online Status:</td><td style='color:red;'>");
                memberData.Append("Offline");
                memberData.Append("</td></tr>");


            }
            else
            {
                memberData.Append("Online Status:</td><td style='color:green;'>");
                memberData.Append("Online");
                memberData.Append("</td></tr>");



            }

            memberDataMain = memberData.ToString();


            #endregion

            #region MAIN COLUMN MOST RECENT POST DATE
            //MOST RECENT POST DATE ON MAIN DISPLAY==========================================================================
            NotificationDAL notificationDAL = new NotificationDAL();

            StringBuilder recentPostDate = new StringBuilder();
            Post aPost = new Post();
            aPost = notificationDAL.GetMostRecentPostDate(aMember);


            if (aPost.CreateDate == DateTime.Parse("1/1/0001 12:00:00 AM"))
            {
                reusable_Methods = new Reusable_Methods();
                string date = reusable_Methods.FormatDateTime(aPost.CreateDate);



                recentPostDate.Append("<p style='font-size: 1.2em; color: #A3ADB5;'>Most recent post date<span style='color: #0e93be;'> - ");
                recentPostDate.Append("No posts");
                recentPostDate.Append("</span></p>");

                mostRecentPostDate = recentPostDate.ToString();
            }
            else
            {
                reusable_Methods = new Reusable_Methods();
                string date = reusable_Methods.FormatDateTime(aPost.CreateDate);



                recentPostDate.Append("<p style='font-size: 1.2em; color: #A3ADB5;'>Most recent post date<span style='color: #0e93be;'> - ");
                recentPostDate.Append(date);
                recentPostDate.Append("</span></p>");

                mostRecentPostDate = recentPostDate.ToString();
            }
            #endregion

            #region MAIN ACTUAL DISPLAY NAME
            MemberInfoDAL memberInfoDAL = new MemberInfoDAL();
            Member actualMember = new Member(Context.Session["memberID"].ToString());
            aMember = memberInfoDAL.GetActualDisplayName(actualMember);

            actualMemberDisplayName = aMember.DisplayName;
            #endregion
        }



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