using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Configuration;
using System.IO;
using iTextSharp.text.pdf;
using Prj.Utility.Classes;
using iTextSharp.text.pdf.parser;
using System.Text.RegularExpressions;

namespace Ls.Prj.Utility
{
    public static class Utility
    {
        public static DateTime DateParse(string date)
        {
            date = date.Trim();
            if (!string.IsNullOrEmpty(date))
                return DateTime.Parse(date, new System.Globalization.CultureInfo("en-GB"));
            return new DateTime();
        }
        public static void SetDropByValue(DropDownList drop, string value)
        {
            drop.SelectedIndex = drop.Items.IndexOf(drop.Items.FindByValue(value));
        }

        //public string GetAppSetting(string Key)
        //{
        //  string Value=  ConfigurationSettings.AppSettings[PrivateKey];
           
        //}
        

         public static string GetPrivSimKey(string key)
        {
            string Value = "";
            try
            {

                Value = System.Configuration.ConfigurationManager.AppSettings.Get(key)+ "parteCablata!";
            }
            catch
            {

            }
            return Value;

        }
        public static string ReadSetting(string key)
        {
            string Value = "";
            try
            {
                
                Value = System.Configuration.ConfigurationManager.AppSettings.Get(key);
            }
            catch 
            {

            }
            return Value;
          
        }

       
        public static List<Title> ReadPdfFile(string fileName)
        {
             List<Title> LstTitles;
                StringBuilder text = new StringBuilder();

                PdfReader pdfReader = new PdfReader(fileName);
                LstTitles = new List<Title>();
                for (int page = 1; page <= pdfReader.NumberOfPages; page++)
                {
                   
                    ITextExtractionStrategy strategy = new LocationTextExtractionStrategy();
                    string currentText = PdfTextExtractor.GetTextFromPage(pdfReader, page, strategy);
                  

                    currentText = Encoding.UTF8.GetString(ASCIIEncoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(currentText)));
                  
                    string RegTitoli = @"^([0-9\.]+)\s+([^\§]+)$";
                    string RegSoloNumeri = @"^([0-9\s]+)$";
                    string[] righe = currentText.Split(new string[] { "\n" }, StringSplitOptions.None);
                    foreach (string currRiga in righe)
                    {
                        string CurRigaTrimmed = currRiga.Trim();
                        MatchCollection mcTitoli = Regex.Matches(CurRigaTrimmed, RegTitoli);
                        foreach (Match m in mcTitoli)
                        {
                            Regex regexSoloNumeri = new Regex(RegSoloNumeri);
                            Match match2 = regexSoloNumeri.Match(CurRigaTrimmed);
                            if (!match2.Success)
                            {
                                string RegPuntini = @"[.]{7,10000}";
                                Regex regexPuntini = new Regex(RegPuntini);
                                Match matchPuntini = regexPuntini.Match(CurRigaTrimmed);
                                if (!matchPuntini.Success)
                                {
                                    //aggiunge solo se non sono righe di indice
                                    Title CurrTit = new Title();
                                    CurrTit.Riga = m.Value;
                                    CurrTit.Pagina = page;
                                    LstTitles.Add(CurrTit);
                                }


                              
                            }


                        }
                    }
                  
                }
                pdfReader.Close();
           
            return LstTitles;
        }
    }
}
