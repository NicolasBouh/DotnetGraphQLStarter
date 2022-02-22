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
    [StringLength(200)] 
    public string Password { get; set; } = null!;
    
    public ICollection<Training> Trainings { get; set; } =
        new List<Training>();
}