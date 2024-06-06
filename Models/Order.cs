namespace Devices_E_Commerce.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Order
{
    [Key]
    public int Id { get; set; }

    [Required]
    public DateTime? OrderDate { get; set; }

    [Required]
    public double? TotalAmount { get; set; }

    [Required]
    public int CustomerId { get; set; }

    // Navigation property
    public CustomerAccount? Customer { get; set; }

    // Navigation property
    public ICollection<OrderItem>? OrderItems { get; set; }

    // Navigation property
    public Payment? Payment { get; set; }
}
