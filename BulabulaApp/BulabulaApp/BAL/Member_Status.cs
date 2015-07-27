using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BulabulaApp
{
    public class Member_Status
    {
        private string memberId;
        private int statusId;
        private string reasonBlocked;
        private DateTime blockedDate;

        public Member_Status(string memberId, int statusId, string reasonBlocked, DateTime blockedDate)
        {
            MemberId = memberId;
            StatusId = statusId;
            ReasonBlocked = reasonBlocked;
            BlockedDate = blockedDate;
        }
        public Member_Status(string memberId)
        {
            MemberId = memberId;
        }


        #region Properties

        public string MemberId
        {
            get { return memberId; }
            set { memberId = value; }
        }

        public int StatusId
        {
            get { return statusId; }
            set { statusId = value; }
        }

        public string ReasonBlocked
        {
            get { return reasonBlocked; }
            set { reasonBlocked = value; }
        }

        public DateTime BlockedDate
        {
            get { return blockedDate; }
            set { blockedDate = value; }
        }

        #endregion

    }
}
