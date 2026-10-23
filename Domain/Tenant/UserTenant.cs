namespace Domain.Tenant;

public class UserTenant : BaseEntity
{
    public required string UserId { get; set; }
    public AppUser? User { get; set; }

    public required string TenantId { get; set; }
    public Tenant? Tenant { get; set; }

    public required string Role { get; set; }
}
