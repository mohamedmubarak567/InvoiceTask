using KUHr.Data.Entities.InvoiceTables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUHr.Service.Dto
{
    public class InvoiceItemDto
    {
        public Guid? Id { get; set; }
        public Guid InvoiceId { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
