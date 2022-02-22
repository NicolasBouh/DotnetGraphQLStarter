using System.ComponentModel.DataAnnotations;

namespace DotnetGraphQLStarter.Domain.Core;

public abstract class BaseEntity
{
    [Key]
    public int Id { get; set; }
}