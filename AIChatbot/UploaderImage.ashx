<%@ WebHandler Language="C#" Class="UploaderImage" %>

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

public class UploaderImage : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        try
        {
            context.Response.ContentType = "text/plain";
           
            if (context.Request.QueryString["filename"].ToString() != "")
            {

                string dirFullPath = HttpContext.Current.Server.MapPath("~/Public/Photos/Temp");
               
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
                       
                        /////// Image Path Get ///////

                        fileExtension = Path.GetExtension(fileName);
                     
                        str_image = guid + fileExtension; //+ fileExtension;

                        /////// Image Path Get End ///////

                        string pathToSave = HttpContext.Current.Server.MapPath("~/Public/Photos/Temp/") + str_image ;
                        file.SaveAs(pathToSave);

                        // img size  ////
                        System.Drawing.Image img = System.Drawing.Image.FromStream(file.InputStream);
                        int height = img.Height;
                        int width = img.Width;
                        decimal size = Math.Round(((decimal)file.ContentLength / (decimal)1024), 2);

                        // image size end   //

                    
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