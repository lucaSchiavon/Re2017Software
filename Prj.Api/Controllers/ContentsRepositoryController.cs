using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Prj.Api.Base.Controllers;
using Ls.Prj.Api.DTO;
using AIChatbot.Api.Classes;
using Ls.Prj.EFRepository;

namespace SwoordsApi.Controllers
{
    public class ContentsRepositoryController : BaseController
    {

        #region "Codice vecchio"
        /// <summary>
        /// dato Intent ed Entities ritorna i contenuti richiesti dal chatbot
        /// </summary>
        ///<remarks>
        ///
        /// </remarks>
        ///  
        //[Route("api/ContentsRepository/GetDocumentContents")]
        //[HttpPost]
        //public System.Web.Http.Results.JsonResult<ContainerDTO> GetDocumentContents(LuisEntityDocumentDTO ObjLuisEntityDocumentDTO)
        //{
        //    ContentRepositoryPageManager _MngContentRepositoryPageManager = new ContentRepositoryPageManager();
        //    ContainerDTO ObjContainerDTO = new ContainerDTO();

        //    try
        //    {

        //       // ObjContainerDTO = _MngContentRepositoryPageManager.GetDocumentContents(ObjLuisEntityDocumentDTO);
        //        ObjContainerDTO.success = true;
        //    }

        //      catch (Exception ex)
        //    {
        //        ObjContainerDTO.success = false;
        //        ObjContainerDTO.msg = ex.Message;     
        //    }

        //    return Json(ObjContainerDTO);

        //}
        #endregion

        [Route("api/ContentsRepository/GetDocumentContents")]
        [HttpPost]
        public System.Web.Http.Results.JsonResult<ContainerDTO> GetDocumentContents(LuisEntityDocumentDTO ObjLuisEntityDocumentDTO)
        {
            ContentRepositoryPageManager _MngContentRepositoryPageManager = new ContentRepositoryPageManager();
            ContainerDTO ObjContainerDTO = new ContainerDTO();

            try
            {

                ObjContainerDTO = _MngContentRepositoryPageManager.GetDocumentContents(ObjLuisEntityDocumentDTO);
                ObjContainerDTO.success = true;
            }

            catch (Exception ex)
            {
                ObjContainerDTO.success = false;
                ObjContainerDTO.msg = ex.Message;
            }

            return Json(ObjContainerDTO);

        }

        [Route("api/ContentsRepository/GetAlarms")]
        [HttpPost]
        public System.Web.Http.Results.JsonResult<ContainerDTO> GetAlarms(LuisEntityAlarmDTO ObjLuisEntityAlarmDTO)
        {
            ContentRepositoryPageManager _MngContentRepositoryPageManager = new ContentRepositoryPageManager();
            ContainerDTO ObjContainerDTO = new ContainerDTO();

            try
            {
              
                ObjContainerDTO = _MngContentRepositoryPageManager.GetAlarms(ObjLuisEntityAlarmDTO);
                ObjContainerDTO.success = true;
            }

            catch (Exception ex)
            {
                ObjContainerDTO.success = false;
                ObjContainerDTO.msg = ex.Message;
            }

            return Json(ObjContainerDTO);

        }

        [Route("api/ContentsRepository/GetAudit")]
        [HttpPost]
        public System.Web.Http.Results.JsonResult<ContainerDTO> GetAudit(LuisEntityAuditDTO ObjLuisEntityAuditDTO)
        {
            ContentRepositoryPageManager _MngContentRepositoryPageManager = new ContentRepositoryPageManager();
            ContainerDTO ObjContainerDTO = new ContainerDTO();

            try
            {

                ObjContainerDTO = _MngContentRepositoryPageManager.GetAudit(ObjLuisEntityAuditDTO);
                ObjContainerDTO.success = true;
            }

            catch (Exception ex)
            {
                ObjContainerDTO.success = false;
                ObjContainerDTO.msg = ex.Message;
            }

            return Json(ObjContainerDTO);

        }

    }
}
