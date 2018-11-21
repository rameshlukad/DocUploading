using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DocUploading.App_Start
{
    public class AppBaseClass : System.Web.UI.Page 
    {
        public AppBaseClass()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        protected override void OnPreInit(EventArgs e)
        {
            //if (HttpContext.Current.Session["UserId"] == null)
            //{
            //    HttpContext.Current.Response.Redirect("~/logout.aspx");
            //}
            //else if (HttpContext.Current.Session["UserId"].ToString() == string.Empty)
            //{
            //    HttpContext.Current.Response.Redirect("~/logout.aspx");
            //}

            base.OnPreInit(e);
        }
    }
}
 