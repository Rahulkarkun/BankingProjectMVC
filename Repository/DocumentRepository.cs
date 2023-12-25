using BankingProjectMVC.Helpers;
using BankingProjectMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankingProjectMVC.Repository
{
    public class DocumentRepository : IDocumentRepository
    {

        public string Add(Document document)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var txn = session.BeginTransaction())
                {
                    session.Save(document);
                    txn.Commit();
                }
            }
            return "Added Successfully";
        }

        public string Update(Document document)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var txn = session.BeginTransaction())
                {
                    session.Update(document);
                    txn.Commit();
                }
            }
            return "Updated Successfully";
        }

        public string Delete(Document document)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var txn = session.BeginTransaction())
                {
                    session.Delete(document);
                    txn.Commit();
                }
            }
            return "Deleted Successfully";
        }

        public Document GetById(int documentId)
        {
            Document document = null;
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var txn = session.BeginTransaction())
                {
                    document = session.Load<Document>(documentId);
                    txn.Commit();
                }
            }
            return document;
        }

        public List<Document> GetAll()
        {
            List<Document> documents = new List<Document>() { };
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var txn = session.BeginTransaction())
                {
                    documents = session.Query<Document>().ToList();
                    txn.Commit();
                }
            }
            return documents;
        }

    }
}
