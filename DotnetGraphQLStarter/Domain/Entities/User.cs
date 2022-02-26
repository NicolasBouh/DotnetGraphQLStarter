using System.ComponentModel.DataAnnotations;
using DotnetGraphQLStarter.Domain.Core;

namespace DotnetGraphQLStarter.Domain.Entities;

public class User : BaseEntity
{
    [Required] 
    [StringLength(100)] 
    public string FirstName { get; set; } = null!;
    [Required] 
    [StringLength(100)] 
    public string LastName { get; set; } = null!;
    [Required] 
    [StringLength(100)] 
    public string Email { get; set; } = null!;
    [Required] 
    [StringLength(400)] 
    public byte[] PasswordHash { get; set; } = null!;
    
    [Required] 
    [StringLength(400)] 
    public byte[] PassworSalt { get; set; } = null!;
    
    public ICollection<Training> Trainings { get; set; } =
        new List<Training>();
}