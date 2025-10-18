namespace PropertyManagement.Application.DTOs;

public class PropertyQueryParameters
{
    public string? Name { get; set; }
    public int? HostId { get; set; }
    public string? Status { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
