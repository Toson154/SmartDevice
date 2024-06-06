namespace Devices_E_Commerce.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
public class CustomerAccount
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string FirstName { get; set; }

    [Required]
    public string? LastName { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Phone]
    public string? Phone { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    // Navigation property
    public ICollection<Order>? Orders { get; set; }
}
