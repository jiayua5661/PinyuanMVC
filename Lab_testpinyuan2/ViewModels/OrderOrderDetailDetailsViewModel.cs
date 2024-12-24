using Lab_testpinyuan2.Dto;
using System.ComponentModel;

namespace Lab_testpinyuan2.ViewModels
{
    public class OrderOrderDetailDetailsViewModel
    {
        [DisplayName("客戶名稱")]
        public string CompanyName { get; set; } = null!;

        [DisplayName("訂單日期")]
        public DateOnly OrderDate { get; set; }

        [DisplayName("估價單號碼")]
        public string? QuoteNumber { get; set; }

        public List<OrderDetailDto> OrderDetailDtos { get; set; }
    }
}
