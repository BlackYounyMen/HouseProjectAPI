using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace House.Dto
{
    public class CustomerListDto
    {

        public int Id { get; set; }
        public string Number { get; set; }
        public string CustomerName { get; set; }
        public string CompanyAddress { get; set; }
        public string Contacts { get; set; }
        public string Telephone { get; set; }
        public string BankAccount { get; set; }
        public string BankName { get; set; }
        public string EnterpriseCode { get; set; }
        public string CustomerType { get; set; }
        public string Industry { get; set; }
        public string CreditRating { get; set; }
        public string Representative { get; set; }
        public string TaxpayerNum { get; set; }

        public string Cus_Id { get; set; }
        public string DustomerId { get; set; }
        public string Name { get; set; }
        public string Post { get; set; }
        public string Phone { get; set; }
        public string Dep { get; set; }
        public string Email { get; set; }
        public DateTime EntryTime { get; set; }


        public string FileName { get; set; }
        public DateTime UploadTime { get; set; }
        public string FileSize { get; set; }
        public string FileType { get; set; }
        public string Enteredby { get; set; }
        public string Url { get; set; }
        public string FIleCategroy { get; set; }
    }
    public class CustomerListDtoState
    {
        public bool Select { get; set; }
        public bool Id { get; set; }
        public bool Number { get; set; }
        public bool CustomerName { get; set; }
        public bool CompanyAddress { get; set; }
        public bool Contacts { get; set; }
        public bool Telephone { get; set; }
        public bool BankAccount { get; set; }
        public bool BankName { get; set; }
        public bool EnterpriseCode { get; set; }
        public bool CustomerType { get; set; }
        public bool Industry { get; set; }
        public bool CreditRating { get; set; }
        public bool Representative { get; set; }
        public bool TaxpayerNum { get; set; }

        public bool Cus_Id { get; set; }
        public bool DustomerId { get; set; }
        public bool Name { get; set; }
        public bool Post { get; set; }
        public bool Phone { get; set; }
        public bool Dep { get; set; }
        public bool Email { get; set; }
        public bool EntryTime { get; set; }


        public bool FileName { get; set; }
        public bool UploadTime { get; set; }
        public bool FileSize { get; set; }
        public bool FileType { get; set; }
        public bool Enteredby { get; set; }
        public bool Url { get; set; }
        public bool FIleCategroy { get; set; }
    }
}
