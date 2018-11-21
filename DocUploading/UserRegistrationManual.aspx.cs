using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DocUploading.App_Start;
using Microsoft.SqlServer;
using System.Data.SqlClient;
using System.Data;

namespace DocUploading
{
    public partial class UserRegistrationManual : AppBaseClass
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string conString = AppClass.ConnectionString;
            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand("usp_users_entry_new", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FirstName", txtFirstName.Text);
                cmd.Parameters.AddWithValue("@LastName", txtLastName.Text);
                cmd.Parameters.AddWithValue("@Title", txtTitle.Text);
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@Phone", txtMobNumber.Text);
                cmd.Parameters.AddWithValue("@DirectPhone", txtPhoneNumber.Text);
                cmd.Parameters.AddWithValue("@Organization", txtMobNumber.Text);
                connection.Open();
                int i = cmd.ExecuteNonQuery();
                connection.Close();
            }
        }

    }
}
