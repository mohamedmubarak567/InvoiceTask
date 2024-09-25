using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUHr.Service.Dto
{
  public  class InvoiceDto
    {
        public Guid? Id { get; set; }
        public string InvoiceNumber { get; set; }
        public string CustomerName { get; set; }
        public decimal TotalAmount { get; set; }
        public IEnumerable<InvoiceItemDto> InvoiceItems { get; set; }
        public string Notes { get; set; }
        public int InvoiceItemCount { get; set; }
    }
}
