using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApp.Data;

namespace UserApp.Repo
{
    public class RepositoryUser : Repository<User>
    {
        public RepositoryUser(UserAppContext context) : base(context)
        {

        }

    }
}
