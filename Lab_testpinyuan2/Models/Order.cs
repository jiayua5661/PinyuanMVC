using System;
using System.Collections.Generic;

namespace Lab_testpinyuan2.Models;

/// <summary>
/// 訂單表
/// </summary>
public partial class Order
{
    public int OrderId { get; set; }

    public int CompanyId { get; set; }

    public DateOnly OrderDate { get; set; }

    public string? QuoteNumber { get; set; }
}
