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

namespace DocUploading
{
    public partial class regularEmailUploadXlsx : AppBaseClass
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
            if (FileUpload1.PostedFile != null)
            {
                string path = string.Concat(Server.MapPath("~/UploadFile/" + FileUpload1.FileName));
                FileUpload1.SaveAs(path);
                string excelCS = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=Excel 8.0", path);
                using (OleDbConnection con = new OleDbConnection(excelCS))
                {
                    OleDbCommand cmd = new OleDbCommand("select * from [Sheet1$]", con);
                    con.Open();
                    DbDataReader dr = cmd.ExecuteReader();
                    string conString = AppClass.ConnectionString;
                    SqlBulkCopy bulkInsert = new SqlBulkCopy(conString);
                    bulkInsert.DestinationTableName = "tbl_temp_upload";
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


        //protected void btnUpload_ClickTest(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        string conString = AppClass.ConnectionString;
        //        using (SqlConnection conn = new SqlConnection(conString))
        //        {
        //            using (SqlCommand cmdd = new SqlCommand("spi_emaildata1", conn))
        //            {
        //                cmdd.CommandType = CommandType.StoredProcedure;
        //                cmdd.Parameters.AddWithValue("@created_stamp", dateTimeNow);
        //                cmdd.Parameters.AddWithValue("@sessionId", 1);
        //                conn.Open();
        //                // cmdd.ExecuteNonQuery();


        //                SqlDataReader dr1 = cmdd.ExecuteReader();

        //                while (dr1.Read())
        //                {
        //                    if (dr1.HasRows == true)
        //                    {
        //                        lblMessage.Text = "email = " + dr1[0].ToString() + " Already exist";

        //                        break;
        //                    }
        //                }

        //                conn.Close();
        //                lblMessage.Text = "Your file uploaded successfully";
        //                lblMessage.ForeColor = System.Drawing.Color.Green;
        //                //Response.Redirect("DisplayData");

        //            }
        //        }

        //    }

        //    catch (Exception ex)
        //    {
        //        lblMessage.Text = "Your file not uploaded successfully" + (ex.Message);
        //        lblMessage.ForeColor = System.Drawing.Color.Red;
        //    }
        //}
    }
}