using KUHr.Data.Entities.Base;
using System.Collections.Generic;
using System;


namespace KUHr.Data.Entities.InvoiceTables
{
   public class Invoice : BaseModel
    {
        public string InvoiceNumber { get; set; }
        public string CustomerName { get; set; }
        public decimal TotalAmount { get; set; }
        public virtual ICollection<InvoiceItem> InvoiceItems { get; set; } = new List<InvoiceItem>();
        public string Notes { get; set; }
    }

}
