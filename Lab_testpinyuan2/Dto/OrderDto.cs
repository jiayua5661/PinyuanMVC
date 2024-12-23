using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Lab_testpinyuan2.Dto
{
    public class OrderDto
    {
        [DisplayName("客戶名稱")]
        public int CompanyId { get; set; }

        [DisplayName("訂單日期")]
        public DateOnly OrderDate { get; set; }

        [DisplayName("估價單號碼")]
        [MaxLength(5)]
        public string? QuoteNumber { get; set; }

        public List<OrderDetailDto> OrderDetailDtos { get; set; }
    }
}
