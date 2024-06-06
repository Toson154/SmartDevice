namespace Devices_E_Commerce.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Review
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Comment { get; set; }

    [Required]
    [Range(1, 5)]
    public int Rating { get; set; }

    [Required]
    public string UserName { get; set; }

    [ForeignKey("Device")]
    public int DeviceId { get; set; }

    // Navigation property
    public Device? Device { get; set; }
}
