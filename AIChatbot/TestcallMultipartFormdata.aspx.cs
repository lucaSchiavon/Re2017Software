using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Re2017
{
    public partial class TestcallMultipartFormdata : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public void Upload(object sourc, EventArgs e)
        {
            //http://2.235.241.7:8080/bank-report-entries/Checking1/upload
            int intFileNameLength;
            string strFileNamePath;
            string strFileNameOnly;
            strFileNamePath = myFile.PostedFile.FileName;
           // intFileNameLength = (1, StrReverse(strFileNamePath), @"\");
            //strFileNameOnly = Mid(strFileNamePath, (Len(strFileNamePath) - intFileNameLength) + 2)

   //         myFile.PostedFile.SaveAs(@"c:\inetpub\wwwroot\yourwebapp\upload\" + strFileNameOnly);
   //   lblMsg.Text = "File Upload Success."
   //lblFileContentType.Text = "Content type: " & MyFile.PostedFile.ContentType
   //lblFileSize.Text = "File size: " & CStr(MyFile.PostedFile.ContentLength) & " bytes"
        }
    }
}