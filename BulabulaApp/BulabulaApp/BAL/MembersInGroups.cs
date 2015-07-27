using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BulabulaApp.BAL
{
    public class MembersInGroups
    {
           
            public string Quantity { get; set; }
            public string GroupDesc { get; set; }

            public MembersInGroups(string i, string l)
            {
                Quantity = i;
                GroupDesc = l;
              
            }

    }
}