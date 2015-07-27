using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BulabulaApp
{
    public class Notification
    {
        private int notificationId;
        private string memberId;
        private string friendId;
        private int messageId;
        private int postId;
        private int ratingId;
        private int commentId;
        private string notificationType;
        private bool isRead;
        private int groupId;
        private int reportId;

        public Notification(int notificationId, string memberId, int postId, int ratingId, string notificationType)
        {
            NotificationId = notificationId;
            MemberId = memberId;
            PostId = postId;
            RatingId = ratingId;
            NotificationType = notificationType;
        }
        public Notification(int notificationId, string memberId, int ratingId, string notificationType, int commentId)
        {
            NotificationId = notificationId;
            MemberId = memberId;
            CommentId = commentId;
            RatingId = ratingId;
            NotificationType = notificationType;
        }
        public Notification(int notificationId, string memberId, int postId, string notificationType)
        {
            NotificationId = notificationId;
            MemberId = memberId;
            PostId = postId;
            RatingId = ratingId;
            NotificationType = notificationType;
        }
        public Notification(int notificationId, string memberId, string notificationType)
        {
            NotificationId = notificationId;
            MemberId = memberId;
            NotificationType = notificationType;
        }
        public Notification(int notificationId, string memberId, string notificationType, int i)
        {
            NotificationId = notificationId;
            MemberId = memberId;
            NotificationType = notificationType;
        }
        public Notification(string notificationType)
        {
            NotificationType = notificationType;
        }
        public Notification(int notificationId, string memberId, int groupId, int postId, string notificationType, int i)
        {
            NotificationId = notificationId;
            MemberId = memberId;
            GroupId = groupId;
            PostId = postId;
            NotificationType = notificationType;
        }
        public Notification(string notificationType, string memberId, int notificationId, int messageId)
        {
            NotificationId = notificationId;
            MemberId = memberId;
            MessageId = messageId;
            NotificationType = notificationType;
        }
        #region Properties
        public int NotificationId
        {
            get { return notificationId; }
            set { notificationId = value; }
        }
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
        public int MessageId
        {
            get { return messageId; }
            set { messageId = value; }
        }
        public int PostId
        {
            get { return postId; }
            set { postId = value; }
        }
        public int RatingId
        {
            get { return ratingId; }
            set { ratingId = value; }
        }
        public int CommentId
        {
            get { return commentId; }
            set { commentId = value; }
        }
        public string NotificationType
        {
            get { return notificationType; }
            set { notificationType = value; }
        }
        public bool IsRead
        {
            get { return isRead; }
            set { isRead = value; }
        }
        public int GroupId
        {
            get { return groupId; }
            set { groupId = value; }
        }
        public int ReportId
        {
            get { return reportId; }
            set { reportId = value; }
        }
        #endregion
    }
}
