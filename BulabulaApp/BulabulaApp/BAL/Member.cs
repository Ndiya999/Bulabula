using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;

namespace BulabulaApp
{
   
        public class Member
        {

            private string memberId;
            private string firstName;
            private string lastName;
            private string displayName;
            private DateTime registrationDate;
            private string email;
            private Image profilePicture;
            private string description;
            private string campus;
            private string memberType;
            private bool isOnline;

            public Member(string memberId, string displayName, DateTime registrationDate, string email, Image profilePicture, string description, string campus, string membertype, bool isOnline)
            {
                MemberId = memberId;
                DisplayName = displayName;
                RegistrationDate = registrationDate;
                Email = email;
                ProfilePicture = profilePicture;
                Description = description;
                Campus = campus;
                MemberType = memberType;
                IsOnline = isOnline;
            }

            public Member(string memberId, string displayName, string email, Image profilePicture, string description, string campus, string memberType)
            {
                MemberId = memberId;
                DisplayName = displayName;
                Email = email;
                ProfilePicture = profilePicture;
                Description = description;
                Campus = campus;
                MemberType = memberType;
            }

            public Member(string memberId, string firstName, string lastName, string displayName, string email, Image profilePicture, string description, string campus, string memberType)
            {
                MemberId = memberId;
                FirstName = firstName;
                LastName = lastName;
                DisplayName = displayName;
                Email = email;
                ProfilePicture = profilePicture;
                Description = description;
                Campus = campus;
                MemberType = memberType;
            }
            public Member(string memberid, string firstName, string lastName, string displayname, DateTime registrationdate, string email, Image profilepicture, string description, string campus, string membertype, bool isOnline)
            {
                MemberId = memberId;
                FirstName = firstName;
                LastName = lastName;
                DisplayName = displayName;
                Email = email;
                ProfilePicture = profilePicture;
                Description = description;
                Campus = campus;
                MemberType = memberType;
                IsOnline = isOnline;
            }
            public Member()
            {

            }
            public Member(string displayName, int i)
            {
                DisplayName = displayName;
            }

            public Member(string memberId)
            { 
                MemberId = memberId; 
            }

            public Member(string memberId, string displayName)
            {

                MemberId = memberId;
                DisplayName = displayName;
            }

            public Member(string memberId, string displayName, string email)
            {
                MemberId = memberId;
                DisplayName = displayName;
                Email = email;
            }
            public Member(string memberId, string displayName, int i)
            {

                MemberId = memberId;
                DisplayName = displayName;
            }

            public Member(string memberId, string displayName, string email, string description, string campus)
            {
                MemberId = memberId;
                DisplayName = displayName;
                Email = email;
                Description = description;
                Campus = campus;
            }
            public Member(string memberId, string displayName, string email, string description, string campus, bool isOnline, Image profilePicture)
            {
                MemberId = memberId;
                DisplayName = displayName;
                Email = email;
                Description = description;
                Campus = campus;
                IsOnline = isOnline;
                ProfilePicture = profilePicture;
            }

            #region PROPERTIES

            public string MemberId
            {
                get { return memberId; }
                set { memberId = value; }
            }
            public string FirstName
            {
                get { return firstName; }
                set { firstName = value; }
            }

            public string LastName
            {
                get { return lastName; }
                set { lastName = value; }
            }

            public string DisplayName
            {
                get { return displayName; }
                set { displayName = value; }
            }

            public DateTime RegistrationDate
            {
                get { return registrationDate; }
                set { registrationDate = value; }
            }

            public string Email
            {
                get { return email; }
                set { email = value; }
            }

            public Image ProfilePicture
            {
                get { return profilePicture; }
                set { profilePicture = value; }
            }

            public string Description
            {
                get { return description; }
                set { description = value; }
            }

            public string Campus
            {
                get { return campus; }
                set { campus = value; }
            }

            public string MemberType
            {
                get { return memberType; }
                set { memberType = value; }
            }

            public bool IsOnline
            {
                get { return isOnline; }
                set { isOnline = value; }
            }

            #endregion
        }
    }
