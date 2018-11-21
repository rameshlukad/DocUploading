using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DocUploading.App_Start;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using System.Data.SqlTypes;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Net;
using System.Web.Security;
using System.Web.UI.HtmlControls; 

namespace DocUploading
{
    public partial class AdminLogin : AppBaseClass
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAdminLogin_Click(object sender, EventArgs e)
        {
            string conString = AppClass.ConnectionString;
            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand("usp_user_login_new", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Email", entEmail.Text);
                cmd.Parameters.AddWithValue("@Password", Decrypt(entPassword.Text.Trim()));
                connection.Open();
                SqlDataReader read = cmd.ExecuteReader();
                read.Read();
                if (read.HasRows)
                {
                    lblMsg.Text = "Successfull login!";
                    Response.Redirect("Dashboard.aspx");
                }
                else
                {
                    lblMsg.Text = "Wrong user/password";
                }
                connection.Close();
            }
        } 

        private static string Decrypt(string Password)
        {
            Password = Password.Replace('-', '+').Replace('_', '/').PadRight(4 * ((Password.Length + 3) / 4), '=');
            string EncryptionKey = "abc!123";
            
            byte[] DecodeUrlBase64 = Convert.FromBase64String(Password);

            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(DecodeUrlBase64, 0, DecodeUrlBase64.Length);
                        cs.Close();
                    }
                    Password = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return Password;
        }



    }
}
