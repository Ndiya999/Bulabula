using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Web.Script.Services;
using System.Text;
using System.Data;
using System.Web.Script.Serialization;
namespace BulabulaApp.WebServices
{
    /// <summary>
    /// Summary description for processComments
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ScriptService]
    [System.ComponentModel.ToolboxItem(false)]
     public class Searchfriend : System.Web.Services.WebService
    {
        MessagesDAL m = new MessagesDAL();

        NewsfeedDAL newsFeedDAL = new NewsfeedDAL();
       

        #region Search for a friend
        [WebMethod(EnableSession = true)]
        public List<AutoCompleteDTO> GetAllFriends(string prefixTxt)
        {
            

            //GetCompletionList

            MessagesDAL member = new MessagesDAL();
            List<Member> members = member.SearchMember(prefixTxt);
            AutoCompleteDTO auto;
            List<AutoCompleteDTO> items = new List<AutoCompleteDTO>();
            string[,] mmb = new string[1,2];
   

         
            foreach (var element in members)
            {

                auto = new AutoCompleteDTO(element.MemberId, element.DisplayName);
                items.Add(auto);

            }
       
            return items;
        }

        #endregion


        #region Search for a Member
        [WebMethod(EnableSession = true)]
        public List<AutoCompleteDTO> GetAllMembers(string prefixTxt)
        {


            //GetCompletionList

            MessagesDAL member = new MessagesDAL();
            List<Member> members = member.SearchMember(prefixTxt);
            List<Group> groups = member.SearchGroup(prefixTxt);

            AutoCompleteDTO auto;
            List<AutoCompleteDTO> items = new List<AutoCompleteDTO>();
            string[,] mmb = new string[1, 2];


            //ADDING MEMBERS TO LIST
            foreach (var element in members)
            {
                if (element.MemberId == HttpContext.Current.Session["memberID"].ToString())
                {
                    //DO NOT ADD
                }
                else
                {
                    auto = new AutoCompleteDTO(element.MemberId, element.DisplayName, "People");
                    items.Add(auto);
                }

            }

            //ADDING GROUPS TO LIST
            foreach (var element in groups)
            {
                if (element.MemberId == HttpContext.Current.Session["memberID"].ToString())
                {
                    //DO NOT ADD
                }
                else
                {
                    auto = new AutoCompleteDTO(element.GroupId.ToString(), element.GroupDescription, "Groups");
                    items.Add(auto);
                }

            }

            return items;
        }

        #endregion
      
        MessagesDAL test = new MessagesDAL();


        #region Search for an untagged friend
        [WebMethod(EnableSession = true)]
        public List<AutoCompleteDTO>  GetUntaggedFriends(string prefixTxt, int postID)
        {

            Member m;
            Post p;

            MessagesDAL member = new MessagesDAL();
            List<Member> members = member.SearchUntaggedMember(m = new Member(Context.Session["memberID"].ToString(), prefixTxt), p = new Post(postID));
            AutoCompleteDTO auto;
            List<AutoCompleteDTO> items = new List<AutoCompleteDTO>();
            string[,] mmb = new string[1, 2];



            foreach (var element in members)
            {

                auto = new AutoCompleteDTO(element.MemberId, element.DisplayName);
                items.Add(auto);

            }

            return items;
        }

        #endregion


        [WebMethod]
        public static string GetMemberProfile(string encrypMemberID)
         {
             string m = encrypMemberID;

             return "<div class='msgeSentNotification blueFontTextColor BorberRad3'>Your Message was sent successfully.</div>";
    
        }  
    
    
    }
}
