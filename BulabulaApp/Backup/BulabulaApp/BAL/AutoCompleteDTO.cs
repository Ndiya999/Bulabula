using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BulabulaApp
{
    public class AutoCompleteDTO
    {
       
            public string id { get; set; }
            public string label { get; set; }
            public string category { get; set; }

            public AutoCompleteDTO(string i, string l, string lt)
            {
                id = i;
                label = l;
                category = lt;
            }


            public AutoCompleteDTO(string i, string l)
            {
                id = i;
                label = l;
             }


     
    }
}