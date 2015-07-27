using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BulabulaApp
{
    class Member_Friend
    {
        private string memberId;
        private string friendId;
        private bool isBlocked;
        private string invitationStatus;
        private bool memberBlockedFriend;
        private bool friendBlockedMember;

        public Member_Friend(string memberId, string friendId, string invitationStatus, bool isBlocked)
        {
            MemberId = memberId;
            FriendId = friendId;
            InvitationStatus = invitationStatus;
            IsBlocked = isBlocked;
        }

        public Member_Friend(string invitationStatus)
        {
            InvitationStatus = invitationStatus;
        }
        public Member_Friend(bool memberBlockedFriend)
        {
            MemberBlockedFriend = memberBlockedFriend;
        }
        public Member_Friend(bool friendBlockedMember, int i)
        {
            FriendBlockedMember = friendBlockedMember;
        }

        #region Properties

        public string MemberId
        {
            get { return memberId; }
            set { memberId = value; }
        }

        public string FriendId
        {
            get { return friendId; }
            set { friendId = value; }
        }

        public bool IsBlocked
        {
            get { return isBlocked; }
            set { isBlocked = value; }
        }

        public string InvitationStatus
        {
            get { return invitationStatus; }
            set { invitationStatus = value; }
        }
        public bool FriendBlockedMember
        {
            get { return friendBlockedMember; }
            set { friendBlockedMember = value; }
        }

        public bool MemberBlockedFriend
        {
            get { return memberBlockedFriend; }
            set { memberBlockedFriend = value; }
        }
        #endregion

    }
}
