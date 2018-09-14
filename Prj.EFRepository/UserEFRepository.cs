using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ls.Base.EFRepository;
using Ls.Prj.EF;
using Ls.Prj.Entity;
using AutoMapper;

namespace Ls.Prj.EFRepository
{
    public class UserEFRepository : GenericEFRepository<AiChatboxContest, User> 
    {
        public UserEFRepository(string Username)
            : base (Username)
        {
           
        }

        public User SelectEntity(Int32 IdEntity)
        {
            User Obj = null;
            using (UserEFRepository Rep = new UserEFRepository(""))
            {

                Obj = Rep.Context.Users.Where(x => x.IdUser == IdEntity).ToList().FirstOrDefault();

            }

            return Obj;

        }

    }
}
