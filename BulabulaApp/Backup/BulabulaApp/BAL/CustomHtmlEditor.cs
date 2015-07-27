using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BulabulaApp
{
    public class CustomHtmlEditor
    {
    }
    public class CustomEditor : AjaxControlToolkit.HTMLEditor.Editor
    {
        public CustomEditor()
        {
            //
            // TODO: Add constructor logic here
            //
        }


        protected override void FillTopToolbar()
        {
            //base.FillTopToolbar();

            AjaxControlToolkit.HTMLEditor.ToolbarButton.MethodButton btn = new AjaxControlToolkit.HTMLEditor.ToolbarButton.MethodButton();
            btn.NormalSrc = "aa.jpg";
            btn.Attributes.Add("onclick", "alert('a');");
            TopToolbar.Buttons.Add(btn);

        }
    }
}