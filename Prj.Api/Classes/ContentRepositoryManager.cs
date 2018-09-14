using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ls.Prj.Entity;
using Ls.Prj.EFRepository;
using System.Data.SqlClient;
using AutoMapper;
using Ls.Prj.DTO;
using Ls.Prj.Api.DTO;
using AIChatbot.Api.Structure;
using System.Data;
using it.itryframework2.data;
using Ls.Prj.Utility;

namespace AIChatbot.Api.Classes
{
    public class ContentRepositoryPageManager
    {
        #region GetAlarms

      
        public ContainerDTO GetAlarms(LuisEntityAlarmDTO ObjLuisEntityAlarmDTO)
        {

            ContainerDTO ObjContainerDTO = new ContainerDTO();

            List<AlmHistoryDTO> LstGetAlarmDTO = GetLstAlarm(ObjLuisEntityAlarmDTO);
           


            ObjContainerDTO.payload = new dynamic[] { LstGetAlarmDTO };
            return ObjContainerDTO;
        }

        public List<AlmHistoryDTO> GetLstAlarm(LuisEntityAlarmDTO ObjLuisEntityAlarmDTO)
        {

            ContainerDTO ObjContainerDTO = new ContainerDTO();

            List<GetAlarmDTO> LstAlmDto = new List<GetAlarmDTO>();
            List<Tag> LstTag = new List<Tag>();
            List<AlmHistoryDTO> LstAlmHistoryDTO = new List<AlmHistoryDTO>();
            List<AlmHistoryDTO> LstAlmHistoryOutputDTO = new List<AlmHistoryDTO>();

            if ((ObjLuisEntityAlarmDTO.DispositivoValue != null) && (ObjLuisEntityAlarmDTO.TypeValue != null))
            {
                using (TagEFRepository Rep = new TagEFRepository(""))
                {

                    LstTag = Rep.Context.Tags.Where(x => x.Device == ObjLuisEntityAlarmDTO.DispositivoValue && x.Alarm == 1 && x.Description.Contains(ObjLuisEntityAlarmDTO.TypeValue)).ToList();

                }
            }
            if ((ObjLuisEntityAlarmDTO.DispositivoValue != null) && (ObjLuisEntityAlarmDTO.TypeValue == null))
            {
                using (TagEFRepository Rep = new TagEFRepository(""))
                {

                    LstTag = Rep.Context.Tags.Where(x => x.Device == ObjLuisEntityAlarmDTO.DispositivoValue && x.Alarm == 1 ).ToList();

                }
            }
            if ((ObjLuisEntityAlarmDTO.DispositivoValue == null) && (ObjLuisEntityAlarmDTO.TypeValue != null))
            {
                using (TagEFRepository Rep = new TagEFRepository(""))
                {

                    LstTag = Rep.Context.Tags.Where(x => x.Alarm == 1 && x.Description.Contains(ObjLuisEntityAlarmDTO.TypeValue)).ToList();

                }
            }
            if ((ObjLuisEntityAlarmDTO.DispositivoValue == null) && (ObjLuisEntityAlarmDTO.TypeValue == null))
            {

            }
            //una volta ottenuti i tags cerco  gli allarmi nell'audit
            it.itryframework2.data.DBMapper database = new it.itryframework2.data.DBMapper();
            foreach (Tag CurrTag in LstTag)
            {
                System.Data.DataTable Dt = database.select("select * from ALM_HISTORY where AH_TAGNAME='" + CurrTag.TagName + "'");

                foreach (DataRow Dr in Dt.Rows)
                {
                    AlmHistoryDTO ObjAlmHistoryDTO = new AlmHistoryDTO();
                    ObjAlmHistoryDTO.AH_NODE = Dr["AH_NODE"].ToString();
                    ObjAlmHistoryDTO.AH_DESCRIPTION = Dr["AH_DESCRIPTION"].ToString();
                    ObjAlmHistoryDTO.AH_PRIORITY = Dr["AH_PRIORITY"].ToString();
                    ObjAlmHistoryDTO.AH_DATEIN = Dr["AH_DATEIN"].ToString();
                    ObjAlmHistoryDTO.AH_DATELAST = Dr["AH_DATELAST"].ToString();
                    ObjAlmHistoryDTO.Device = CurrTag.Device;
                    LstAlmHistoryDTO.Add(ObjAlmHistoryDTO);
                 
                }


            }

            if ((ObjLuisEntityAlarmDTO.DispositivoValue != null) && (ObjLuisEntityAlarmDTO.TypeValue == null))
            {
                //solo allarmi attivi
                LstAlmHistoryOutputDTO = LstAlmHistoryDTO.Where(x => x.AH_DATELAST == "").ToList();
            }
            else if ((ObjLuisEntityAlarmDTO.DispositivoValue == null) && (ObjLuisEntityAlarmDTO.TypeValue != null))
            {
                LstAlmHistoryOutputDTO = LstAlmHistoryDTO.Where(x => x.AH_DATELAST == "").ToList();
            }
            else if ((ObjLuisEntityAlarmDTO.DispositivoValue != null) && (ObjLuisEntityAlarmDTO.TypeValue != null))
            {
                //raggruppa per tag e poi cicla sopra ogni tag
                //var carsByPersonId = LstAlmHistoryDTO.ToLookup(p => p.AH_DESCRIPTION, p => p.AH_DATEIN);

                //     var res=  LstAlmHistoryDTO.GroupBy(x => x.AH_DESCRIPTION)
                //.OrderBy(group => group.Key)
                //.Select(group => Tuple.Create(group.Key, group.Count()));

                var groups = LstAlmHistoryDTO
                   .GroupBy(n => n.AH_DESCRIPTION)
                   .Select(n => new
                   {
                       Description = n.Key,
                       Conteggio = n.Count()
                   }
                   ).ToList();

                List<AlmHistoryDTO> LstAlmHistoryOnlyLastForTagDTO = new List<AlmHistoryDTO>();
                foreach (var currDesc in groups)
                {
                    
                    List<AlmHistoryDTO> LstCurrGroupAlm = new List<AlmHistoryDTO>();
                    LstCurrGroupAlm= LstAlmHistoryDTO.Where(x => x.AH_DESCRIPTION == currDesc.Description).OrderByDescending(y => y.AH_DATEIN).ToList();
                    LstAlmHistoryOnlyLastForTagDTO.Add(LstCurrGroupAlm.FirstOrDefault());
                }

                //***************************
                //    //ad ogni cambiamento di tag recupera l'ultimo record
                //    List<AlmHistoryDTO> LstAlmHistoryWithSameTagDTO = new List<AlmHistoryDTO>();
                //List<AlmHistoryDTO> LstAlmHistoryOrderedDTO = new List<AlmHistoryDTO>();
               
                //LstAlmHistoryOrderedDTO = LstAlmHistoryDTO.OrderBy(x => x.AH_DATEIN == "").ToList();

                //string PreviousDesc = "";
                //foreach (AlmHistoryDTO CurrAlmHistoryDTO in LstAlmHistoryOrderedDTO)
                //{
                //    //per ogni record del set di record filtrato in audittrail
                //    if ((PreviousDesc=="") || (PreviousDesc!= CurrAlmHistoryDTO.AH_DESCRIPTION))
                //    {
                //        //recupera l'ultimo allarme intervenuto per ogni tipo di allarme
                //        List<AlmHistoryDTO> LstCurrAlmsWithSameDesc = new List<AlmHistoryDTO>();
                //        LstCurrAlmsWithSameDesc = LstAlmHistoryOrderedDTO.Where(x => x.AH_DESCRIPTION == CurrAlmHistoryDTO.AH_DESCRIPTION).OrderByDescending(y=>y.AH_DATEIN).ToList();
                //        LstAlmHistoryOnlyLastForTagDTO.Add(LstCurrAlmsWithSameDesc.FirstOrDefault());
                //        PreviousDesc = CurrAlmHistoryDTO.AH_DESCRIPTION;
                //    }

                   

                //}
                //********************
                LstAlmHistoryOutputDTO = LstAlmHistoryOnlyLastForTagDTO;
                //LstAlmHistoryWithSameTagDTO = LstAlmHistoryDTO.Where(x => x.AH_DESCRIPTION == "").ToList();
                //LstAlmHistoryOutputDTO = LstAlmHistoryDTO.Where(x => x.AH_DATELAST == "").ToList();
            }
                //if (ObjLuisEntityAlarmDTO.DispositivoValue != "")
                //{
                //    if (ObjLuisEntityAlarmDTO.TypeValue != "")
                //    {
                //        using (TagEFRepository Rep = new TagEFRepository(""))
                //        {

                //            LstTag = Rep.Context.Tags.Where(x => x.Device == ObjLuisEntityAlarmDTO.DispositivoValue && x.Alarm == 1 && x.Description.Contains(ObjLuisEntityAlarmDTO.TypeValue)).ToList();

                //        }
                //        //una volta ottenuti i tags cerco  gli allarmi nell'audit
                //        var database = new it.itryframework2.data.DBMapper();
                //        foreach (Tag CurrTag in LstTag)
                //        {
                //            System.Data.DataTable Dt = database.select("select * from ALM_HISTORY where AH_TAGNAME='" + CurrTag.TagName + "'");

                //            foreach (DataRow Dr in Dt.Rows)
                //            {
                //                AlmHistoryDTO ObjAlmHistoryDTO = new AlmHistoryDTO();
                //                ObjAlmHistoryDTO.AH_NODE = Dr["AH_NODE"].ToString();
                //                ObjAlmHistoryDTO.AH_DESCRIPTION = Dr["AH_DESCRIPTION"].ToString();
                //                ObjAlmHistoryDTO.AH_PRIORITY = Dr["AH_PRIORITY"].ToString();
                //                ObjAlmHistoryDTO.AH_DATEIN = Dr["AH_DATEIN"].ToString();
                //                ObjAlmHistoryDTO.AH_DATELAST = Dr["AH_DATELAST"].ToString();
                //                LstAlmHistoryDTO.Add(ObjAlmHistoryDTO);
                //            }


                //        }


                //    }
                //    else
                //    {
                //        //solo tipo valore

                //    }

                //}
                //else
                //{
                //    //solo tipo valore
                //    using (TagEFRepository Rep = new TagEFRepository(""))
                //    {

                //        LstTag = Rep.Context.Tags.Where(x => x.Device == ObjLuisEntityAlarmDTO.DispositivoValue && x.Alarm == 1).ToList();

                //    }
                //}
                ////mapping in dto qui...
                ////.ForMember(dest => dest.Enabled, opt => opt.MapFrom(src => (((bool)src.Enabled) ? "YES" : "NO")));
                //var config = new MapperConfiguration(cfg =>
                //{
                //    cfg.CreateMap<Tag, GetAlarmDTO>()
                //     .ForMember(dest => dest.TagValue, opt => opt.MapFrom(src => src.TagValue.TValue));
                //});

                //IMapper mapper = config.CreateMapper();
                //LstAlmDto = mapper.Map<List<Tag>, List<GetAlarmDTO>>(LstTag);

                return LstAlmHistoryOutputDTO;

        }
        #endregion



        #region GetAudit
        public ContainerDTO GetAudit(LuisEntityAuditDTO ObjLuisEntityAuditDTO)
        {

            ContainerDTO ObjContainerDTO = new ContainerDTO();

            List<RptAudittrailDTO> LstGetAuditDTO = GetLstAudit(ObjLuisEntityAuditDTO);

            ObjContainerDTO.payload = new dynamic[] { LstGetAuditDTO };
            return ObjContainerDTO;
        }
        public List<RptAudittrailDTO> GetLstAudit(LuisEntityAuditDTO ObjLuisEntityAuditDTO)
        {

            ContainerDTO ObjContainerDTO = new ContainerDTO();

            List<GetAlarmDTO> LstAlmDto = new List<GetAlarmDTO>();
            List<Tag> LstTag = new List<Tag>();
            List<RptAudittrailDTO> LstRptAudittrailDTO = new List<RptAudittrailDTO>();

            if ((ObjLuisEntityAuditDTO.DispositivoValue != null) && (ObjLuisEntityAuditDTO.TypeValue != null))
            {
                using (TagEFRepository Rep = new TagEFRepository(""))
                {

                    LstTag = Rep.Context.Tags.Where(x => x.Device == ObjLuisEntityAuditDTO.DispositivoValue && x.Alarm == 0 && x.Description.Contains(ObjLuisEntityAuditDTO.TypeValue)).ToList();

                }
            }
            if ((ObjLuisEntityAuditDTO.DispositivoValue != null) && (ObjLuisEntityAuditDTO.TypeValue == null))
            {
                using (TagEFRepository Rep = new TagEFRepository(""))
                {

                    LstTag = Rep.Context.Tags.Where(x => x.Device == ObjLuisEntityAuditDTO.DispositivoValue && x.Alarm == 0).ToList();

                }
            }
            if ((ObjLuisEntityAuditDTO.DispositivoValue == null) && (ObjLuisEntityAuditDTO.TypeValue != null))
            {
                using (TagEFRepository Rep = new TagEFRepository(""))
                {

                    LstTag = Rep.Context.Tags.Where(x => x.Alarm == 0 && x.Description.Contains(ObjLuisEntityAuditDTO.TypeValue)).ToList();

                }
            }
            if ((ObjLuisEntityAuditDTO.DispositivoValue == null) && (ObjLuisEntityAuditDTO.TypeValue == null))
            {

            }
            //una volta ottenuti i tags cerco  gli allarmi nell'audit
            it.itryframework2.data.DBMapper database = new it.itryframework2.data.DBMapper();
            foreach (Tag CurrTag in LstTag)
            {
                System.Data.DataTable Dt = database.select("select * from RPT_AUDITTRAIL where AT_TAG='" + CurrTag.TagName + "'");

                foreach (DataRow Dr in Dt.Rows)
                {
                    RptAudittrailDTO ObjRptAudittrailDTO = new RptAudittrailDTO();
                    ObjRptAudittrailDTO.AT_DATA = Dr["AT_Data"].ToString();
                    ObjRptAudittrailDTO.AT_Descr = Dr["AT_Descr"].ToString();
                    ObjRptAudittrailDTO.AT_OpName = Dr["AT_OpName"].ToString();
                    ObjRptAudittrailDTO.AT_STAZIONE = Dr["AT_Stazione"].ToString();
                    ObjRptAudittrailDTO.AT_SYSTEM = Dr["AT_System"].ToString();
                    LstRptAudittrailDTO.Add(ObjRptAudittrailDTO);
                }


            }


            return LstRptAudittrailDTO;

        }
        #endregion
       
        
        #region GetDocumentContents
        //viene chiamata dal client passando le entities specifiche in caso di intent documento / procedure
        public ContainerDTO GetDocumentContents(LuisEntityDocumentDTO ObjLuisEntityDocumentDTO)
        {

            ContainerDTO ObjContainerDTO = new ContainerDTO();

            List<GetDocGroupByChapDTO> LstGetDocumentContentsDTO = GetDocuments(ObjLuisEntityDocumentDTO);
            List<GetImagesDTO> LstGetImageContentsDTO = GetImages(ObjLuisEntityDocumentDTO);
            GetDocumentContentsDTO ObjGetDocumentContentsDTO = new GetDocumentContentsDTO();
            ObjGetDocumentContentsDTO.Documents = LstGetDocumentContentsDTO.ToArray();
            ObjGetDocumentContentsDTO.Images = LstGetImageContentsDTO.ToArray();

           
            ObjContainerDTO.payload = new dynamic[] { ObjGetDocumentContentsDTO };
            return ObjContainerDTO;
        }

        

        private DataTable GetDataTable(string Sql, LuisEntityDocumentDTO ObjLuisEntityDocumentDTO)
        {
            DataTable dt;
            using (DocumentEFRepository Rep = new DocumentEFRepository(""))
            {
                dt = new DataTable();
                var conn = Rep.Context.Database.Connection;
                var connectionState = conn.State;
                if (connectionState != ConnectionState.Open) conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = Sql ;
                    cmd.CommandType = CommandType.Text;

                    //cmd.Parameters.Add(new SqlParameter("ScopoDocumentoValue", ObjLuisEntityDocumentDTO.ScopoDocumentoValue));
                   
                    //cmd.Parameters.Add(new SqlParameter("DispositivoValue", ObjLuisEntityDocumentDTO.DispositivoValue));
                    //cmd.Parameters.Add(new SqlParameter("TagValue", ObjLuisEntityDocumentDTO.TagValue));
                    using (var reader = cmd.ExecuteReader())
                    {
                        dt.Load(reader);
                    }
                }
            }

            return dt;
        }
        public List<GetDocGroupByChapDTO> GetDocuments(LuisEntityDocumentDTO ObjLuisEntityDocumentDTO)
        {
            
            ContainerDTO ObjContainerDTO = new ContainerDTO();
            string Sql = "";
            if (ObjLuisEntityDocumentDTO.ScopoDocumentoValue != "" && ObjLuisEntityDocumentDTO.DispositivoValue != "" && ObjLuisEntityDocumentDTO.TagValue != "")
            {
                //ooo
                //scopo dispositivo tag ++
                Sql = "select sum(RANK) as attinenza,IdChapter,chaptername,chapargument,chapdevice,chapalias,pagenumber,iddocument,docname,docargument,docdevice,docnumer from [dbo].[GetTableOfResult]('" + ObjLuisEntityDocumentDTO.ScopoDocumentoValue + "','" + ObjLuisEntityDocumentDTO.DispositivoValue + "','" + ObjLuisEntityDocumentDTO.TagValue + "')";
            }
            else if (ObjLuisEntityDocumentDTO.ScopoDocumentoValue == "" && ObjLuisEntityDocumentDTO.DispositivoValue != "" && ObjLuisEntityDocumentDTO.TagValue != "")
            {
                //xoo
                //dispositivo tag  ++
                Sql = "select sum(RANK) as attinenza,IdChapter,chaptername,chapargument,chapdevice,chapalias,pagenumber,iddocument,docname,docargument,docdevice,docnumer from [dbo].[GetTableOfResultForDeviceTag]('" + ObjLuisEntityDocumentDTO.DispositivoValue + "','" + ObjLuisEntityDocumentDTO.TagValue + "')";
            }
            else if (ObjLuisEntityDocumentDTO.ScopoDocumentoValue == "" && ObjLuisEntityDocumentDTO.DispositivoValue == "" && ObjLuisEntityDocumentDTO.TagValue != "")
            {
                //xxo
                //tag ++
                Sql = "select sum(RANK) as attinenza,IdChapter,chaptername,chapargument,chapdevice,chapalias,pagenumber,iddocument,docname,docargument,docdevice,docnumer from [dbo].[GetTableOfResultForTag]('" + ObjLuisEntityDocumentDTO.TagValue + "')";
            }
            else if (ObjLuisEntityDocumentDTO.ScopoDocumentoValue != "" && ObjLuisEntityDocumentDTO.DispositivoValue == "" && ObjLuisEntityDocumentDTO.TagValue != "")
            {
                //oxo
                //scopo tag ++
                Sql = "select sum(RANK) as attinenza,IdChapter,chaptername,chapargument,chapdevice,chapalias,pagenumber,iddocument,docname,docargument,docdevice,docnumer from [dbo].[GetTableOfResultForScopeTag]('" + ObjLuisEntityDocumentDTO.ScopoDocumentoValue + "','" + ObjLuisEntityDocumentDTO.TagValue + "')";
            }
            else if (ObjLuisEntityDocumentDTO.ScopoDocumentoValue == "" && ObjLuisEntityDocumentDTO.DispositivoValue == "" && ObjLuisEntityDocumentDTO.TagValue == "")
            {
                //xxx
                //qui non dovrebbe cadere mai, gestita da alex
            }
            else if (ObjLuisEntityDocumentDTO.ScopoDocumentoValue != "" && ObjLuisEntityDocumentDTO.DispositivoValue == "" && ObjLuisEntityDocumentDTO.TagValue == "")
            {
                //oxx
                //scopo ++
                Sql = "select sum(RANK) as attinenza,IdChapter,chaptername,chapargument,chapdevice,chapalias,pagenumber,iddocument,docname,docargument,docdevice,docnumer from [dbo].[GetTableOfResultForScope]('" + ObjLuisEntityDocumentDTO.ScopoDocumentoValue  + "')";
            }
            else if (ObjLuisEntityDocumentDTO.ScopoDocumentoValue != "" && ObjLuisEntityDocumentDTO.DispositivoValue != "" && ObjLuisEntityDocumentDTO.TagValue == "")
            {
                //oox
                //scopo ++
                Sql = "select sum(RANK) as attinenza,IdChapter,chaptername,chapargument,chapdevice,chapalias,pagenumber,iddocument,docname,docargument,docdevice,docnumer from [dbo].[GetTableOfResultForScopeDevice]('" + ObjLuisEntityDocumentDTO.DispositivoValue + "','" + ObjLuisEntityDocumentDTO.ScopoDocumentoValue + "')";
            }
             else if (ObjLuisEntityDocumentDTO.ScopoDocumentoValue == "" && ObjLuisEntityDocumentDTO.DispositivoValue != "" && ObjLuisEntityDocumentDTO.TagValue == "")
            {
                //xox
                //scopo ++
                Sql = "select sum(RANK) as attinenza,IdChapter,chaptername,chapargument,chapdevice,chapalias,pagenumber,iddocument,docname,docargument,docdevice,docnumer from [dbo].[GetTableOfResultForDevice]('" + ObjLuisEntityDocumentDTO.DispositivoValue + "')";
            }
            //string Sql = "select sum(RANK) as attinenza,IdChapter,chaptername,chapargument,chapdevice,pagenumber,iddocument,docname,docargument,docdevice,docnumer from [dbo].[GetTableOfResult]('@ScopoDocumentoValue','@DispositivoValue','@TagValue')";
            Sql += " group by Idchapter,chaptername,chapargument,chapdevice,chapalias,pagenumber,iddocument,docname,docargument,docdevice,docnumer  order by sum(RANK) desc";

            DataTable DtDocumentsForScopoValue = GetDataTable(Sql, ObjLuisEntityDocumentDTO);
            List<GetDocumentsDTO> LstGetDocumentsDTO=new List<GetDocumentsDTO>();
            if (DtDocumentsForScopoValue.Rows.Count > 0)
            {
                DataRow[] Drows;
                List<DataRow> DrowsFilteredForTipology=new List<DataRow>();

                if (ObjLuisEntityDocumentDTO.DispositivoValue != "")
                {
                    Drows = DtDocumentsForScopoValue.Select("chapdevice='" + ObjLuisEntityDocumentDTO.DispositivoValue + "' or chapalias like '%" + ObjLuisEntityDocumentDTO.DispositivoValue + "%'");
                }
                else
                {
                    Drows = DtDocumentsForScopoValue.Select();
                }

                if (ObjLuisEntityDocumentDTO.TipoDocumentoValue != "")
                {

                    foreach (DataRow Dr in Drows)
                    {
                        using (DocumentEFRepository Rep = new DocumentEFRepository(""))
                        {
                            Document Doc = Rep.Context.Documents.Where(x => x.Type.Typology == ObjLuisEntityDocumentDTO.TipoDocumentoValue).FirstOrDefault();

                            if (Doc != null)
                            {
                                DrowsFilteredForTipology.Add(Dr);
                            }
                        }
                    }
                }
                else
                {
                    foreach (DataRow Dr in Drows)
                    {
                        using (DocumentEFRepository Rep = new DocumentEFRepository(""))
                        {
                           
                                DrowsFilteredForTipology.Add(Dr);
                            
                        }
                    }
                }

                int i = 1;
                foreach (DataRow Dr in DrowsFilteredForTipology)
                {
                    //ritorna solo i top 3 results
                    if (i < 4)
                    { 
                    GetDocumentsDTO ObjGetDocumentsDTO = new GetDocumentsDTO();
                    ObjGetDocumentsDTO.DocName = Dr["docname"].ToString();
                    ObjGetDocumentsDTO.ChapterName = Dr["chaptername"].ToString();
                    ObjGetDocumentsDTO.PageNumber = Dr["pagenumber"].ToString();
                    //ObjGetDocumentsDTO.UrlDoc =Utility.ReadSetting("UrlAdminApp") + "/Public/Images/" +  Dr["docname"].ToString();
                    LstGetDocumentsDTO.Add(ObjGetDocumentsDTO);
                    }
                    i += 1;
                }
                //var results = from d in LstGetDocumentsDTO
                //              group d.ChapterName by d.DocName;
                //var results = from d in LstGetDocumentsDTO
                //              group d.ChapterName by d.DocName into g
                //              select new { DocName = g.Key, Chapters = g.ToList() };

                var results = from d in LstGetDocumentsDTO
                              group new { ChapName = d.ChapterName, PagNumber = d.PageNumber } by d.DocName into g
                              select new { DocName = g.Key, Chapters = g.ToList() };



                // List<GetDocGroupByChapDTO> LstGetDocGroupByChapDTO = results.ToList<GetDocGroupByChapDTO>();
                //<GetDocGroupByChapDTO> LstGetDocGroupByChapDTO = results.ToList();

                var LstGetDocGroupByChapDTO = results.ToList();

                //map results sul tipizzato LstDoc
                List<GetDocGroupByChapDTO> LstDoc = new List<GetDocGroupByChapDTO>();
                foreach (var item in LstGetDocGroupByChapDTO)
                {
                    GetDocGroupByChapDTO ObjGetDocGroupByChapDTO = new GetDocGroupByChapDTO();
                    ObjGetDocGroupByChapDTO.DocName = item.DocName;

                    Document Doc;
                    using (DocumentEFRepository Rep = new DocumentEFRepository(""))
                    {
                        Doc = Rep.Context.Documents.Where(x => x.DocName == ObjGetDocGroupByChapDTO.DocName).FirstOrDefault(); 
                    }
                    ObjGetDocGroupByChapDTO.DocUrl = Utility.ReadSetting("UrlAdminApp") + "/Public/Images/" + Doc.PDFName;
                    //List<ChapDTO> LstChap = new List<ChapDTO>();
                    foreach (var Chapt in item.Chapters)
                    {
                        ChapDTO ObjChapDTO = new ChapDTO();
                        ObjChapDTO.ChapName = Chapt.ChapName;
                        ObjChapDTO.PagNumber = Chapt.PagNumber;
                        ObjGetDocGroupByChapDTO.Chapters.Add(ObjChapDTO);
                    }
                  
                    LstDoc.Add(ObjGetDocGroupByChapDTO);
                }
                    return LstDoc;
            }
            else
            {
                //var results = from d in LstGetDocumentsDTO
                //              group d.ChapterName by d.DocName into g
                //              select new { PersonId = g.Key, Chapters = g.ToList() };
                var results = from d in LstGetDocumentsDTO
                              group new { ChapName = d.ChapterName, PagNumber = d.PageNumber } by d.DocName into g
                              select new { DocName = g.Key, Chapters = g.ToList() };

                var LstGetDocGroupByChapDTO = results.ToList();

                //map results sul tipizzato LstDoc
                List<GetDocGroupByChapDTO> LstDoc = new List<GetDocGroupByChapDTO>();
                foreach (var item in LstGetDocGroupByChapDTO)
                {
                    GetDocGroupByChapDTO ObjGetDocGroupByChapDTO = new GetDocGroupByChapDTO();
                    ObjGetDocGroupByChapDTO.DocName = item.DocName;
                    foreach (var Chapt in item.Chapters)
                    {
                        ChapDTO ObjChapDTO = new ChapDTO();
                        ObjChapDTO.ChapName = Chapt.ChapName;
                        ObjChapDTO.PagNumber = Chapt.PagNumber;
                        ObjGetDocGroupByChapDTO.Chapters.Add(ObjChapDTO);
                    }
                    LstDoc.Add(ObjGetDocGroupByChapDTO);
                }
                //errore la ricerca non ha prodotto risultati
                return LstDoc;
                //throw new Exception("Error, search has produced 0 result");
               
            }

            
            
        }

        public List<GetImagesDTO> GetImages(LuisEntityDocumentDTO ObjLuisEntityDocumentDTO)
        {

            ContainerDTO ObjContainerDTO = new ContainerDTO();
            string Sql = "";
            if (ObjLuisEntityDocumentDTO.ScopoDocumentoValue != "" && ObjLuisEntityDocumentDTO.DispositivoValue != "" && ObjLuisEntityDocumentDTO.TagValue != "")
            {
                //scopo dispositivo tag ++
                //select sum(RANK) as attinenza,IdImage,ImageName,[Description],[Tags],Argument,Device from [dbo].[GetTableOfResultForImg]('Sala acque','Sala acque','Sala acque')  group by IdImage,ImageName,[Description],[Tags],Argument,Device  order by sum(RANK) desc
                //Sql = "select sum(RANK) as attinenza,IdChapter,chaptername,chapargument,chapdevice,pagenumber,iddocument,docname,docargument,docdevice,docnumer from [dbo].[GetTableOfResult]('" + ObjLuisEntityDocumentDTO.ScopoDocumentoValue + "','" + ObjLuisEntityDocumentDTO.DispositivoValue + "','" + ObjLuisEntityDocumentDTO.TagValue + "')";
                Sql = "select sum(RANK) as attinenza,IdImage,ImageName,[Description],[Tags],Argument,Device,Alias from [dbo].[GetTableOfResult_Img]('" + ObjLuisEntityDocumentDTO.ScopoDocumentoValue + "','" + ObjLuisEntityDocumentDTO.DispositivoValue + "','" + ObjLuisEntityDocumentDTO.TagValue + "')";
            }
            else if (ObjLuisEntityDocumentDTO.ScopoDocumentoValue == "" && ObjLuisEntityDocumentDTO.DispositivoValue != "" && ObjLuisEntityDocumentDTO.TagValue != "")
            {
                //dispositivo tag  ++
                Sql = "select sum(RANK) as attinenza,IdImage,ImageName,[Description],[Tags],Argument,Device,Alias from [dbo].[GetTableOfResult_Img_ForDeviceTag]('" + ObjLuisEntityDocumentDTO.DispositivoValue + "','" + ObjLuisEntityDocumentDTO.TagValue + "')";
            }
            else if (ObjLuisEntityDocumentDTO.ScopoDocumentoValue == "" && ObjLuisEntityDocumentDTO.DispositivoValue == "" && ObjLuisEntityDocumentDTO.TagValue != "")
            {
                //tag ++
                Sql = "select sum(RANK) as attinenza,IdImage,ImageName,[Description],[Tags],Argument,Device,Alias from [dbo].[GetTableOfResult_Img_ForTag]('" + ObjLuisEntityDocumentDTO.TagValue + "')";
            }
            else if (ObjLuisEntityDocumentDTO.ScopoDocumentoValue != "" && ObjLuisEntityDocumentDTO.DispositivoValue == "" && ObjLuisEntityDocumentDTO.TagValue != "")
            {
                //scopo tag ++
                Sql = "select sum(RANK) as attinenza,IdImage,ImageName,[Description],[Tags],Argument,Device,Alias from [dbo].[GetTableOfResult_Img_ForScopeTag]('" + ObjLuisEntityDocumentDTO.ScopoDocumentoValue + "','" + ObjLuisEntityDocumentDTO.TagValue + "')";
            }
            else if (ObjLuisEntityDocumentDTO.ScopoDocumentoValue == "" && ObjLuisEntityDocumentDTO.DispositivoValue == "" && ObjLuisEntityDocumentDTO.TagValue == "")
            {
                //qui non dovrebbe cadere mai, gestita da alex
            }
            else if (ObjLuisEntityDocumentDTO.ScopoDocumentoValue != "" && ObjLuisEntityDocumentDTO.DispositivoValue == "" && ObjLuisEntityDocumentDTO.TagValue == "")
            {
                //scopo ++......
                Sql = "select sum(RANK) as attinenza,IdImage,ImageName,[Description],[Tags],Argument,Device,Alias from [dbo].[GetTableOfResult_Img_ForScope]('" + ObjLuisEntityDocumentDTO.ScopoDocumentoValue + "')";
            }
            else if (ObjLuisEntityDocumentDTO.ScopoDocumentoValue != "" && ObjLuisEntityDocumentDTO.DispositivoValue != "" && ObjLuisEntityDocumentDTO.TagValue == "")
            {
                //oox
                //scopo ++ da aggiungere
                Sql = "select sum(RANK) as attinenza,IdImage,ImageName,[Description],[Tags],Argument,Device,Alias from [dbo].[GetTableOfResult_Img_ForScopeDevice]('" + ObjLuisEntityDocumentDTO.DispositivoValue + "','" + ObjLuisEntityDocumentDTO.ScopoDocumentoValue + "')";
            }
            else if (ObjLuisEntityDocumentDTO.ScopoDocumentoValue == "" && ObjLuisEntityDocumentDTO.DispositivoValue != "" && ObjLuisEntityDocumentDTO.TagValue == "")
            {
                //xox da aggiungere
                //scopo ++
                Sql = "select sum(RANK)  as attinenza,IdImage,ImageName,[Description],[Tags],Argument,Device,Alias from [dbo].[GetTableOfResult_Img_ForDevice]('" + ObjLuisEntityDocumentDTO.DispositivoValue + "')";
            }
            //string Sql = "select sum(RANK) as attinenza,IdChapter,chaptername,chapargument,chapdevice,pagenumber,iddocument,docname,docargument,docdevice,docnumer from [dbo].[GetTableOfResult]('@ScopoDocumentoValue','@DispositivoValue','@TagValue')";
            Sql += "  group by IdImage,ImageName,[Description],[Tags],Argument,Device,Alias  order by sum(RANK) desc";

            DataTable DtImagesForScopoValue = GetDataTable(Sql, ObjLuisEntityDocumentDTO);
            List<GetImagesDTO> LstGetImagesDTO = new List<GetImagesDTO>();
            if (DtImagesForScopoValue.Rows.Count > 0)
            {
                DataRow[] Drows;
                List<DataRow> DrowsFilteredForTipology = new List<DataRow>();

                if (ObjLuisEntityDocumentDTO.DispositivoValue != "")
                {
                    Drows = DtImagesForScopoValue.Select("Device='" + ObjLuisEntityDocumentDTO.DispositivoValue + "' or alias like '%" + ObjLuisEntityDocumentDTO.DispositivoValue + "%'");
                }
                else
                {
                    Drows = DtImagesForScopoValue.Select();
                }

                //if (ObjLuisEntityDocumentDTO.TipoDocumentoValue != "")
                //{

                //    foreach (DataRow Dr in Drows)
                //    {
                //        using (DocumentEFRepository Rep = new DocumentEFRepository(""))
                //        {
                //            Document Doc = Rep.Context.Documents.Where(x => x.Type.Typology == ObjLuisEntityDocumentDTO.TipoDocumentoValue).FirstOrDefault();

                //            if (Doc != null)
                //            {
                //                DrowsFilteredForTipology.Add(Dr);
                //            }
                //        }
                //    }
                //}
                //else
                //{
                //    foreach (DataRow Dr in Drows)
                //    {
                //        using (DocumentEFRepository Rep = new DocumentEFRepository(""))
                //        {

                //            DrowsFilteredForTipology.Add(Dr);

                //        }
                //    }
                //}

                //int i = 1;
                //foreach (DataRow Dr in DrowsFilteredForTipology)
                //{
                //    //ritorna solo i top 3 results
                //    if (i < 4)
                //    {
                //        GetDocumentsDTO ObjGetDocumentsDTO = new GetDocumentsDTO();
                //        ObjGetDocumentsDTO.DocName = Dr["docname"].ToString();
                //        ObjGetDocumentsDTO.ChapterName = Dr["chaptername"].ToString();
                //        ObjGetDocumentsDTO.PageNumber = Dr["pagenumber"].ToString();
                //        ObjGetDocumentsDTO.UrlDoc = Dr["docname"].ToString();
                //        LstGetDocumentsDTO.Add(ObjGetDocumentsDTO);
                //    }
                //    i += 1;
                //}

                int i = 1;
                foreach (DataRow Dr in Drows)
                {
                    //ritorna solo i top 3 results
                    if (i < 4)
                    {
                        GetImagesDTO ObjGetImgDTO = new GetImagesDTO();
                        ObjGetImgDTO.ImgName = Dr["ImageName"].ToString();
                        ObjGetImgDTO.Description = Dr["Description"].ToString();
                        ObjGetImgDTO.UrlImg = Utility.ReadSetting("UrlAdminApp") + "/Public/Photos/" + Dr["ImageName"].ToString();
                    
                        LstGetImagesDTO.Add(ObjGetImgDTO);
                    }
                    i += 1;
                }


                //var results = from d in LstGetDocumentsDTO
                //              group new { ChapName = d.ChapterName, PagNumber = d.PageNumber } by d.DocName into g
                //              select new { DocName = g.Key, Chapters = g.ToList() };



              
            //    var LstGetDocGroupByChapDTO = results.ToList();

            //    //map results sul tipizzato LstDoc
            //    List<GetDocGroupByChapDTO> LstDoc = new List<GetDocGroupByChapDTO>();
            //    foreach (var item in LstGetDocGroupByChapDTO)
            //    {
            //        GetDocGroupByChapDTO ObjGetDocGroupByChapDTO = new GetDocGroupByChapDTO();
            //        ObjGetDocGroupByChapDTO.DocName = item.DocName;
            //        //List<ChapDTO> LstChap = new List<ChapDTO>();
            //        foreach (var Chapt in item.Chapters)
            //        {
            //            ChapDTO ObjChapDTO = new ChapDTO();
            //            ObjChapDTO.ChapName = Chapt.ChapName;
            //            ObjChapDTO.PagNumber = Chapt.PagNumber;
            //            ObjGetDocGroupByChapDTO.Chapters.Add(ObjChapDTO);
            //        }

            //        LstDoc.Add(ObjGetDocGroupByChapDTO);
            //    }
            //    return LstDoc;
            //}
            //else
            //{
            //    //var results = from d in LstGetDocumentsDTO
            //    //              group d.ChapterName by d.DocName into g
            //    //              select new { PersonId = g.Key, Chapters = g.ToList() };
            //    var results = from d in LstGetDocumentsDTO
            //                  group new { ChapName = d.ChapterName, PagNumber = d.PageNumber } by d.DocName into g
            //                  select new { DocName = g.Key, Chapters = g.ToList() };

            //    var LstGetDocGroupByChapDTO = results.ToList();

            //    //map results sul tipizzato LstDoc
            //    List<GetDocGroupByChapDTO> LstDoc = new List<GetDocGroupByChapDTO>();
            //    foreach (var item in LstGetDocGroupByChapDTO)
            //    {
            //        GetDocGroupByChapDTO ObjGetDocGroupByChapDTO = new GetDocGroupByChapDTO();
            //        ObjGetDocGroupByChapDTO.DocName = item.DocName;
            //        foreach (var Chapt in item.Chapters)
            //        {
            //            ChapDTO ObjChapDTO = new ChapDTO();
            //            ObjChapDTO.ChapName = Chapt.ChapName;
            //            ObjChapDTO.PagNumber = Chapt.PagNumber;
            //            ObjGetDocGroupByChapDTO.Chapters.Add(ObjChapDTO);
            //        }
            //        LstDoc.Add(ObjGetDocGroupByChapDTO);
            //    }
                //errore la ricerca non ha prodotto risultati
              
                //throw new Exception("Error, search has produced 0 result");

            }
            return LstGetImagesDTO;


        }
        #endregion


        #region GetAlarmContents
        //viene chiamata dal client passando le entities specifiche in caso di intent Allarme


        #endregion


        #region Codice vecchio
        //viene chiamata dal client passando le entities specifiche in caso di intent documento / procedure
        //public ContainerDTO GetDocumentContents(LuisEntityDocumentDTO ObjLuisEntityDocumentDTO)
        //{

        //    ContainerDTO ObjContainerDTO = new ContainerDTO();

        //    List<GetDocumentsDTO> LstGetDocumentContentsDTO = GetDocuments(ObjLuisEntityDocumentDTO);
        //    List<GetImagesDTO> LstGetImageContentsDTO = GetImages(ObjLuisEntityDocumentDTO);
        //    GetDocumentContentsDTO ObjGetDocumentContentsDTO = new GetDocumentContentsDTO();
        //    ObjGetDocumentContentsDTO.Documents = LstGetDocumentContentsDTO.ToArray();
        //    ObjGetDocumentContentsDTO.Images = LstGetImageContentsDTO.ToArray();

        //    //ObjContainerDTO.payload = LstGetDocumentContentsDTO.ToArray();
        //    ObjContainerDTO.payload = new dynamic[] { ObjGetDocumentContentsDTO };
        //    return ObjContainerDTO;
        //}

        //public List<GetImagesDTO> GetImagesOLD1(LuisEntityDocumentDTO ObjLuisEntityDocumentDTO)
        //{

        //    ContainerDTO ObjContainerDTO = new ContainerDTO();
        //    string Sql = "select top 5 ImageName,Description,* from image ";
        //    string SqlWhere = " where 1=1 ";





        //    if (ObjLuisEntityDocumentDTO.DispositivoValue != "" || ObjLuisEntityDocumentDTO.TagValue != "")
        //    {

        //        if (ObjLuisEntityDocumentDTO.ScopoDocumentoValue != "")
        //        {
        //            SqlWhere += " and ";
        //            //siamo nel caso mostrami documento[TipoDocumento] di conformità[Scopo documento] dell'impianto
        //            SqlWhere += "(freetext(ImageName,'" + ObjLuisEntityDocumentDTO.ScopoDocumentoValue + "') or freetext([Description],'" + ObjLuisEntityDocumentDTO.ScopoDocumentoValue + "') or freetext(Tags,'" + ObjLuisEntityDocumentDTO.ScopoDocumentoValue + "') or freetext(Argument,'" + ObjLuisEntityDocumentDTO.ScopoDocumentoValue + "') or freetext(Device,'" + ObjLuisEntityDocumentDTO.ScopoDocumentoValue + "'))";
        //            //SqlWhere += "(contains(Chapter.ChapterName,'formsof(INFLECTIONAL,' + @ScopoDocumentoValue + ')') or contains(Chapter.Argument,'formsof(INFLECTIONAL, ' + @ScopoDocumentoValue + ')') or contains(Chapter.Device,'formsof(INFLECTIONAL, ' + @ScopoDocumentoValue + ')'))";
        //        }

        //        SqlWhere += " or (contains(ImageName,'NEAR(\"" + ObjLuisEntityDocumentDTO.DispositivoValue + "\", \"" + ObjLuisEntityDocumentDTO.TagValue + "\")') or contains(description,'NEAR(\"" + ObjLuisEntityDocumentDTO.DispositivoValue + "\", \"" + ObjLuisEntityDocumentDTO.TagValue + "\")') or contains(Tags,'NEAR(\"" + ObjLuisEntityDocumentDTO.DispositivoValue + "\", \"" + ObjLuisEntityDocumentDTO.TagValue + "\")') or contains(Argument,'NEAR(\"" + ObjLuisEntityDocumentDTO.DispositivoValue + "\", \"" + ObjLuisEntityDocumentDTO.TagValue + "\")') or contains(Device,'NEAR(\"" + ObjLuisEntityDocumentDTO.DispositivoValue + "\", \"" + ObjLuisEntityDocumentDTO.TagValue + "\")')) ";
        //    }
        //    else
        //    {
        //        if (ObjLuisEntityDocumentDTO.ScopoDocumentoValue != "")
        //        {
        //            SqlWhere += " and ";

        //            SqlWhere += "((freetext(ImageName,'" + ObjLuisEntityDocumentDTO.ScopoDocumentoValue + "') or freetext([Description],'" + ObjLuisEntityDocumentDTO.ScopoDocumentoValue + "') or freetext(Tags,'" + ObjLuisEntityDocumentDTO.ScopoDocumentoValue + "') or freetext(Argument,'" + ObjLuisEntityDocumentDTO.ScopoDocumentoValue + "') or freetext(Device,'" + ObjLuisEntityDocumentDTO.ScopoDocumentoValue + "'))";
        //            //SqlWhere += " and ((contains(Chapter.ChapterName,'near(" + ObjLuisEntityDocumentDTO.DispositivoValue + ", " + ObjLuisEntityDocumentDTO.TagValue + ")') or contains(Chapter.Argument,'near(" + ObjLuisEntityDocumentDTO.DispositivoValue + ", " + ObjLuisEntityDocumentDTO.TagValue + ")') or contains(Chapter.Device,'near(" + ObjLuisEntityDocumentDTO.DispositivoValue + ", " + ObjLuisEntityDocumentDTO.TagValue + ")'))))";
        //            // SqlWhere += "((contains(Chapter.ChapterName,'formsof(INFLECTIONAL, ' + @ScopoDocumentoValue + ')') or contains(Chapter.Argument,'formsof(INFLECTIONAL, ' + @ScopoDocumentoValue + ')') or contains(Chapter.Device,'formsof(INFLECTIONAL, ' + @ScopoDocumentoValue + ')'))";
        //        }

        //        // SqlWhere += " and ((contains(Chapter.ChapterName,'near(' + @DispositivoValue + ' , ' + @TagValue + ')') or contains(Chapter.Argument,'near(' + @DispositivoValue + ' , ' + @TagValue + ')')') or contains(Chapter.Device,'near(' + @DispositivoValue + ' , ' + @TagValue + ')')'))))";
        //        // SqlWhere += " and ((contains(Chapter.ChapterName,'near(" + ObjLuisEntityDocumentDTO.DispositivoValue + ", " + ObjLuisEntityDocumentDTO.TagValue + ")') or contains(Chapter.Argument,'near(" + ObjLuisEntityDocumentDTO.DispositivoValue + ", " + ObjLuisEntityDocumentDTO.TagValue + ")') or contains(Chapter.Device,'near(" + ObjLuisEntityDocumentDTO.DispositivoValue + ", " + ObjLuisEntityDocumentDTO.TagValue + ")'))))";
        //    }
        //    //if (countSql == 2)
        //    //{
        //    //    SqlWhere += "(" + SqlWhereInt + ")";
        //    //}
        //    //else
        //    //{
        //    //    SqlWhere += SqlWhereInt ;
        //    //}
        //    DataTable dt;
        //    using (ImageEFRepository Rep = new ImageEFRepository(""))
        //    {
        //        dt = new DataTable();
        //        var conn = Rep.Context.Database.Connection;
        //        var connectionState = conn.State;
        //        if (connectionState != ConnectionState.Open) conn.Open();
        //        using (var cmd = conn.CreateCommand())
        //        {
        //            cmd.CommandText = Sql + SqlWhere;
        //            cmd.CommandType = CommandType.Text;

        //            using (var reader = cmd.ExecuteReader())
        //            {
        //                dt.Load(reader);
        //            }
        //        }
        //    }
        //    List<GetImagesDTO> LstGetImageContentsDTO = new List<GetImagesDTO>();
        //    foreach (DataRow Dr in dt.Rows)
        //    {
        //        GetImagesDTO ObjGetImageContentsDTO = new GetImagesDTO();
        //        ObjGetImageContentsDTO.ImgName = Dr["ImageName"].ToString();
        //        ObjGetImageContentsDTO.UrlImg = Dr["ImageName"].ToString();
        //        ObjGetImageContentsDTO.Description = Dr["Description"].ToString();

        //        LstGetImageContentsDTO.Add(ObjGetImageContentsDTO);

        //    }

        //    return LstGetImageContentsDTO;
        //}

        //public List<GetDocumentsDTO> GetDocumentsOLD(LuisEntityDocumentDTO ObjLuisEntityDocumentDTO)
        //{

        //    ContainerDTO ObjContainerDTO = new ContainerDTO();
        //    string Sql = "SELECT top 5 Type.Typology, [Document].DocName,[Document].PDFName, [Document].Argument, [Document].Device, Chapter.ChapterName, Chapter.PageNumber,Chapter.Argument AS ChapterArgument, Chapter.Device AS ChapterDevice FROM  [Document] INNER JOIN  Chapter ON [Document].IdDocument = Chapter.IdDocument INNER JOIN Type ON [Document].IdTypology = Type.IdTypology ";
        //    string SqlWhere = " where 1=1 ";



        //    if (ObjLuisEntityDocumentDTO.TipoDocumentoValue != "")
        //    {
        //        //SqlWhere += " and (Type.Typology = '" + ObjLuisEntityDocumentDTO.TipoDocumentoValue + "')";
        //        SqlWhere += " and (Type.Typology = @TipoDocumentoValue)";
        //    }
        //    if (ObjLuisEntityDocumentDTO.DispositivoValue != "" || ObjLuisEntityDocumentDTO.TagValue != "")
        //    {

        //        if (ObjLuisEntityDocumentDTO.ScopoDocumentoValue != "")
        //        {
        //            SqlWhere += " and ";
        //            //siamo nel caso mostrami documento[TipoDocumento] di conformità[Scopo documento] dell'impianto
        //            SqlWhere += "(contains(Chapter.ChapterName,'formsof(INFLECTIONAL," + ObjLuisEntityDocumentDTO.ScopoDocumentoValue.Replace("'", "''") + ")') or contains(Chapter.Argument,'formsof(INFLECTIONAL," + ObjLuisEntityDocumentDTO.ScopoDocumentoValue.Replace("'", "''") + ")') or contains(Chapter.Device,'formsof(INFLECTIONAL," + ObjLuisEntityDocumentDTO.ScopoDocumentoValue.Replace("'", "''") + ")'))";
        //            //SqlWhere += "(contains(Chapter.ChapterName,'formsof(INFLECTIONAL,' + @ScopoDocumentoValue + ')') or contains(Chapter.Argument,'formsof(INFLECTIONAL, ' + @ScopoDocumentoValue + ')') or contains(Chapter.Device,'formsof(INFLECTIONAL, ' + @ScopoDocumentoValue + ')'))";
        //        }

        //        SqlWhere += " or ((contains(Chapter.ChapterName,'near(\"" + ObjLuisEntityDocumentDTO.DispositivoValue + "\", \"" + ObjLuisEntityDocumentDTO.TagValue + "\")') or contains(Chapter.Argument,'near(\"" + ObjLuisEntityDocumentDTO.DispositivoValue + "\", \"" + ObjLuisEntityDocumentDTO.TagValue + "\")') or contains(Chapter.Device,'near(\"" + ObjLuisEntityDocumentDTO.DispositivoValue + "\", \"" + ObjLuisEntityDocumentDTO.TagValue + "\")')))";
        //    }
        //    else
        //    {
        //        if (ObjLuisEntityDocumentDTO.ScopoDocumentoValue != "")
        //        {
        //            SqlWhere += " and ";

        //            SqlWhere += "((contains(Chapter.ChapterName,'formsof(INFLECTIONAL," + ObjLuisEntityDocumentDTO.ScopoDocumentoValue.Replace("'", "''") + ")') or contains(Chapter.Argument,'formsof(INFLECTIONAL," + ObjLuisEntityDocumentDTO.ScopoDocumentoValue.Replace("'", "''") + ")') or contains(Chapter.Device,'formsof(INFLECTIONAL," + ObjLuisEntityDocumentDTO.ScopoDocumentoValue.Replace("'", "''") + ")'))";
        //            //SqlWhere += " and ((contains(Chapter.ChapterName,'near(" + ObjLuisEntityDocumentDTO.DispositivoValue + ", " + ObjLuisEntityDocumentDTO.TagValue + ")') or contains(Chapter.Argument,'near(" + ObjLuisEntityDocumentDTO.DispositivoValue + ", " + ObjLuisEntityDocumentDTO.TagValue + ")') or contains(Chapter.Device,'near(" + ObjLuisEntityDocumentDTO.DispositivoValue + ", " + ObjLuisEntityDocumentDTO.TagValue + ")'))))";
        //            // SqlWhere += "((contains(Chapter.ChapterName,'formsof(INFLECTIONAL, ' + @ScopoDocumentoValue + ')') or contains(Chapter.Argument,'formsof(INFLECTIONAL, ' + @ScopoDocumentoValue + ')') or contains(Chapter.Device,'formsof(INFLECTIONAL, ' + @ScopoDocumentoValue + ')'))";
        //        }

        //        // SqlWhere += " and ((contains(Chapter.ChapterName,'near(' + @DispositivoValue + ' , ' + @TagValue + ')') or contains(Chapter.Argument,'near(' + @DispositivoValue + ' , ' + @TagValue + ')')') or contains(Chapter.Device,'near(' + @DispositivoValue + ' , ' + @TagValue + ')')'))))";
        //        // SqlWhere += " and ((contains(Chapter.ChapterName,'near(" + ObjLuisEntityDocumentDTO.DispositivoValue + ", " + ObjLuisEntityDocumentDTO.TagValue + ")') or contains(Chapter.Argument,'near(" + ObjLuisEntityDocumentDTO.DispositivoValue + ", " + ObjLuisEntityDocumentDTO.TagValue + ")') or contains(Chapter.Device,'near(" + ObjLuisEntityDocumentDTO.DispositivoValue + ", " + ObjLuisEntityDocumentDTO.TagValue + ")'))))";
        //    }
        //    //if (countSql == 2)
        //    //{
        //    //    SqlWhere += "(" + SqlWhereInt + ")";
        //    //}
        //    //else
        //    //{
        //    //    SqlWhere += SqlWhereInt ;
        //    //}
        //    DataTable dt;
        //    using (DocumentEFRepository Rep = new DocumentEFRepository(""))
        //    {
        //        dt = new DataTable();
        //        var conn = Rep.Context.Database.Connection;
        //        var connectionState = conn.State;
        //        if (connectionState != ConnectionState.Open) conn.Open();
        //        using (var cmd = conn.CreateCommand())
        //        {
        //            cmd.CommandText = Sql + SqlWhere;
        //            cmd.CommandType = CommandType.Text;

        //            //cmd.Parameters.Add(new SqlParameter("ScopoDocumentoValue", ObjLuisEntityDocumentDTO.ScopoDocumentoValue));
        //            cmd.Parameters.Add(new SqlParameter("TipoDocumentoValue", ObjLuisEntityDocumentDTO.TipoDocumentoValue));
        //            //cmd.Parameters.Add(new SqlParameter("DispositivoValue", ObjLuisEntityDocumentDTO.DispositivoValue));
        //            //cmd.Parameters.Add(new SqlParameter("TagValue", ObjLuisEntityDocumentDTO.TagValue));
        //            using (var reader = cmd.ExecuteReader())
        //            {
        //                dt.Load(reader);
        //            }
        //        }
        //    }
        //    List<GetDocumentsDTO> LstGetDocumentContentsDTO = new List<GetDocumentsDTO>();
        //    foreach (DataRow Dr in dt.Rows)
        //    {
        //        GetDocumentsDTO ObjGetDocumentContentsDTO = new GetDocumentsDTO();
        //        ObjGetDocumentContentsDTO.DocName = Dr["DocName"].ToString();
        //        ObjGetDocumentContentsDTO.UrlDoc = Dr["PDFName"].ToString();
        //        ObjGetDocumentContentsDTO.ChapterName = Dr["ChapterName"].ToString();
        //        ObjGetDocumentContentsDTO.PageNumber = Dr["PageNumber"].ToString();
        //        LstGetDocumentContentsDTO.Add(ObjGetDocumentContentsDTO);

        //    }



        //    return LstGetDocumentContentsDTO;
        //}

        //public List<GetImagesDTO> GetImagesOLD(LuisEntityDocumentDTO ObjLuisEntityDocumentDTO)
        //{

        //    ContainerDTO ObjContainerDTO = new ContainerDTO();
        //    string Sql = "select top 5 ImageName,Description,* from image ";
        //    string SqlWhere = " where 1=1 ";





        //    if (ObjLuisEntityDocumentDTO.DispositivoValue != "" || ObjLuisEntityDocumentDTO.TagValue != "")
        //    {

        //        if (ObjLuisEntityDocumentDTO.ScopoDocumentoValue != "")
        //        {
        //            SqlWhere += " and ";
        //            //siamo nel caso mostrami documento[TipoDocumento] di conformità[Scopo documento] dell'impianto
        //            SqlWhere += "(freetext(ImageName,'" + ObjLuisEntityDocumentDTO.ScopoDocumentoValue + "') or freetext([Description],'" + ObjLuisEntityDocumentDTO.ScopoDocumentoValue + "') or freetext(Tags,'" + ObjLuisEntityDocumentDTO.ScopoDocumentoValue + "') or freetext(Argument,'" + ObjLuisEntityDocumentDTO.ScopoDocumentoValue + "') or freetext(Device,'" + ObjLuisEntityDocumentDTO.ScopoDocumentoValue + "'))";
        //            //SqlWhere += "(contains(Chapter.ChapterName,'formsof(INFLECTIONAL,' + @ScopoDocumentoValue + ')') or contains(Chapter.Argument,'formsof(INFLECTIONAL, ' + @ScopoDocumentoValue + ')') or contains(Chapter.Device,'formsof(INFLECTIONAL, ' + @ScopoDocumentoValue + ')'))";
        //        }

        //        SqlWhere += " or (contains(ImageName,'NEAR(\"" + ObjLuisEntityDocumentDTO.DispositivoValue + "\", \"" + ObjLuisEntityDocumentDTO.TagValue + "\")') or contains(description,'NEAR(\"" + ObjLuisEntityDocumentDTO.DispositivoValue + "\", \"" + ObjLuisEntityDocumentDTO.TagValue + "\")') or contains(Tags,'NEAR(\"" + ObjLuisEntityDocumentDTO.DispositivoValue + "\", \"" + ObjLuisEntityDocumentDTO.TagValue + "\")') or contains(Argument,'NEAR(\"" + ObjLuisEntityDocumentDTO.DispositivoValue + "\", \"" + ObjLuisEntityDocumentDTO.TagValue + "\")') or contains(Device,'NEAR(\"" + ObjLuisEntityDocumentDTO.DispositivoValue + "\", \"" + ObjLuisEntityDocumentDTO.TagValue + "\")')) ";
        //    }
        //    else
        //    {
        //        if (ObjLuisEntityDocumentDTO.ScopoDocumentoValue != "")
        //        {
        //            SqlWhere += " and ";

        //            SqlWhere += "((freetext(ImageName,'" + ObjLuisEntityDocumentDTO.ScopoDocumentoValue + "') or freetext([Description],'" + ObjLuisEntityDocumentDTO.ScopoDocumentoValue + "') or freetext(Tags,'" + ObjLuisEntityDocumentDTO.ScopoDocumentoValue + "') or freetext(Argument,'" + ObjLuisEntityDocumentDTO.ScopoDocumentoValue + "') or freetext(Device,'" + ObjLuisEntityDocumentDTO.ScopoDocumentoValue + "'))";
        //            //SqlWhere += " and ((contains(Chapter.ChapterName,'near(" + ObjLuisEntityDocumentDTO.DispositivoValue + ", " + ObjLuisEntityDocumentDTO.TagValue + ")') or contains(Chapter.Argument,'near(" + ObjLuisEntityDocumentDTO.DispositivoValue + ", " + ObjLuisEntityDocumentDTO.TagValue + ")') or contains(Chapter.Device,'near(" + ObjLuisEntityDocumentDTO.DispositivoValue + ", " + ObjLuisEntityDocumentDTO.TagValue + ")'))))";
        //            // SqlWhere += "((contains(Chapter.ChapterName,'formsof(INFLECTIONAL, ' + @ScopoDocumentoValue + ')') or contains(Chapter.Argument,'formsof(INFLECTIONAL, ' + @ScopoDocumentoValue + ')') or contains(Chapter.Device,'formsof(INFLECTIONAL, ' + @ScopoDocumentoValue + ')'))";
        //        }

        //        // SqlWhere += " and ((contains(Chapter.ChapterName,'near(' + @DispositivoValue + ' , ' + @TagValue + ')') or contains(Chapter.Argument,'near(' + @DispositivoValue + ' , ' + @TagValue + ')')') or contains(Chapter.Device,'near(' + @DispositivoValue + ' , ' + @TagValue + ')')'))))";
        //        // SqlWhere += " and ((contains(Chapter.ChapterName,'near(" + ObjLuisEntityDocumentDTO.DispositivoValue + ", " + ObjLuisEntityDocumentDTO.TagValue + ")') or contains(Chapter.Argument,'near(" + ObjLuisEntityDocumentDTO.DispositivoValue + ", " + ObjLuisEntityDocumentDTO.TagValue + ")') or contains(Chapter.Device,'near(" + ObjLuisEntityDocumentDTO.DispositivoValue + ", " + ObjLuisEntityDocumentDTO.TagValue + ")'))))";
        //    }
        //    //if (countSql == 2)
        //    //{
        //    //    SqlWhere += "(" + SqlWhereInt + ")";
        //    //}
        //    //else
        //    //{
        //    //    SqlWhere += SqlWhereInt ;
        //    //}
        //    DataTable dt;
        //    using (ImageEFRepository Rep = new ImageEFRepository(""))
        //    {
        //        dt = new DataTable();
        //        var conn = Rep.Context.Database.Connection;
        //        var connectionState = conn.State;
        //        if (connectionState != ConnectionState.Open) conn.Open();
        //        using (var cmd = conn.CreateCommand())
        //        {
        //            cmd.CommandText = Sql + SqlWhere;
        //            cmd.CommandType = CommandType.Text;

        //            using (var reader = cmd.ExecuteReader())
        //            {
        //                dt.Load(reader);
        //            }
        //        }
        //    }
        //    List<GetImagesDTO> LstGetImageContentsDTO = new List<GetImagesDTO>();
        //    foreach (DataRow Dr in dt.Rows)
        //    {
        //        GetImagesDTO ObjGetImageContentsDTO = new GetImagesDTO();
        //        ObjGetImageContentsDTO.ImgName = Dr["ImageName"].ToString();
        //        ObjGetImageContentsDTO.UrlImg = Dr["ImageName"].ToString();
        //        ObjGetImageContentsDTO.Description = Dr["Description"].ToString();

        //        LstGetImageContentsDTO.Add(ObjGetImageContentsDTO);

        //    }

        //    return LstGetImageContentsDTO;
        //}
        #endregion



    }
}