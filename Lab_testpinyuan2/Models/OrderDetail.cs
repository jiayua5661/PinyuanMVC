using System;
using System.Collections.Generic;

namespace Lab_testpinyuan2.Models;

public partial class OrderDetail
{
    public int OrdeDetailId { get; set; }

    public int OrderId { get; set; }

    public string ProductName { get; set; } = null!;

    public short Amount { get; set; }

    public int Price { get; set; }
}
