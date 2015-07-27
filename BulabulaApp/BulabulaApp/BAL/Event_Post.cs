using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BulabulaApp
{
	public class Event_Post
	{
        private int postId;
        private string eventName;
        private string eventDetails;
        private string eventVenue;
        private DateTime startDate;
        private DateTime endDate;
        private string host;
        private string eventType;

        public Event_Post(int postId, string eventName, string eventDetails, string eventVenue, DateTime startDate, DateTime endDate, string host, string eventType)
        {
            PostId = postId;
            EventName = eventName;
            EventDetails = eventDetails;
            EventVenue = eventVenue;
            StartDate = startDate;
            EndDate = endDate;
            Host = host;
            EventType = eventType;
        }
        public Event_Post(string eventName, string eventDetails, string eventVenue, DateTime startDate, DateTime endDate, string host, string eventType)
        {
            EventName = eventName;
            EventDetails = eventDetails;
            EventVenue = eventVenue;
            StartDate = startDate;
            EndDate = endDate;
            Host = host;
            EventType = eventType;
        }
        public Event_Post(string eventName)
        {
            EventName = eventName;
        }
        #region Properties

        public int PostId
        {
            get { return postId; }
            set { postId = value; }
        }

        public string EventName
        {
            get { return eventName; }
            set { eventName = value; }
        }

        public string EventDetails
        {
            get { return eventDetails; }
            set { eventDetails = value; }
        }

        public string EventVenue
        {
            get { return eventVenue; }
            set { eventVenue = value; }
        }

        public DateTime StartDate
        {
            get { return startDate; }
            set { startDate = value; }
        }

        public DateTime EndDate
        {
            get { return endDate; }
            set { endDate = value; }
        }

        public string Host
        {
            get { return host; }
            set { host = value; }
        }

        public string EventType
        {
            get { return eventType; }
            set { eventType = value; }
        }

        #endregion
	}
}