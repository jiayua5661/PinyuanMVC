using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Lab_testpinyuan2.Dto
{
    public class OrderDetailDto
    {
        [MaxLength(50, ErrorMessage = "產品名稱最多50字")]
        [DisplayName("產品名稱")]
        public string ProductName { get; set; } = null!;

        [Range(1, 32000, ErrorMessage = "數量範圍1-32000")]
        [DisplayName("數量")]
        public short Amount { get; set; }

        [DisplayName("單價")]
        public int Price { get; set; }
    }
}
