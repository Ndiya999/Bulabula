using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Collections;
using System.Drawing;

namespace BulabulaApp
{
    public partial class Login : System.Web.UI.Page
    {
        ActiveDirectoryDAL userInformation;
        LoginDAL dal;
        ArrayList arrayGroups;
        public string failureText = "";

        protected void Page_Load(object sender, EventArgs e)
        {

            ErrorInfo.Visible = false;

            if (!IsPostBack)
            {
                //Check if the browser support cookies 
                if (Request.Browser.Cookies)
                {
                    //Check if the cookies wth name BulaBulaLogin exist on user's machine 
                    if (Request.Cookies["login"] != null)
                    {
                        string username = null, password = null;
                        if (Request.Cookies["login"]["username"] != null)
                        {
                            username = Request.Cookies["login"]["username"];
                        }
                        if (Request.Cookies["login"]["password"] != null)
                        {
                            password = Request.Cookies["login"]["password"];
                        }

                        userInformation = new ActiveDirectoryDAL(SSTCryptographer.Decrypt(username), SSTCryptographer.Decrypt(password));
                        Session["memberID"] = username;
                      //  Server.Transfer("~/Home.aspx"); 
                        Response.Redirect("~/Home.aspx");
                    }
                }
            }

        }

        protected void LoginButton_Click(object sender, EventArgs e)
        {

            Member_Status member_Status;
            dal = new LoginDAL();
           
            string username = LoginUser.UserName;
            string password = LoginUser.Password;

            
            
            //Validate the Credentials against the active directory
            userInformation = new ActiveDirectoryDAL(username, password);

            
            username = SSTCryptographer.Encrypt(username);

            bool isValid = userInformation.ValidateCredentials();
            if (!isValid) //Invalid Credentials
            {
                failureText = "Incorrect username or password.";
                ErrorInfo.Visible = true;
            }
            else if (dal.GetMemberStatus(member_Status = new Member_Status(username)))
            {
                failureText = "Your account has been suspended.";
                ErrorInfo.Visible = true;
            }
            else //Valid Credentials
            {
                userInformation.GetUserInformation();

                if (IsSchoolOfICTMember())
                {
                    //treat the case where we set the remember me check box
                    if (LoginUser.RememberMeSet)
                    {
                        RememberMe(username, password);
                    }

                    dal = new LoginDAL();

                    //save data into db if first time login
                    Member member; 
                    if (!dal.MemberExists(member = new Member(username)))
                    {
                        //add member to database 
                        member = new Member(username, userInformation.GetFirstName(), userInformation.GetLastName(), userInformation.GetDisplayName(), userInformation.GetEmail(), userInformation.GetThumbnailPhoto(), userInformation.GetDescription(), userInformation.GetCompany(), ValidateMemberType());
                        dal.InsertMember(member);

                        //assign member to groups
                       AssignMemberToGroups(username);
                    }

                    Session["memberID"] = username;
                   // Server.Transfer("~/Home.aspx"); 
                Response.Redirect("~/Home.aspx"); //Redirect to home page if the user is a memeber of the faculty of ICT
                }
                else
                {
                    //Display error message if the user is not a memeber of the faculty of ICT
                    failureText = "Sorry. You need to be a registered school of ICT member in order to gain access to this site";
                    ErrorInfo.Visible = true;
                }
            }
        
        }
        #region IS SCHOOL OF ICT MEMBER
        /// <summary>
        /// Validate if the user is a memeber of the school of ICT against the active directory
        /// </summary>
        /// <returns></returns>
        public bool IsSchoolOfICTMember()
        {
            dal = new LoginDAL();
            Membership membership = new Membership(userInformation.GetDepartment(), userInformation.GetDescription());
            if (dal.MembershipExists(membership))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region REMEMBER ME COOKIE
        public void RememberMe(string username, string password)
        {
            try
            {
                if (Request.Cookies["login"] != null)
                {
                    HttpCookie getCookie = Request.Cookies.Get("login");
                }
                else
                {
                    HttpCookie cookie = new HttpCookie("login");
                    Response.Cookies.Add(cookie);
                    cookie.Values.Add("username", username);
                    cookie.Values.Add("password", SSTCryptographer.Encrypt(password));
                    cookie.Expires = DateTime.Now.AddMonths(1);
                }
            }
            catch (Exception e)
            {
                e.ToString();
            }
        }
        #endregion

        #region ASSIGN MEMBER TO GROUPS
        /// <summary>
        /// Assigns member to module groups found in the active directory
        /// </summary>
        /// <param name="username"></param>
        public void AssignMemberToGroups(string username)
        {
            arrayGroups = userInformation.GetAllGroupsForAUser();
            int count = arrayGroups.Count;

            for (int i = 0; i < count; ++i)
            {
                bool isValidFirst3CharsOfModulesCode = false;
                bool isValidLast4CharsOfModulesCode = false;
                //eliminate none modules codes
                if (arrayGroups[i].ToString().Length == 7) //Number of characters in module code. eg ONT3210
                {
                    string first3CharsOfModulesCode = arrayGroups[i].ToString().Substring(0, 3);
                    foreach (char c in first3CharsOfModulesCode)
                    {
                        if ((int)c >= 65 && (int)c <= 90) //First 3 characters on modulue code EACH between A - Z
                        {
                            isValidFirst3CharsOfModulesCode = true;
                        }
                        else
                        {
                            isValidFirst3CharsOfModulesCode = false;
                            break;
                        }
                    }

                    string last4CharsOfModulesCode = arrayGroups[i].ToString().Substring(3, 4);
                    foreach (char c in last4CharsOfModulesCode)
                    {
                        if ((int)c >= 48 && (int)c <= 57) //Last 4 characters on modulue code EACH between 0 - 9
                        {
                            isValidLast4CharsOfModulesCode = true;
                        }
                        else
                        {
                            isValidLast4CharsOfModulesCode = false;
                            break;
                        }
                    }

                    if (isValidFirst3CharsOfModulesCode == true && isValidLast4CharsOfModulesCode == true)
                    {
                        //add group to database if group not in database
                        dal = new LoginDAL();
                        Group group;
                        if (!dal.GroupExists(group = new Group(arrayGroups[i].ToString())))
                        {
                            dal.InsertGroup(group = new Group(arrayGroups[i].ToString(), userInformation.GetGroupDescription(arrayGroups[i].ToString())));
                        }
                        //finally assign the member to a module group found in the active directory
                        Group_Member group_Member = new Group_Member(username, dal.GetGroupID(group));
                        dal.AssignMemberTogroup(group_Member);
                    }
                }
            }
        }
        #endregion

        #region VALIDATE MEMBER TYPE
        public string ValidateMemberType()
        {
            arrayGroups = userInformation.GetAllGroupsForAUser();
            int count = arrayGroups.Count;

            string memberType = "";
            for (int i = 0; i < count; ++i)
            {
                //eliminate none modules codes
                if (arrayGroups[i].ToString() == "All Staff") //Number of characters in module code. eg ONT3210
                {
                    memberType = "Stuff";
                    break;
                }
                else if (arrayGroups[i].ToString() == "All Students")
                {
                    memberType = "Student";
                    break;
                }
            }
            return memberType;
        }
        #endregion
    }
}