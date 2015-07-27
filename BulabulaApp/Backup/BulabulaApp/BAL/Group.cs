using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BulabulaApp
{
    public class Group
    {
        private int groupId;
        private string groupName;
        private string groupDescription;
        private DateTime createDate;
        private string memberId;
        public int NumberOfMenbers { get; set; }

        public Group(int groupId, string groupName, string groupDescription, DateTime createDate, string memberId)
        {
            GroupId = groupId;
            GroupDescription = groupDescription;
            GroupName = groupName;
            CreateDate = createDate;
            MemberId = memberId;
        }
        public Group(int groupId, string groupDescription)
        {
            GroupId = groupId;
            GroupDescription = groupDescription;
        }
        public Group(int groupId)
        {
            GroupId = groupId;
        }
        public Group()
        { }
        public Group(string groupDescription, int i)
        {
            GroupDescription = groupDescription;
        }
        public Group(string groupName)
        {
            GroupName = groupName;
        }

        public Group(string groupName, string groupDescription)
        {
            GroupName = groupName;
            GroupDescription = groupDescription;
        }

        #region Properties
        public int GroupId
        {
            get { return groupId; }
            set { groupId = value; }
        }

        public string GroupName
        {
            get { return groupName; }
            set { groupName = value; }
        }

        public string GroupDescription
        {
            get { return groupDescription; }
            set { groupDescription = value; }
        }

        public DateTime CreateDate
        {
            get { return createDate; }
            set { createDate = value; }
        }

        public string MemberId
        {
            get { return memberId; }
            set { memberId = value; }
        }
        #endregion
    }
}