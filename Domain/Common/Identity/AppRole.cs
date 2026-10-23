using Microsoft.AspNetCore.Identity;

namespace Domain.Common.Identity;

public class AppRole : IdentityRole
{
    public string? Description { get; set; }
}