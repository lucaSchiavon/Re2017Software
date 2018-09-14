<%@ WebHandler Language="C#" Class="Uploader" %>

using System;
using System.Web;
using System.IO;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using System.Data.SqlClient;

public class Uploader : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        try
        {
            context.Response.ContentType = "text/plain";
          


            if (context.Request.QueryString["filename"].ToString() != "")
            {

                string dirFullPath = HttpContext.Current.Server.MapPath("~/Public/Images/Temp");
                //string[] files;
                //int numFiles;

                string str_image = "";
                if ((context.Request.QueryString["filename"].ToString() != ""))
                {
                    string guid = context.Request.QueryString["filename"].ToString();
                    HttpFileCollection fil = context.Request.Files;
                    for (int s = 0; s < fil.Count; s++)
                    {
                        HttpPostedFile file = context.Request.Files[s];
                        string fileName = file.FileName;
                        string fileExtension = file.ContentType;
                        //files = System.IO.Directory.GetFiles(dirFullPath);
                        //numFiles = files.Length;
                        //numFiles = numFiles + 1;


                        /////// Image Path Get ///////

                        fileExtension = Path.GetExtension(fileName);
                        //str_image = username + "_" + numFiles.ToString() + fileExtension;
                        str_image = guid; //+ fileExtension;

                        /////// Image Path Get End ///////

                        string pathToSave = HttpContext.Current.Server.MapPath("~/Public/Images/Temp/") + str_image;
                        file.SaveAs(pathToSave);

                        // img size  ////
                        System.Drawing.Image img = System.Drawing.Image.FromStream(file.InputStream);
                        int height = img.Height;
                        int width = img.Width;
                        decimal size = Math.Round(((decimal)file.ContentLength / (decimal)1024), 2);

                        // image size end   //

                        //SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["dbcon"].ToString());
                        //con.Open();
                        //DateTime dt = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                        //string month = dt.Month.ToString() + "/" + dt.Year.ToString();
                        //string date = dt.Day.ToString() + "/" + dt.Month.ToString() + "/" + dt.Year.ToString();
                        //SqlCommand cmd = new SqlCommand("insert into imagereg(imgname,username,imgpath,imgheight,imgwidth,imgsize,date1,month1) values('" + str_image.ToString() + "','" + id.ToString() + "','" + "~/IMAGES/" + str_image.ToString() + "','"+height.ToString()+"','"+width.ToString()+"','"+size.ToString()+"','"+date.ToString()+"','"+month.ToString()+"')", con);
                        //cmd.ExecuteNonQuery();

                        context.Response.Write(str_image);

                    }

                }
            }
        }
        catch (Exception ex)
        {

            context.Response.Write("ERROR: "+ex.Message);
        }
    }




    public bool IsReusable {
        get {
            return false;
        }


    }



}