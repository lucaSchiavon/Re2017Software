using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using AIChatbot.Classes;
using Ls.Prj.DTO;
using Ls.Prj.EFRepository;
using Ls.Prj.Entity;
using Ls.Prj.Utility;
using Prj.Utility.Base;

namespace AQuest.ChatBotGsk.PigeonCms.pgn_content.Contents
{
    public partial class ImportTags : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            PhldDanger.Visible = false;
            PhldPrimary.Visible = true;
        }

       
        protected void BtnSalva_Click(object sender, EventArgs e)
        {
            try
            {
                string filePath = HttpContext.Current.Server.MapPath(Utility.ReadSetting("LogPath"));
                Prj.Utility.Classes.FileLogger ObjFilLog = new Prj.Utility.Classes.FileLogger(filePath);

                

                byte[] fileData = null;
                Stream fileStream = null;
                int length = 0;

                if (FileUpload1.HasFile)
                {
                    length = FileUpload1.PostedFile.ContentLength;
                    fileData = new byte[length + 1];
                    fileStream = FileUpload1.PostedFile.InputStream;
                    fileStream.Read(fileData, 0, length);
                }

                // use your new byte array, if you want it in a memory stream then do this...
                MemoryStream stream = new MemoryStream(fileData);
                StreamReader reader = new StreamReader(stream);
                
                int TagCounter = 0;
                int TagImportErrCounter = 0;
                ObjFilLog.Log("Start to import" , Typology.Info);
               
                string headerLine = reader.ReadLine();
                while (!reader.EndOfStream)
                    {
                       
                        string CurrentRow = "";
                        try
                        {
                            //salta la prima riga (intestazione)
                            CurrentRow = reader.ReadLine();
                            string sep = "\t";
                            string[] Columns = CurrentRow.Split(sep.ToCharArray());
                            Tag CurrTag = new Tag();
                            CurrTag.TagName = Columns[0].ToString();
                            CurrTag.Machine = Columns[2].ToString();
                            CurrTag.Enabled = true;
                            CurrTag.Description = Columns[7].ToString();
                            CurrTag.Node = Columns[1].ToString();
                            CurrTag.Device = Columns[3].ToString();
                            CurrTag.ValueType = Columns[4].ToString();
                            CurrTag.IdTagValue = Convert.ToInt32(Columns[5]);
                            CurrTag.Alarm = Convert.ToInt32(Columns[6]);
                            InsertTag(CurrTag);
                            TagCounter += 1;
                        }
                        catch (Exception ex)
                        {
                            //scrive nel log come è andata l'importazione
                            ObjFilLog.Log("Tag not inserted: " + CurrentRow, Typology.Error);
                            TagImportErrCounter += 1;
                        }
                    
                }
                ObjFilLog.Log("Finish to import, " + TagCounter + " tags inserted; " + TagImportErrCounter + " tags not inserted", Typology.Info);
                HypLnkLog.Text = ObjFilLog.LogName;
                HypLnkLog.NavigateUrl = "/Public/Log/" + ObjFilLog.LogName;

                PhldDanger.Visible = false;
                PhldPrimary.Visible = true;
              
                #region upload con file fisico
                //try
                //{

                //        Boolean fileOK = false;
                //        String path = Server.MapPath("~/Public/FileUploaded/");
                //        if (FileUpload1.HasFile)
                //        {
                //            String fileExtension =
                //                System.IO.Path.GetExtension(FileUpload1.FileName).ToLower();
                //            String[] allowedExtensions =
                //                {".csv", ".txt"};
                //            for (int i = 0; i < allowedExtensions.Length; i++)
                //            {
                //                if (fileExtension == allowedExtensions[i])
                //                {
                //                    fileOK = true;
                //                }
                //            }
                //        }

                //        if (fileOK)
                //        {
                //            try
                //            {
                //            //fa l'upload del file
                //                FileUpload1.PostedFile.SaveAs(path
                //                    + FileUpload1.FileName);
                //            //importa i dati
                //            LitOk.Text = "Data succesfully imported!";
                //            }
                //            catch (Exception ex)
                //            {
                //            PrintError(ex);
                //        }
                //        }
                //        else
                //        {
                //        throw new Exception("Only .csv or .txt files can be accepted");
                //        }

                //}
                //catch (Exception ex)
                //{
                //    //qui segnalare errore su un pannello del form
                //    PrintError(ex);
                //}
                #endregion
            }
            catch (Exception ex)
            {
                PrintError(ex);
            }
        }

        //protected void BtnClose_Click(object sender, EventArgs e)
        //{
        //    //DivError.Attributes.Add("Class", "ParentDivDeleting Disattivato");

        //}

        #region routine private alla pagina
        public void PrintError(Exception ex)
        {

            LitError.Text = "Error occurred, please verify your file structure and datatype";
            PhldDanger.Visible = true;
            PhldPrimary.Visible = false;
        }

        public Tag InsertTag(Tag CurrTag)
        {
            Tag NewTag = new Tag();


            NewTag.TagName = CurrTag.TagName;
            NewTag.Machine = CurrTag.Machine;
            NewTag.Enabled = true;
            NewTag.Description = CurrTag.Description;
            NewTag.Node = CurrTag.Node;
            NewTag.Device = CurrTag.Device;
            NewTag.ValueType = CurrTag.ValueType;
            NewTag.IdTagValue = Convert.ToInt32(CurrTag.IdTagValue);
            NewTag.Alarm = Convert.ToInt32(CurrTag.Alarm);

            using (TagEFRepository TagRep = new TagEFRepository(""))
            {
                TagRep.Context.Tags.Add(NewTag);
                TagRep.Context.SaveChanges();
            }

            return NewTag;

        }

        #endregion

    }
}