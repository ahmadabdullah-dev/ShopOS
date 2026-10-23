namespace Domain.Tenant;

public class Tenant : BaseEntity
{
    public required string Name { get; set; }
    public string? Description { get; set; }
}