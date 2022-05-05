using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApp.Data;
using UserApp.Repo;

namespace UserApp.Service
{
    public class UserService
    {
        RepositoryUser _repositoryUser;
        public UserService(UserAppContext context)
        {
            _repositoryUser = new RepositoryUser(context);
        }

        public IEnumerable<User> GetAllList()
        {           
            return _repositoryUser.GetAllList();
        }
        public User GetById(int id)
        {
            return _repositoryUser.GetById(id);
        }

        public void Create(User user)
        {
            _repositoryUser.Create(user);
        }

        public void Delete(User user)
        {
           _repositoryUser.Delete(user);
        }

        public void Update(User user)
        {
            _repositoryUser.Update(user);
        }
    }
}
