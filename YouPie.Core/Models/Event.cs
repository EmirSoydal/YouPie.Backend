namespace YouPie.Core.Models;

public class Event : BaseEntity
{
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public string Url { get; set; } = null!;
    public bool Visible { get; set; }
    public bool InviteOnly { get; set; }
    public Location Location { get; set; } = null!;
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}

public abstract class Location
{
    private string Latitude { get; set; } = null!;
    private string Longitude { get; set; } = null!;
}