using LAB_testpinyuan.Dto;
using Lab_testpinyuan2.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Lab_testpinyuan2.Dto
{
    public class EditOrderDto
    {
        public int OrderId { get; set; }

        [DisplayName("客戶名稱")]
        public int ClientId { get; set; }

        [DisplayName("訂單日期")]
        public DateOnly OrderDate { get; set; }

        [DisplayName("估價單號碼")]
        [MaxLength(5)]
        public string? QuoteNumber { get; set; }

        public List<OrderDetail> EditOrderDetail { get; set; }
    }
}
