using BankingProjectMVC.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankingProjectMVC.MappingFiles
{
    public class DocumentMap : ClassMap<Document>
    {
        public DocumentMap()
        {
            Table("Document");
            Id(m => m.Id);
            Map(m => m.DocumentName);
            Map(m => m.IsVerified);
            //Map(m => m.DocumentFile);
            Map(m => m.DocumentFile).CustomType("BinaryBlob").Length(int.MaxValue);
            References(m => m.Customer).Column("CustomerId");

        }
    }
}