using Domain.Enums;
using System.ComponentModel.DataAnnotations;
namespace Domain.Entities;
public class Owner
{
    [Key]
    [MaxLength(13)]
    public string PhoneNumber { get; set; }

    [MaxLength(30)]
    public string? FirstName { get; set; }
    
    [MaxLength(30)]
    public string? LastName { get; set; }
    public decimal Balance { get; set; }
    public StatusOwner Status { get; set; }
    public int Limit { get; set; }
    public DateTime CreatedAt { get; set; }
    public ICollection<Card> Cards { get; set; }
    public ICollection<Transaction> Transactions { get; set; }
}
