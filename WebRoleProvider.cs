using BankingProjectMVC.Helpers;
using BankingProjectMVC.Models;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace BankingProjectMVC
{
    public class WebRoleProvider:RoleProvider
    {
        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }


        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            //throw new NotImplementedException(); 
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var txn = session.BeginTransaction())
                {
                    var userRoles = session.Query<User>().Where(x => x.Username == username).Fetch(x => x.Role).Select(x => x.Role.RoleName).FirstOrDefault();
                    var str = new string[1];
                    str[0] = userRoles;
                    return str;
                }
            }
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}