using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BulabulaApp
{
    public class Messages
    {
        private int messageId;
        private DateTime dateTime; 
        private string messageText;
        private int memberId;

        public Messages()
        {
        }

        public Messages( DateTime dateTime, string messageText, int memberId )
        { 
        
            
            this.dateTime = dateTime;
            this.messageText = messageText;
            this.memberId = memberId;
        
        
        }

           public Messages(string messageText)
        {

        
            this.messageText = messageText;

        }

        public Messages(DateTime dateTime, string messageText)
        {


            this.dateTime = dateTime;
            this.messageText = messageText;
          



        }

        public Messages(int messageId)
        {
            this.messageId = messageId;
        
        }

        public Messages(int messageId, DateTime dateTime, string messageText, int memberId)
        {

            this.messageId = messageId;
            this.dateTime = dateTime;
            this.messageText = messageText;
            this.memberId = memberId;


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