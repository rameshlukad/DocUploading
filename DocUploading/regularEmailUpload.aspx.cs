using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls; 
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.OleDb;
using System.Data.Common;
using DocUploading.App_Start;
using Microsoft.SqlServer;
using System.IO;

namespace DocUploading
{
    public partial class regularEmailUpload : AppBaseClass
    {
        DateTime dateTimeNow = DateTime.Now;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            DateTime TimeStamp = DateTime.Now;
            String SessionId = "1";
            DataTable dt = new DataTable();
            dateTimeNow = DateTime.Now;

            dt.Columns.AddRange(new DataColumn[17] {
            new DataColumn("autoid", typeof(string)),
            new DataColumn("first_name", typeof(string)),
            new DataColumn("last_name", typeof(string)),
            new DataColumn("designation", typeof(string)),
            new DataColumn("email", typeof(string)),
            new DataColumn("phone", typeof(string)),
            new DataColumn("directphone", typeof(string)),
            new DataColumn("organization", typeof(string)),
            new DataColumn("address1", typeof(string)),
            new DataColumn("address2", typeof(string)),
            new DataColumn("city", typeof(string)),
            new DataColumn("state", typeof(string)),
            new DataColumn("country", typeof(string)),
            new DataColumn("created_stamp", typeof(DateTime)),
            new DataColumn("updated_stamp", typeof(DateTime)),
            new DataColumn("sessionId", typeof(string)),
            new DataColumn("current_datetime", typeof(string)),
            });



            if (FileUpload1.PostedFile != null)
            {
                string path = string.Concat(Server.MapPath("~/UploadFile/" + FileUpload1.FileName));
                FileUpload1.SaveAs(path);
                string excelCS = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=Excel 8.0", path);

                //string conString = string.Empty;
                //string extension = Path.GetExtension(FileUpload1.PostedFile.FileName);
                //switch (extension)
                //{
                //    case ".xls": //Excel 97-03
                //        conString = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                //        break;
                //    case ".xlsx": //Excel 07 or higher
                //        conString = ConfigurationManager.ConnectionStrings["Excel07+ConString"].ConnectionString;
                //        break;

                //}
                //conString = string.Format(conString, path);


                using (OleDbConnection con = new OleDbConnection(excelCS))
                {

                    OleDbCommand cmd = new OleDbCommand("select * from [Sheet1$]", con);
                    con.Open();
                    DbDataReader dr = cmd.ExecuteReader();
                    string conString = AppClass.ConnectionString;


                   // dt.Load(cmd.ExecuteReader());
                    SqlBulkCopy bulkInsert = new SqlBulkCopy(conString);

                    //cmd.Parameters.AddWithValue("@created_stamp", dateTimeNow);
                    //cmd.Parameters.AddWithValue("@updated_stamp", dateTimeNow);

                    bulkInsert.DestinationTableName = "tbl_temp_upload";

                    string conStrings = AppClass.ConnectionString;
                    using (SqlConnection conn1 = new SqlConnection(conStrings))
                    {
                        string sql = "INSERT INTO tbl_temp_upload (created_stamp,updated_stamp) values (@created_stamp, @updated_stamp)";
                        SqlCommand cmd2 = new SqlCommand(sql, conn1);
                        cmd2.Parameters.AddWithValue("@created_stamp", SqlDbType.DateTime).Value = dateTimeNow;
                        cmd2.Parameters.AddWithValue("@updated_stamp", SqlDbType.DateTime).Value = dateTimeNow;
                        conn1.Open();
                        cmd2.ExecuteNonQuery();
                        conn1.Close();
                    }

                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        bulkInsert.ColumnMappings.Add(i, i);
                    }

                    bulkInsert.WriteToServer(dr);

                    try
                    {
                        SqlConnection conn = new SqlConnection(conString);
                        SqlCommand cmdd = new SqlCommand("spi_emaildata1", conn);
                        cmdd.CommandType = CommandType.StoredProcedure;
                        cmdd.Parameters.AddWithValue("@created_stamp", dateTimeNow);
                        cmdd.Parameters.AddWithValue("@sessionId", 1);
                        conn.Open();
                        SqlDataReader dr1 = cmdd.ExecuteReader();
                        while (dr1.Read())
                        {
                            if (dr1.HasRows == true)
                            {
                                lblMessage.Text = "email = " + dr1[0].ToString() + " Already exist";

                                break;
                            }
                        }


                        conn.Close();
                        con.Close();

                        lblMessage.Text = "Your file uploaded successfully";
                        lblMessage.ForeColor = System.Drawing.Color.Green;
                        //Response.Redirect("DisplayData");
                    }


                    catch (Exception ex)
                    {
                        lblMessage.Text = "Your file not uploaded successfully" + (ex.Message);
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                    }

                }
            }

            else
            {
                lblMessage.Text = "Your file not uploaed";
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }

    }
}