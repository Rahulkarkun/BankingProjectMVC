using BankingProjectMVC.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankingProjectMVC.MappingFiles
{
    public class TransactionMap : ClassMap<Transaction>
    {
        public TransactionMap()
        {
            Table("Transactions");
            Id(o => o.Id);
            Map(o => o.TransactionType);
            Map(o => o.Amount);
            Map(o => o.Date);
            Map(o => o.ToAccountNumber);
            Map(o => o.FromAccountNumber);
            References(o => o.Account).Column("AccountId").Cascade.All();
        }
    }
}