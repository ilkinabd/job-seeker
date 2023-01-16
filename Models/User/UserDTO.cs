namespace JobSeekerApi.Models;

public class UserDTO
{
    public long UserTypeId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Gender { get; set; }
    public bool IsActive { get; set; }
    public string ContactNumber { get; set; }
    public bool SmsNotificationActive { get; set; }
    public bool EmailNotificationActive { get; set; }
    public byte[]? Image { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime DeletedAt { get; set; }
}