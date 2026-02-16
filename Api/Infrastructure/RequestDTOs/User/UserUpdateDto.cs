using System;

namespace Api.Infrastructure.RequestDTOs.User;

public class UserUpdateDto
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}
