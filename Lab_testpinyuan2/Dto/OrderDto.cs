using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LAB_testpinyuan.Dto
{
    public class OrderDto
    {
        public int CompanyId { get; set; }

        [DisplayName("訂單日")]
        public DateOnly OrderDate { get; set; }

        [DisplayName("估價單號")]
        [MaxLength(5)]
        public string? QuoteNumber { get; set; }

        public List<OrderDetailDto> OrderDetailDtos { get; set; }
    }
}
