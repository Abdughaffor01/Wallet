namespace Domain.DTOs.OwnerDTOs;
public class UpdateOwnerDto:BaseOwnerDto
{
    public string? PhoneNumber { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}
