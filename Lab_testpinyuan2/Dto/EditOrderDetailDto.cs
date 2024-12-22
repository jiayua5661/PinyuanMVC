namespace Lab_testpinyuan2.Dto
{
    public class EditOrderDetailDto
    {
        public int OrdeDetailId { get; set; }

        public string ProductName { get; set; } = null!;

        public short Amount { get; set; }

        public int Price { get; set; }
    }
}
