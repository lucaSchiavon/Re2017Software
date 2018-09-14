using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ls.Prj.Api.DTO
{
    public class ContainerDTO
    {
        /// <summary>
        /// proprietà buleana: se impostata a false si è verificato un errore nella chiamata alle api se true l'api non ha restituito errori.
        /// </summary>
        public bool success
        {
            get;
            set;
        }
        /// <summary>
        /// questa proprietà risulta valorizzata con il messaggio dell'errore solo se si è verificato un errore nell'api e quindi la proprietà success è impostata a false.
        /// </summary>
        public string msg
        {
            get;
            set;
        }
        /// <summary>
        /// contiene il contenuto restituito dall'api ossia i dati richiesti sotto forma di array di oggetti dynamic.
        /// </summary>
        public dynamic[] payload
        {
            get;
            set;
        }
    }
}
