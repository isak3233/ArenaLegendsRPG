using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ArenaLegendsRPG.Core.APIHandlers.DTOs;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(WeaponDto), "weapon")]
[JsonDerivedType(typeof(ProtectionDto), "protection")]
public abstract class ItemDto
{
    [Required]
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public bool IsEquipped { get; init; }
}