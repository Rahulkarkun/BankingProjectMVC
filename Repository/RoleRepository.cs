using BankingProjectMVC.Helpers;
using BankingProjectMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankingProjectMVC.Repository
{
    public class RoleRepository : IRoleRepository
    {
        public string Add(Role role)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var txn = session.BeginTransaction())
                {
                    session.Save(role);
                    txn.Commit();

                }
            }
            return "Added Succesfully";
        }
        public string Update(Role role)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var txn = session.BeginTransaction())
                {
                    session.Update(role);
                    txn.Commit();

                }
            }
            return "Updated Succesfully";
        }
        public string Delete(Role role)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var txn = session.BeginTransaction())
                {
                    session.Delete(role);
                    txn.Commit();

                }
            }
            return "Deleted Succesfully";
        }
        public Role GetById(int roleId)
        {
            Role role = null;
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var txn = session.BeginTransaction())
                {
                    role = session.Load<Role>(roleId);
                    txn.Commit();

                }
            }
            return role;
        }
        public List<Role> GetAll()
        {
            List<Role> role = new List<Role>() { };
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var txn = session.BeginTransaction())
                {
                    role = session.Query<Role>().ToList();
                    txn.Commit();

                }
            }
            return role;
        }
    }
}