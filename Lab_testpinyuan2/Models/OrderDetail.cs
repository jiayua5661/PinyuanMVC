using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Lab_testpinyuan2.Models;

public partial class OrderDetail
{
    public int OrdeDetailId { get; set; }

    public int OrderId { get; set; }

    [MaxLength(50, ErrorMessage = "產品名稱最多50字")]
    [DisplayName("產品名稱")]
    public string ProductName { get; set; } = null!;

    [Range(1, 32000, ErrorMessage = "數量範圍1-32000")]
    [DisplayName("數量")]
    public short Amount { get; set; }

    [DisplayName("單價")]
    public int Price { get; set; }
}
