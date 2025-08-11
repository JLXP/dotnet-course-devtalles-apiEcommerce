using System;

namespace ApiEcommerce.Models.Dtos;

public class UserRegisterDto
{
    public int ID { get; set; }
    public required string Name { get; set; }
    public required string Username { get; set; }
    public string? Password { get; set; }
    public string? Role { get; set; }
}
