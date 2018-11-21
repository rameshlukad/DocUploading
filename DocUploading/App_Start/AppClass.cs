using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;

namespace DocUploading.App_Start
{
    public class AppClass
    {
        public AppClass()
        { 
        }
        public static string ConnectionString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
        public static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["ConString"].ConnectionString.ToString();
        }
         
    } 
}