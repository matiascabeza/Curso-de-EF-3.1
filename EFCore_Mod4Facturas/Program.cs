using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore_Mod4Facturas
{
    class Program
    {
        static void Main(string[] args)
        {
            CreandoLaFacturaYSuDetalleDeManeraSimultanea();
        }

        static void CreandoLaFacturaYSuDetalleDeManeraSimultanea()
        {
            using (var context = new ApplicationDbContext())
            {
                List<Product> products = context.Products.ToList();

                var InvoiceDetail1 = new InvoiceDetail();
                InvoiceDetail1.ProductId = products[0].Id;
                InvoiceDetail1.Price = products[0].Price;
                InvoiceDetail1.Quantity = 3;

                var InvoiceDetail2 = new InvoiceDetail();
                InvoiceDetail2.ProductId = products[1].Id;
                InvoiceDetail2.Price = products[1].Price;
                InvoiceDetail2.Quantity = 1;

                List<InvoiceDetail> details = new List<InvoiceDetail>() { InvoiceDetail1, InvoiceDetail2 };

                var invoice = new Invoice();
                invoice.DateIssue = DateTime.Now;
                invoice.Details = details;
                invoice.Total = details.Sum(x => x.Price * x.Quantity);

                context.Add(invoice);
                context.SaveChanges();
            }
        }
    }
}
