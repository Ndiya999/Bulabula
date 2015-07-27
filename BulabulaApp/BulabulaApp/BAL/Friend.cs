using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BulabulaApp
{
    public class Friend
    {
        private string friendId;

        public Friend(string friendId)
        {
            FriendId = friendId;
        }

        public string FriendId
        {
            get { return friendId; }
            set { friendId = value; }
        }
    }
}
