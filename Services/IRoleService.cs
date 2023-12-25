using BankingProjectMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingProjectMVC.Services
{
    public interface IRoleService
    {
        string Add(Role role);
        string Update(Role role);
        string Delete(Role role);
        Role GetById(int roleId);
        List<Role> GetAll();
    }
}
