using BankingProjectMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingProjectMVC.Repository
{
    public interface IUserRepository
    {
        string Add(User user);
        string Update(User user);
        string Delete(User user);
        User GetById(int userId);
        List<User> GetAll();
        User GetUserWithRole(int id);
        User GetUserByUsername(string username);
    }
}
