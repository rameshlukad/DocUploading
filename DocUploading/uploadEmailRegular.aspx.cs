using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.Common;
using DocUploading.App_Start;
using Microsoft.SqlServer;
using System.IO;

namespace DocUploading
{
    public partial class uploadEmailRegular : System.Web.UI.Page
    {
        DateTime dateTimeNow = DateTime.Now;
        int sessionId = 1;
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        protected void insertdata_Click(object sender, EventArgs e)
        {
            if (FileUpload1.PostedFile != null)
            {
                //string path = string.Concat(HttpContext.Current.Server.MapPath("~/UploadFile/" + FileUpload1.FileName));
                //FileUpload1.SaveAs(path);
                //string excelCS = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=Excel 8.0", path);

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
                //    case ".csv":
                //        conString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;DataSource=" + Server.MapPath("~/UploadFile/" + FileUpload1.FileName) + ";Extended Properties=text;HDR=Yes;FMT=Delimited(,)");
                //        break;
                //}
                //conString = string.Format(conString, path);



 OleDbConnection oconn = new OleDbConnection
                  (@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Server.MapPath("~/UploadFile/" + FileUpload1.FileName) + "; Extended Properties = Excel 8.0");//OledbConnection and 

                //string conString = AppClass.ConnectionString;
                //using (OleDbConnection con = new OleDbConnection(conString))
                //{
                    try
                    {
                        OleDbCommand ocmd = new OleDbCommand("select * from [Sheet1$]", oconn);
                    oconn.Open();
                        OleDbDataReader odr = ocmd.ExecuteReader();
                        string first_name = "";
                        string last_name = "";
                        string designation = "";
                        string email = "";
                        string phone = "";
                        string directphone = "";
                        string organization = "";
                        string address1 = "";
                        string address2 = "";
                        string city = "";
                        string state = "";
                        string country = "";
                        string created_stamp = "";
                        string updated_stamp = "";
                        int sessionId = 1;
                        string current_datetime = "";

                        while (odr.Read())
                        {
                            first_name = valid(odr, 1);//Here we are calling the valid method
                            last_name = valid(odr, 2);
                            designation = valid(odr, 3);
                            email = valid(odr, 4);
                            phone = valid(odr, 5);
                            directphone = valid(odr, 6);
                            organization = valid(odr, 7);
                            address1 = valid(odr, 8);
                            address2 = valid(odr, 9);
                            city = valid(odr, 10);
                            state = valid(odr, 11);
                            country = valid(odr, 12);
                            created_stamp = valid(odr, 13);
                            updated_stamp = valid(odr, 14);
                            sessionId = Convert.ToInt32(valid(odr, 15));
                            current_datetime = valid(odr, 16);

                            //Here using this method we are inserting the data into the database
                            insertdataintosql(first_name, last_name, designation, email, phone, directphone, organization, address1, address2, city, state, country, created_stamp, updated_stamp, sessionId, current_datetime);


                            string conString1 = AppClass.ConnectionString;
                            SqlConnection conn = new SqlConnection(conString1);
                            SqlCommand cmdd = new SqlCommand("spi_emaildata1", conn);
                            cmdd.CommandType = CommandType.StoredProcedure;
                            cmdd.Parameters.AddWithValue("@created_stamp", dateTimeNow).Value = dateTimeNow;
                            cmdd.Parameters.AddWithValue("@sessionId", sessionId).Value = sessionId;
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
                            lblMessage.Text = "Your file uploaded successfully";
                            lblMessage.ForeColor = System.Drawing.Color.Green;

                        }

                    oconn.Close();
                    }

                    catch (DataException ee)
                    {
                        lblmsg.Text = ee.Message;
                        lblmsg.ForeColor = System.Drawing.Color.Red;
                    }
                    finally
                    {
                        lblmsg.Text = "Data Inserted Sucessfully";
                        lblmsg.ForeColor = System.Drawing.Color.Green;
                    }

                //}
            }
        }


        //This valid method is mainly used to check where the null values are 
        //contained in the Excel Sheet and replacing them with zero
        protected string valid(OleDbDataReader myreader, int stval)
        {
            object val = myreader[stval];
            if (val != DBNull.Value)
            {
                return val.ToString();
            }

            else
            {
                return Convert.ToString(1);
            }

        }


        public void insertdataintosql(string first_name, string last_name, string designation, string email, string phone, string directphone,
            string organization, string address1, string address2, string city, string state, string country, string created_stamp, string updated_stamp,
           int sessionId, string current_datetime)

        {//inserting data into the Sql Server
            string conString1 = AppClass.ConnectionString;
            SqlConnection conn = new SqlConnection(conString1);
        SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "insert into tbl_temp_upload(first_name, last_name, designation, email, phone, directphone, organization, address1, address2, city, state, country, created_stamp, updated_stamp, sessionId, current_datetime)" +
                " values(@first_name, @last_name, @designation, @email, @phone, @directphone, @organization, @address1, @address2, @city, @state, @country, @created_stamp, @updated_stamp, @sessionId, @current_datetime);";
            cmd.Parameters.Add("@first_name", SqlDbType.NVarChar).Value = first_name;
            cmd.Parameters.Add("@last_name", SqlDbType.NVarChar).Value = last_name;
            cmd.Parameters.Add("@designation", SqlDbType.NVarChar).Value = designation;
            cmd.Parameters.Add("@email", SqlDbType.NVarChar).Value = email;
            cmd.Parameters.Add("@phone", SqlDbType.NVarChar).Value = phone;
            cmd.Parameters.Add("@directphone", SqlDbType.NVarChar).Value = directphone;
            cmd.Parameters.Add("@organization", SqlDbType.NVarChar).Value = organization;
            cmd.Parameters.Add("@address1", SqlDbType.NVarChar).Value = address1;
            cmd.Parameters.Add("@address2", SqlDbType.NVarChar).Value = address2;
            cmd.Parameters.Add("@city", SqlDbType.NVarChar).Value = city;
            cmd.Parameters.Add("@state", SqlDbType.NVarChar).Value = state;
            cmd.Parameters.Add("@country", SqlDbType.NVarChar).Value = country;
            cmd.Parameters.Add("@created_stamp", SqlDbType.DateTime).Value = dateTimeNow;
            cmd.Parameters.Add("@updated_stamp", SqlDbType.DateTime).Value = dateTimeNow;
            cmd.Parameters.Add("@sessionId", SqlDbType.Int).Value = sessionId;
            cmd.Parameters.Add("@current_datetime", SqlDbType.DateTime).Value = dateTimeNow;

            cmd.CommandType = CommandType.Text;
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }
    }
}