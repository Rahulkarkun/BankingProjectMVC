using BankingProjectMVC.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankingProjectMVC.MappingFiles
{
    public class AccountTypeMap : ClassMap<AccountType>
    {
        public AccountTypeMap()
        {
            {
                Table("AccountType");
                Id(x => x.Id);
                Map(x => x.Type);
                HasMany(x => x.Accounts).Inverse().Cascade.SaveUpdate().KeyColumn("AccountTypeId");

            }
        }
    }
}