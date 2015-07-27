using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.Security.Permissions;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;
using System.DirectoryServices.AccountManagement;
using System.Collections;
using System.Drawing;
using System.IO;


//[assembly: SecurityPermission(SecurityAction.RequestMinimum, Execution = true)]
//[assembly: DirectoryServicesPermission(SecurityAction.RequestMinimum)]

#region REFERENCES
/// <summary>
/// Summary description for ActiveDirectoryDAL
/// Retreives user information from the active directory
/// REFERENCES
/// *Get information for a user in the active directory
/// http://www.myjeeva.com/2012/04/querying-active-directory-using-csharp/
/// http://cloud.github.com/downloads/jeevatkm/generic-repo/ActiveDirectory.zip
/// *Get all groups for a user in the active directory
/// http://urenjoy.blogspot.com/2009/04/getting-active-directory-groups-from.html
/// *Validate a credentials against the active directory
/// http://stackoverflow.com/questions/290548/c-sharp-validate-a-username-and-password-against-active-directory
/// </summary>
#endregion


namespace BulabulaApp
{
    public class ActiveDirectoryDAL
    {
        #region ACTIVE DIRECTORY ATTRIBUTES
        private string username;
        private string password;

        private string domain;
        private string LDAPServer;

        DirectorySearcher dirSearch = null;
        SearchResult rs = null;

        #endregion

        #region ACTIVE DIRECTORY CONSTRUCTOR
        public ActiveDirectoryDAL(string username, string password)
        {
            this.username = username;
            this.password = password;
            this.domain = "nmmu.ac.za";
            this.LDAPServer = "LDAP://nmmu.ac.za";
        }
        #endregion

        #region VALIDATE CREDENTIALS
        public bool ValidateCredentials()
        {

            try
            {
                // create a "principal context" - e.g. your domain (could be machine, too) 
                using (PrincipalContext pc = new PrincipalContext(ContextType.Domain, domain))
                {
                    try
                    {
                        // validate the credentials 
                        bool isValid = pc.ValidateCredentials(username, password);
                        return isValid;
                    }
                    catch (Exception e)
                    {
                        e.Message.ToString();
                        return false;
                    }
                }
            }
            catch (System.DirectoryServices.AccountManagement.PrincipalServerDownException ex)
            {
                //server could not be contacted
                ex.Message.ToString();
                HttpContext.Current.Response.Redirect("~/Error_Pages/503.htm");
                return false;
            }
            catch (Exception ex)
            {
                //general exeption
                ex.Message.ToString();
                HttpContext.Current.Response.Redirect("~/Error_Pages/503.htm");
                return false;
            }
        }
        #endregion

        #region GET SYSTEM DOMAIN
        private string GetSystemDomain()
        {
            try
            {
                return Domain.GetComputerDomain().ToString().ToLower();
            }
            catch (Exception e)
            {
                e.Message.ToString();
                return string.Empty;
            }
        }
        #endregion

        #region CHECK IF USER
        public void GetUserInformation()
        {
            try
            {
                rs = SearchUserByUserName(GetDirectorySearcher());
            }
            catch (Exception e)
            {
                e.Message.ToString();
                HttpContext.Current.Response.Redirect("~/Error_Pages/503.htm");
            }
        }
        #endregion

        #region GET USER INFORMATION

        public string GetFirstName()
        {
            try
            {
                return rs.GetDirectoryEntry().Properties["givenName"].Value.ToString();
            }
            catch (Exception e)
            {
                e.Message.ToString();
                HttpContext.Current.Response.Redirect("~/Error_Pages/503.htm");
                return null;
            }
        }
        public string GetLastName()
        {
            try
            {
                return rs.GetDirectoryEntry().Properties["sn"].Value.ToString();
            }
            catch (Exception e)
            {
                e.Message.ToString();
                HttpContext.Current.Response.Redirect("~/Error_Pages/503.htm");
                return null;
            }
        }
        public string GetEmail()
        {
            try
            {
                return rs.GetDirectoryEntry().Properties["mail"].Value.ToString();
            }
            catch (Exception e)
            {
                e.Message.ToString();
                HttpContext.Current.Response.Redirect("~/Error_Pages/503.htm");
                return null;
            }
        }
        public string GetCompany()
        {
            try
            {
                return rs.GetDirectoryEntry().Properties["company"].Value.ToString();
            }
            catch (Exception e)
            {
                e.Message.ToString();
                HttpContext.Current.Response.Redirect("~/Error_Pages/503.htm");
                return null;
            }
        }
        public string GetDisplayName()
        {
            try
            {
                return rs.GetDirectoryEntry().Properties["displayName"].Value.ToString();
            }
            catch (Exception e)
            {
                e.Message.ToString();
                HttpContext.Current.Response.Redirect("~/Error_Pages/503.htm");
                return null;
            }
        }
        public string GetFullName()
        {
            try
            {
                return rs.GetDirectoryEntry().Properties["CN"].Value.ToString();
            }
            catch (Exception e)
            {
                e.Message.ToString();
                HttpContext.Current.Response.Redirect("~/Error_Pages/503.htm");
                return null;
            }
        }
        public string GetDepartment()
        {
            try
            {
                return rs.GetDirectoryEntry().Properties["department"].Value.ToString();
            }
            catch (Exception e)
            {
                e.Message.ToString();
                HttpContext.Current.Response.Redirect("~/Error_Pages/503.htm");
                return null;
            }
        }
        public string GetDescription()
        {
            try
            {
                return rs.GetDirectoryEntry().Properties["description"].Value.ToString();
            }
            catch (Exception e)
            {
                e.Message.ToString();
                HttpContext.Current.Response.Redirect("~/Error_Pages/503.htm");
                return null;
            }
        }
        public Image GetThumbnailPhoto()
        {
            try
            {
                if (rs.GetDirectoryEntry().Properties["thumbnailPhoto"].Value != null)
                {
                    byte[] imageBytes = rs.GetDirectoryEntry().Properties["thumbnailPhoto"].Value as byte[];
                    MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
                    // Convert byte[] to Image
                    ms.Write(imageBytes, 0, imageBytes.Length);
                    Image image = Image.FromStream(ms, true);
                    return image;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                e.Message.ToString();
                HttpContext.Current.Response.Redirect("~/Error_Pages/503.htm");
                return null;
            }
        }
        #endregion

        #region GET DIRECTORY SEARCHER
        private DirectorySearcher GetDirectorySearcher()
        {
            if (dirSearch == null)
            {
                try
                {
                    dirSearch = new DirectorySearcher(
                        new DirectoryEntry("LDAP://" + domain, username, password));
                }
                catch (DirectoryServicesCOMException e)
                {
                    HttpContext.Current.Response.Redirect("~/Error_Pages/401.htm");
                    e.Message.ToString();
                }
                return dirSearch;
            }
            else
            {
                return dirSearch;
            }
        }
        #endregion

        #region SEARCH USER BY USER NAME
        private SearchResult SearchUserByUserName(DirectorySearcher ds)
        {
            try
            {
                ds.Filter = "(&((&(objectCategory=Person)(objectClass=User)))(samaccountname=" + username + "))";

                ds.SearchScope = SearchScope.Subtree;
                ds.ServerTimeLimit = TimeSpan.FromSeconds(90);

                SearchResult userObject = ds.FindOne();

                if (userObject != null)
                    return userObject;
                else
                    return null;
            }
            catch (Exception e)
            {
                e.Message.ToString();
                //HttpContext.Current.Response.Redirect("Error.aspx");
                return null;
            }
        }
        #endregion

        #region GET ALL GROUPS FOR A USER
        public ArrayList GetAllGroupsForAUser()
        {
            ArrayList groupMembers = new ArrayList();
            DirectoryEntry de;
            DirectorySearcher ds;

            try
            {
                if (username != "")
                {
                    de = new DirectoryEntry(LDAPServer, username, password);
                    ds = new DirectorySearcher(de);
                }
                else
                {
                    ds = new DirectorySearcher(LDAPServer);
                }

                // find all users in this group

                ds.Filter = String.Format("(&(samaccountname={0})(objectClass=person))", username);
                ds.PropertiesToLoad.Add("memberof");
                try
                {
                    foreach (SearchResult sr in ds.FindAll())
                    {
                        foreach (string str in sr.Properties["memberof"])
                        {
                            string str2 = str.Substring(str.IndexOf("=") + 1, str.IndexOf(",") - str.IndexOf("=") - 1);
                            groupMembers.Add(str2);
                        }
                    }
                }
                catch
                {
                    //ignore if any properties found in AD  
                }
                return groupMembers;
            }
            catch (Exception e)
            {
                e.Message.ToString();
                HttpContext.Current.Response.Redirect("~/Error_Pages/503.htm");
                return null;
            }
        }
        #endregion

        #region GET GROUP DESCRIPTION
        public string GetGroupDescription(string groupName)
        {
            string groupMembers = "";

            try
            {
                PrincipalContext con = new PrincipalContext(ContextType.Domain, domain, username, password);
                UserPrincipal user = UserPrincipal.FindByIdentity(con, username);
                var pr = user.GetGroups();

                foreach (GroupPrincipal item in pr)
                {
                    GroupPrincipal goup = item;

                    if (groupName == goup.DisplayName)
                    {
                        groupMembers = goup.Description;
                        //Console.WriteLine(goup.Description + " (" + goup.DisplayName + ")");
                    }
                }
                return groupMembers;
            }
            catch (Exception e)
            {
                e.Message.ToString();
                HttpContext.Current.Response.Redirect("~/Error_Pages/503.htm");
                return null;
            }
        }
        #endregion
    }
}