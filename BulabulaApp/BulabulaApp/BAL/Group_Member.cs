using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BulabulaApp
{
    public class Group_Member
    {
        private string memberId;
        private int groupId;

        public Group_Member(string memberId, int groupId)
        {
            MemberId = memberId;
            GroupId = groupId;
        }

        #region Properties

        public string MemberId
        {
            get { return memberId; }
            set { memberId = value; }
        }

        public int GroupId
        {
            get { return groupId; }
            set { groupId = value; }
        }

        #endregion
    }
}
