namespace Invoice.Pages.Model
{
    public class InvoiceDto
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
