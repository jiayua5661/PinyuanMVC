namespace Lab_testpinyuan2.Dto
{
    public class OrderIndexDto
    {
        public int OrderId { get; set; }

        public int CompanyId { get; set; }
        public string CompanyName { get; set; } = null!;

        public DateOnly OrderDate { get; set; }

        public string? QuoteNumber { get; set; }
    }
}
