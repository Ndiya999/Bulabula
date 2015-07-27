using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BulabulaApp
{
    public class Membership
    {
        private string department;
        private string description;

        public Membership(string department, string description)
        {
            Department = department;
            Description = description;
        }
        public string Department
        {
            get { return department; }
            set { department = value; }
        }
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
    }
}