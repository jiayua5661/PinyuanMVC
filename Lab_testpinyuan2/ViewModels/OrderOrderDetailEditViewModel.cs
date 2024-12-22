using Lab_testpinyuan2.Dto;

namespace Lab_testpinyuan2.ViewModels
{
    public class OrderOrderDetailEditViewModel
    {
        public EditOrderDto Order { get; set; }

        public List<EditOrderClientDto> EditOrderClientDtos { get; set; }
    }
}
