﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ls.Prj.DTO
{
    //[Serializable]
    public class UpdateTemplateDTO
    {
        public int id { get; set; }
        public string description { get; set; }
        public bool disabled { get; set; }
    }
}
