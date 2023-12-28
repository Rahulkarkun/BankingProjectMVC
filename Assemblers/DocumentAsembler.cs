using BankingProjectMVC.Models;
using BankingProjectMVC.Services;
using BankingProjectMVC.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace BankingProjectMVC.Assemblers
{
    public class DocumentAssembler
    {
        public readonly ICustomerService _customerService;
        public DocumentAssembler(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        public Document ConvertToModel(DocumentVM documentVM)
        {
            var cust = _customerService.GetById(documentVM.CustomerId);
            byte[] documentFileBytes = null;

            // Convert HttpPostedFile to byte array
            if (documentVM.PostedFile != null && documentVM.PostedFile.ContentLength > 0)
            {
                using (var binaryReader = new BinaryReader(documentVM.PostedFile.InputStream))
                {
                    documentFileBytes = binaryReader.ReadBytes(documentVM.PostedFile.ContentLength);
                }
            }
            return new Document()
            {
                Id = documentVM.Id,
                DocumentName = documentVM.DocumentName,
                DocumentFile = documentFileBytes,
                IsVerified = documentVM.IsVerified,
                //Customer = new Customer() { Id = documentVM.CustomerId },
                Customer = cust
            };
        }
        public DocumentVM ConvertToViewModel(Document document)
        {
            return new DocumentVM()
            {
                Id = document.Id,
                DocumentName = document.DocumentName,
                DocumentFile = document.DocumentFile,
                CustomerName = document.Customer.FirstName,
                IsVerified = document.IsVerified,
                CustomerId = document.Customer.Id,
            };
        }
    }
}