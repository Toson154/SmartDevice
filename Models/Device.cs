namespace Devices_E_Commerce.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Device
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public double Price { get; set; }

    [ForeignKey("Category")]
    public int CategoryId { get; set; }

    // Navigation property
    public Category? Category { get; set; }


    // Navigation property
    public string? DeviceImage { get; set; } = "default.png";
    public ICollection<Review>? Reviews { get; set; }

    // Navigation property
    public ICollection<OrderItem>? OrderItems { get; set; }
}
