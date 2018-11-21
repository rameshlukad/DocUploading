using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using DocUploading.App_Start;

namespace DocUploading
{
    public partial class CSVupload : AppBaseClass
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnCSV_Click(object sender, EventArgs e)
        {
            string csvPath = Server.MapPath("~/Files/") + Path.GetFileName(FileUpload1.PostedFile.FileName);
            FileUpload1.SaveAs(csvPath);

            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[8] {
            new DataColumn("UserID", typeof(string)),
            new DataColumn("FirstName", typeof(string)),
            new DataColumn("LastName",typeof(string)),
            new DataColumn("Title", typeof(string)),
            new DataColumn("Email",typeof(string)),
            new DataColumn("Phone", typeof(string)),
            new DataColumn("DirectPhone",typeof(string)),
            new DataColumn("Organization", typeof(string))
            });


            string csvData = File.ReadAllText(csvPath);
            foreach (string row in csvData.Split('\n'))
            {
                if (!string.IsNullOrEmpty(row))
                {
                    dt.Rows.Add();
                    int i = 0;
                    foreach (string cell in row.Split(','))
                    {
                        dt.Rows[dt.Rows.Count - 1][i] = cell;
                        i++;
                    }

                }
            }

            string conString = AppClass.ConnectionString;
            using (SqlConnection con = new SqlConnection(conString))
            {
                try
                {
                    using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                    {
                        sqlBulkCopy.DestinationTableName = "dbo.tbl_Users";
                        con.Open();
                        sqlBulkCopy.WriteToServer(dt);
                        con.Close();
                    }

                    lblMessage.Text = "Your file uploaded successfully";
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                }
                catch (Exception)
                {
                    lblMessage.Text = "Your file not uploaded";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }

            }
        }
    }
}