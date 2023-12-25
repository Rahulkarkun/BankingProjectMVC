using BankingProjectMVC.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankingProjectMVC.MappingFiles
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Table("Users"); 
            Id(x => x.Id);
            Map(x => x.Username); 
            Map(x => x.Password);
            References(x => x.Role).Column("RoleId");
            HasOne(x => x.Customer).Cascade.All();
        }
    }
}