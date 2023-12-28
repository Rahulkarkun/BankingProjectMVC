using BankingProjectMVC.Helpers;
using BankingProjectMVC.Models;
using BankingProjectMVC.ViewModels;
using NHibernate.Linq;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankingProjectMVC.Repository
{
    public class UserRepository : IUserRepository
    {
        public User Add(User user)
        {
            //NHibernateProfilerBootstrapper.PreStart();

            using (var session = NHibernateHelper.OpenSession())
            {
                using (var txn = session.BeginTransaction())
                {
                    session.Save(user);
                    txn.Commit();

                }
            }
            return user;
        }
        public string Update(User user)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var txn = session.BeginTransaction())
                {
                    session.Update(user);
                    txn.Commit();

                }
            }
            return "Updated Succesfully";
        }
        public string Delete(User user)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var txn = session.BeginTransaction())
                {
                    session.Delete(user);
                    txn.Commit();

                }
            }
            return "Deleted Succesfully";
        }
        public User GetById(int userId)
        {
            User user = null;
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var txn = session.BeginTransaction())
                {
                    user = session.Query<User>()
                        .Where(c => c.Id == userId)
                        .FirstOrDefault();
                    txn.Commit();

                }
            }
            return user;
        }
        public List<User> GetAll()
        {
            List<User> user = new List<User>() { };
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var txn = session.BeginTransaction())
                {
                    user = session.Query<User>().ToList();
                    txn.Commit();

                }
            }
            return user;
        }

        public User GetUserWithRole(int id)
        {
            User user = null;
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var txn = session.BeginTransaction())
                {
                    user = session.Query<User>().Where(x => x.Id == id).Fetch(x => x.Role).FirstOrDefault();
                    //user = session.Load<User>(id);
                    txn.Commit();
                }
            }
            return user;
        }
        public User GetUserByUsername(string username)
        {
            User user = null;
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var txn = session.BeginTransaction())
                {
                    //user = session.Load<User>(username);
                    //user = session.QueryOver<User>().Where(Restrictions.Eq("Username", username)).SingleOrDefault();
                    //var query = session.QueryOver<User>().Where(x=>x.Username==username);
                    //var query = session.CreateCriteria<User>().List<User>();

                    ////var query = session.Query<User>().ToList<User>();
                    //user =  query.Where(x => x.Username == username).SingleOrDefault();
                    //var query = from u in session.Query<User>()
                    //            where u.Username == username
                    //            select u;

                    //user = list[0];
                    // var user = session.Query<User>().Where(x => x.Username == username);

                    //var temp= session.Query<User>().Where(x=>x.Username==username)
                    //    .Fetch(x=>x.Role);
                    user = session.Query<User>().Where(x => x.Username == username)
                        .Fetch(x => x.Role).FirstOrDefault();
                    //user = query.SingleOrDefault();

                    txn.Commit();
                }
            }
            return user;

        }
    }
}