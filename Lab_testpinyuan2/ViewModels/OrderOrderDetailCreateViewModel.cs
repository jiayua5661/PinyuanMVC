using Lab_testpinyuan2.Dto;

namespace Lab_testpinyuan2.ViewModels
{
    public class OrderOrderDetailCreateViewModel
    {
        public OrderDto orderDto { get; set; }

        public List<EditOrderClientDto> EditOrderClientDtos { get; set; }
    }
}
