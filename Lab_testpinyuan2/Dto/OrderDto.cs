using System.ComponentModel.DataAnnotations;

namespace LAB_testpinyuan.Dto
{
    public class OrderDto
    {
        public int CompanyId { get; set; }

        public DateOnly OrderDate { get; set; }

        [MaxLength(5)]
        public string? QuoteNumber { get; set; }

        public List<OrderDetailDto> OrderDetailDtos { get; set; }
    }
}
