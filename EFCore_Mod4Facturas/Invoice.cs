using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EFCore_Mod4Facturas
{
    public class Invoice
    {
        public int Id { get; set; }
        [Required]
        public DateTime DateIssue { get; set; }
        public decimal Total { get; set; }
        public List<InvoiceDetail> Details { get; set; }
    }
}