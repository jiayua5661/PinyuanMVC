using LAB_testpinyuan.Dto;
using Lab_testpinyuan2.Models;

namespace Lab_testpinyuan2.Dto
{
    public class EditOrderDto
    {
        public int OrderId { get; set; }

        public int ClientId { get; set; }   

        public DateOnly OrderDate { get; set; }

        public string? QuoteNumber { get; set; }

        public List<OrderDetail> EditOrderDetail { get; set; }
    }
}
