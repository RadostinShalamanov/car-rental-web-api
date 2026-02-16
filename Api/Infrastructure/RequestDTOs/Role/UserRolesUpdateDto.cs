using System;
using System.Collections.Generic;

namespace Api.Infrastructure.RequestDTOs.Role;

public class UserRolesUpdateDto
{
    public List<int> RoleIds { get; set; } = new();
}
