namespace Devices_E_Commerce.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Category
{ 
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    public string? Image { get; set; } = "default.png";
    public string? Details { get; set; }
    // Navigation property
    public ICollection<Device>? Devices { get; set; }
}