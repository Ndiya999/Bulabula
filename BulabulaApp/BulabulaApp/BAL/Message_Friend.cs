using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BulabulaApp
{
    class Message_Friend
    {
        private int messageId;
        private int friendId;
        private bool friendDeleted;
        private bool memberDeleted;

        public Message_Friend(int messageId,int friendId,bool friendDeleted,bool memberDeleted)
        {
            MessageId = messageId;
            FriendId = friendId;
            FriendDeleted = friendDeleted;
            MemberDeleted = memberDeleted;
        }

        #region Properties

        public int MessageId
        {
            get { return messageId; }
            set { messageId = value; }
        }

        public int FriendId
        {
            get { return friendId; }
            set { friendId = value; }
        }

        public bool FriendDeleted
        {
            get { return friendDeleted; }
            set { friendDeleted = value; }
        }

        public bool MemberDeleted
        {
            get { return memberDeleted; }
            set { memberDeleted = value; }
        }

        #endregion
    }
}
