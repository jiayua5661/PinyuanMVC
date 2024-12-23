using System.ComponentModel;

namespace Lab_testpinyuan2.ViewModels
{
    public class OrderDetailIndexViewModel
    {
        [DisplayName("客戶名稱")]
        public string CompanyName { get; set; } = null!;

        [DisplayName("訂單日期")]
        public DateOnly OrderDate { get; set; }

        [DisplayName("估價單號碼")]
        public string? QuoteNumber { get; set; }

        [DisplayName("產品名稱")]
        public string ProductName { get; set; } = null!;

        [DisplayName("數量")]
        public short Amount { get; set; }

        [DisplayName("單價")]
        public int Price { get; set; }
    }
}
