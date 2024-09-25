using KUHr.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUHr.Data.Entities.InvoiceTables
{
    public class InvoiceItem : BaseModel
    {
        public Guid InvoiceId { get; set; } 
        [ForeignKey("InvoiceId")]
        public virtual Invoice Invoice { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
