namespace PaymentService.Domain.Entities
{
    public class Payment
    {
        public Guid Id { get; private set; }
        public Guid InvoiceId { get; private set; }
        public Guid UserId { get; private set; }
        public decimal Amount { get; private set; }
        public DateTime PaymentDate { get; private set; }
        public string PaymentStatus { get; private set; }

        public Payment(Guid invoiceId, Guid userId, decimal amount)
        {
            Id = Guid.NewGuid();
            InvoiceId = invoiceId;
            UserId = userId;
            Amount = amount;
            PaymentDate = DateTime.UtcNow;
            PaymentStatus = "Pending";
        }

        public void MarkAsPaid()
        {
            PaymentStatus = "Paid";
        }

        public void MarkAsFailed()
        {
            PaymentStatus = "Failed";
        }
    }

}
