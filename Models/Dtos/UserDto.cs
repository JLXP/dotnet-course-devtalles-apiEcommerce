using System;
using System.ComponentModel.DataAnnotations;

namespace ApiEcommerce.Models.Dtos;

public class UserDto
{
    [Key]
    public string Id { get; set; } = string.Empty;
    public string? Name { get; set; }
    public string? Username { get; set; }
}
