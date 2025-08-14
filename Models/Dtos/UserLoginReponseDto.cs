using System;

namespace ApiEcommerce.Models.Dtos;

public class UserLoginReponseDto
{

    public UserDataDto? User { get; set; }
    public string? Token { get; set; }
    public string? Message { get; set; }
}
