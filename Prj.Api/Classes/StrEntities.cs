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

namespace AIChatbot.Api.Structure
{
    public static class StrEntities
    {

        public const string TipoDocumento = "TipoDocumento";
        public const string ScopoDocumento = "ScopoDocumento";
        public const string Dispositivo = "Dispositivo";
        public const string Tag = "Tag";

    }
}