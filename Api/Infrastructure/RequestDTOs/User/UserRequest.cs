using System;
using System.Collections;
using System.Collections.Generic;
using Api.Infrastructure.ResponseDTOs.Roles;

namespace Api.Infrastructure.RequestDTOs.User;

public class UserRequest
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

}
