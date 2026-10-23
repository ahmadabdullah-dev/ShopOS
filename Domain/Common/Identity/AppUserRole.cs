using Microsoft.AspNetCore.Identity;

namespace Domain.Common.Identity;

public class AppUserRole : IdentityRole
{
    public string? Description { get; set; }
}