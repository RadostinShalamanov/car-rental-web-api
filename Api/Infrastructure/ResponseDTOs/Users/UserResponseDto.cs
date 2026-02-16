using System;
using System.Collections.Generic;
using Api.Infrastructure.ResponseDTOs.Roles;
using Api.Infrastructure.ResponseDTOs.Shared;

namespace Api.Infrastructure.ResponseDTOs.Users;

public class UserResponseDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }

    public List<IdNameDTO> Roles { get; set; } = new();
}
