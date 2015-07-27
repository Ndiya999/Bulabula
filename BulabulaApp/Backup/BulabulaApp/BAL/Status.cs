using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BulabulaApp
{
    class Status
    {
        private int statusId;
        private bool isBlocked;

        public Status(int statusId, bool isBlocked)
        {
            StatusId = statusId;
            IsBlocked = isBlocked;
        }

        #region PROPERTIES

        public int StatusId
        {
            get { return statusId; }
            set { statusId = value; }
        }

        public bool IsBlocked
        {
            get { return isBlocked; }
            set { isBlocked = value; }
        }

        #endregion

    }
}
