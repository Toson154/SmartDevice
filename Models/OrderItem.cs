namespace Devices_E_Commerce.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class OrderItem
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int? Quantity { get; set; }

    [Required]
    public double? Price { get; set; }

    [Required]
    public int OrderId { get; set; }

    [Required]
    public int DeviceId { get; set; }

    // Navigation property
    public Order? Order { get; set; }

    // Navigation property
    public Device? Device { get; set; }
}
