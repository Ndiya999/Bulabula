using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BulabulaApp
{
    class Message
    {
        private int messageId;
        private DateTime dateTime;
        private string messageText;
        private int memberId;

        public Message(int messageId, DateTime dateTime, string messageText, int memberId)
        {
            MessageId = messageId;
            DateTime = dateTime;
            MessageText = messageText;
            MemberId = memberId;
        }
        public Message(int messageId)
        {
            MessageId = messageId;
        }
        public Message()
        {

        }
        #region Properties

        public int MessageId
        {
            get { return messageId; }
            set { messageId = value; }
        }

        public DateTime DateTime
        {
            get { return dateTime; }
            set { dateTime = value; }
        }

        public string MessageText
        {
            get { return messageText; }
            set { messageText = value; }
        }

        public int MemberId
        {
            get { return memberId; }
            set { memberId = value; }
        }

        #endregion
    }
}
