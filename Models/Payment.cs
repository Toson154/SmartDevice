namespace Devices_E_Commerce.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Payment
{
    [Key]
    public int Id { get; set; }

    [Required]
    public DateTime PaymentDate { get; set; }

    [Required]
    public int OrderId { get; set; }

    // Navigation property
    public Order? Order { get; set; }
}
