using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Lab_testpinyuan2.Models;

/// <summary>
/// 客戶資料表
/// </summary>
public partial class Client
{
    public int ClientId { get; set; }

    [DisplayName("客戶名稱")]
    public string CompanyName { get; set; } = null!;

    [DisplayName("電話")]
    public string? Tel { get; set; }

    [DisplayName("傳真")]
    public string? Fax { get; set; }

    [DisplayName("地址")]
    public string? Address { get; set; }

    [DisplayName("統一編號")]
    public string? TaxIdnumber { get; set; }
}
