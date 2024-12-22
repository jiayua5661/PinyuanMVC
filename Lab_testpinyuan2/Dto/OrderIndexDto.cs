using System.ComponentModel;

namespace Lab_testpinyuan2.Dto
{
    public class OrderIndexDto // 這邊應該要是 OrderIndexViewModel
    {
        public int OrderId { get; set; }

        public int CompanyId { get; set; }

        [DisplayName("客戶名稱")]
        public string CompanyName { get; set; } = null!;

        [DisplayName("訂單日期")]
        public DateOnly OrderDate { get; set; }

        [DisplayName("估價單號碼")]
        public string? QuoteNumber { get; set; }
    }
}
