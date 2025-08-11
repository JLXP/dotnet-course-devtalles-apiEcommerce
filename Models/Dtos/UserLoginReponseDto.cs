using System;

namespace ApiEcommerce.Models.Dtos;

public class UserLoginReponseDto
{

    public UserRegisterDto? User { get; set; }
    public string? Token { get; set; }
    public string? Message { get; set; }
}
