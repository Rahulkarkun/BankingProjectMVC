using BankingProjectMVC.Models;
using BankingProjectMVC.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankingProjectMVC.Services
{
    public class DocumentService : IDocumentService
    {

        private readonly IDocumentRepository _documentRepository;
        public DocumentService(IDocumentRepository documentRepository)
        {
            _documentRepository = documentRepository;
        }
        public string Add(Document document)
        {
            return _documentRepository.Add(document);
        }
        public string Update(Document document)
        {
            return _documentRepository.Update(document);
        }
        public string Delete(Document document)
        {
            return _documentRepository.Delete(document);
        }
        public Document GetById(int documentId)
        {
            return _documentRepository.GetById(documentId);
        }
        public List<Document> GetAll()
        {
            return _documentRepository.GetAll();
        }
    }
}