using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

namespace BulabulaApp
{
    public partial class WebForm2 : System.Web.UI.Page
    {

        public string allgroups = null;
        public string AllBlockedFriendsString = null;
        public string AllFriendsString = null;
        public string AllFriendsInvitesString = null;
        public string FriendsOnlineString = null;

        public string CountBlockedFriendsString = null;
        public string CountFriendsString = null;
        public string CountFriendsInvitesString = null;

        public Member aMember = new Member();

        protected void Page_Load(object sender, EventArgs e)
        {
            Literal title = (Literal)Master.FindControl("pageTitle");
            title.Text = "My Friends";

            if (!IsPostBack)
            {
                if (Session["memberID"] == null)
                {

                    Response.Redirect("~/Login.aspx");

                }

                else
                {
                    #region RIGHT COLUMN GET 5 JOINEDGROUPS
                    //JOINED GROUPS==============================================================================


                    string memberId = Context.Session["memberID"].ToString();
                    aMember.MemberId = memberId;

                    Reusable_Methods reusable_Methods = new Reusable_Methods(aMember);
                    allgroups = reusable_Methods.RightColumnGet5JoinedGroups();

                    //===========================================================================================
                    #endregion

                    #region ALL BLOCKED FRIENDS
                    //ALL BLOCKED FRIENDS========================================================================


                    FriendshipDAL dal = new FriendshipDAL();

                    aMember.MemberId = memberId;

                    List<Member> BlockedMembersList = new List<Member>();

                    BlockedMembersList = dal.GetAllBlockedFriends(aMember);
                    int countAllBlockedFriends = BlockedMembersList.Count;

                    StringBuilder CountBlockedFriends = new StringBuilder();
                    CountBlockedFriends.Append("<h4 class='shinyRed ui-corner-top' >Blocked People (<span>");
                    CountBlockedFriends.Append(countAllBlockedFriends);
                    CountBlockedFriends.Append("</span>)</h4>");

                    CountBlockedFriendsString = CountBlockedFriends.ToString();
                    //display on names of blocked members. use value memeber
                    //listBox1.DataSource = BlockedMembersList;
                    //listBox1.DisplayMember = "DisplayName";
                    //listBox1.ValueMember = "MemberID";

                    StringBuilder AllBlockedFriends = new StringBuilder();

                    for (int i = 0; i < countAllBlockedFriends; ++i)
                    {
                        AllBlockedFriends.Append("<div class='aPost btnMemberProfile FriendSpacing' id='");
                        AllBlockedFriends.Append(BlockedMembersList[i].MemberId);
                        AllBlockedFriends.Append("'><div class='msgeHeading ui-corner-all' ><h2 class='ui-corner-all'> <a href='#' class=' ' style='font-weight:normal; '>");
                        AllBlockedFriends.Append(BlockedMembersList[i].DisplayName);

                        AllBlockedFriends.Append("</a><span class=' ui-corner-all floatright'>");
                        if (BlockedMembersList[i].IsOnline == true)
                        {
                            AllBlockedFriends.Append("Online");
                        }
                        else
                        {
                            AllBlockedFriends.Append("Offline");
                        }

                        AllBlockedFriends.Append("</span></h2></div>");

                        AllBlockedFriends.Append("</div>");
                    }

                    AllBlockedFriendsString = AllBlockedFriends.ToString();
                    //===========================================================================================
                    #endregion

                    #region ALL FRIENDS
                    //ALL FRIENDS================================================================================


                    //ALL FRIENDS

                    dal = new FriendshipDAL();

                    aMember.MemberId = memberId;

                    List<Member> FriendsList = new List<Member>();

                    FriendsList = dal.GetAllFriends(aMember);
                    int countAllFriends = FriendsList.Count;

                    StringBuilder CountFriends = new StringBuilder();
                    CountFriends.Append("<h4  class='shinyGreen ui-corner-top'> All Friends (<span>");
                    CountFriends.Append(countAllFriends);
                    CountFriends.Append("</span>)</h4>");
                    CountFriendsString = CountFriends.ToString();

                    StringBuilder AllFriends = new StringBuilder();

                    for (int i = 0; i < countAllFriends; ++i)
                    {

                        AllFriends.Append("<div class='aPost btnMemberProfile FriendSpacing' id='");
                        AllFriends.Append(FriendsList[i].MemberId);
                        AllFriends.Append("'><div class='msgeHeading ui-corner-all' ><h2 class='ui-corner-all'> <a href='#' class=' ' style='font-weight:normal;'>");
                        AllFriends.Append(FriendsList[i].DisplayName);

                        AllFriends.Append("</a><span class=' ui-corner-all floatright'>");
                        if (FriendsList[i].IsOnline == true)
                        {
                            AllFriends.Append("Online");
                        }
                        else
                        {
                            AllFriends.Append("Offline");
                        }

                        AllFriends.Append("</span></h2></div>");

                        AllFriends.Append("</div>");

                    }

                    AllFriendsString = AllFriends.ToString();
                    //===========================================================================================
                    #endregion

                    #region ALL FRIEND INVITES
                    //ALL FRIEND INVITES================================================================================


                    //ALL FRIEND INVITES

                    dal = new FriendshipDAL();

                    aMember.MemberId = memberId;

                    List<Member> FriendInvitesList = new List<Member>();

                    FriendInvitesList = dal.GetAllFriendInvites(aMember);
                    int countAllFriendsInvites = FriendInvitesList.Count;

                    StringBuilder CountFriendsInvites = new StringBuilder();
                    CountFriendsInvites.Append("<h4  class='shinyLightBlue ui-corner-top' >Invites (<span>");
                    CountFriendsInvites.Append(countAllFriendsInvites);
                    CountFriendsInvites.Append("</span>)</h4>");
                    CountFriendsInvitesString = CountFriendsInvites.ToString();

                    StringBuilder AllFriendsInvites = new StringBuilder();

                    for (int i = 0; i < countAllFriendsInvites; ++i)
                    {



                        AllFriendsInvites.Append("<div class='aPost btnMemberProfile FriendSpacing' id='");
                        AllFriendsInvites.Append(FriendInvitesList[i].MemberId);
                        AllFriendsInvites.Append("'><div class='msgeHeading ui-corner-all' ><h2 class='ui-corner-all'> <a href='#' class=' ' style='font-weight:normal;'>");
                        AllFriendsInvites.Append(FriendInvitesList[i].DisplayName);

                        AllFriendsInvites.Append("</a><span class=' ui-corner-all floatright'>");
                        if (FriendInvitesList[i].IsOnline == true)
                        {
                            AllFriendsInvites.Append("Online");
                        }
                        else
                        {
                            AllFriendsInvites.Append("Offline");
                        }

                        AllFriendsInvites.Append("</span></h2></div>");

                        AllFriendsInvites.Append("</div>");
                    }

                    AllFriendsInvitesString = AllFriendsInvites.ToString();
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
                }
            }

        }
    }
}