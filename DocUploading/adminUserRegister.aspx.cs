using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using DocUploading.App_Start;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using System.Data.SqlTypes;
using System.Web.Security; 
using System.Web.UI.HtmlControls; 
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;  
using System.Net; 


namespace DocUploading
{
    public partial class adminUserRegister : AppBaseClass
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAdminUserRegister_Click(object sender, EventArgs e)
        {
            string conString = AppClass.ConnectionString;
            using (SqlConnection connection = new SqlConnection(conString))
            {

                SqlCommand cmd = new SqlCommand("usp_admin_user_register2", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserName", txtUserName.Text);
                cmd.Parameters.AddWithValue("@Email", entEmail.Text);
                cmd.Parameters.AddWithValue("@Password", Encrypt(entPassword.Text.Trim()));
                cmd.Parameters.AddWithValue("@ActiveStatus", 1);
                cmd.Parameters.AddWithValue("@CreatedDate", SqlDbType.DateTime).Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                cmd.Parameters.AddWithValue("@UpdatedDate", SqlDbType.DateTime).Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                connection.Open();
                int i = cmd.ExecuteNonQuery();
                connection.Close();
            }
        }

        private static string Encrypt(string Password)
        {
            string EncryptionKey = "abc!123";
            byte[] clearBytes = Encoding.Unicode.GetBytes(Password);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    Password = Convert.ToBase64String(ms.ToArray());
                }
            }
            return Password;
        }


    }
}