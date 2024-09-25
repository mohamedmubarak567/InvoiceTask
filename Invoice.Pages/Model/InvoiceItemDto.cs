namespace Invoice.Pages.Model
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
