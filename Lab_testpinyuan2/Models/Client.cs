using System;
using System.Collections.Generic;

namespace Lab_testpinyuan2.Models;

/// <summary>
/// 客戶資料表
/// </summary>
public partial class Client
{
    public int ClientId { get; set; }

    public string CompanyName { get; set; } = null!;

    public string? Tel { get; set; }

    public string? Fax { get; set; }

    public string? Address { get; set; }

    public string? TaxIdnumber { get; set; }
}
