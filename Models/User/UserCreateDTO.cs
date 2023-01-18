namespace JobSeekerApi.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
public enum UserType
{
    Seeker = 1,
    Recruiter = 2
}
public class UserCreateDTO
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    // [JsonPropertyName("user_type_id")]
    [EnumDataType(typeof(UserType),ErrorMessage = "User type id must be valid UserType enum (Seeker = 1, Recruiter = 2)")]
    public UserType UserTypeId { get; set; }
    [Required]
    public string? Name { get; set; }
    [Required(ErrorMessage = "Email is required"), EmailAddress(ErrorMessage = "Email is not valid email")]
    public string? Email { get; set; }
    [Required(ErrorMessage = "Password is required")]
    public string? Password { get; set; }

    [Required(ErrorMessage = "Date of birth is required")]
    public DateTime? DateOfBirth { get; set; }
    [Required(ErrorMessage = "Gender is required")]
    public string? Gender { get; set; }
    [Required(ErrorMessage = "Contact number is required")]
    public string? ContactNumber { get; set; }
    [Required(ErrorMessage = "Sms notification is required")]
    public bool? SmsNotificationActive { get; set; }
    [Required(ErrorMessage = "Email notification is required")]
    public bool? EmailNotificationActive { get; set; }
    public byte[]? Image { get; set; }
}