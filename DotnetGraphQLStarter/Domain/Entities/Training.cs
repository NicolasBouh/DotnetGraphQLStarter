using System.ComponentModel.DataAnnotations;
using DotnetGraphQLStarter.Domain.Core;
using DotnetGraphQLStarter.Domain.Enums;

namespace DotnetGraphQLStarter.Domain.Entities;

public class Training : BaseEntity
{ 
    [Required] 
    [StringLength(200)] 
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public int Duration { get; set; }
    public int Distance { get; set; }
    public TrainingType Type { get; set; } = TrainingType.Easy;

    public int UserId { get; set; }
    public User User { get; set; } = default!;
}