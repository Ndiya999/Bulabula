using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BulabulaApp
{
    class Administrator
    {
        private string administratorId;
        private DateTime adminSince;
        private string addedBy;
        private bool maxAuthority;
        private DateTime deleteDate;
        private string reasonDeleted;
        private string deletedBy;
        private bool stillAdmin;


        public Administrator(string administratorId, DateTime adminSince, string addedBy, bool maxAuthority, DateTime deleteDate, string reasonDeleted, string deletedBy, bool stillAdmin)
        {
            AdministratorId = administratorId;
            AdminSince = adminSince;
            AddedBy = addedBy;
            MaxAuthority = maxAuthority;
            DeleteDate = deleteDate;
            ReasonDeleted = reasonDeleted;
            DeletedBy = deletedBy;
            StillAdmin = stillAdmin;
        }
        public Administrator(string administratorId)
        {
            AdministratorId = administratorId;
        }

        public string AdministratorId
        {
            get { return administratorId; }
            set { administratorId = value; }
        }

        public DateTime AdminSince
        {
            get { return adminSince; }
            set { adminSince = value; }
        }

        public string AddedBy
        {
            get { return addedBy; }
            set { addedBy = value; }
        }

        public bool MaxAuthority
        {
            get { return maxAuthority; }
            set { maxAuthority = value; }
        }

        public DateTime DeleteDate
        {
            get { return deleteDate; }
            set { deleteDate = value; }
        }

        public string ReasonDeleted
        {
            get { return reasonDeleted; }
            set { reasonDeleted = value; }
        }

        public string DeletedBy
        {
            get { return deletedBy; }
            set { deletedBy = value; }
        }

        public bool StillAdmin
        {
            get { return stillAdmin; }
            set { stillAdmin = value; }
        }
    }
}
