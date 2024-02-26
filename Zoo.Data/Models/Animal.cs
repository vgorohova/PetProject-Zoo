using System.ComponentModel.DataAnnotations;

namespace Zoo.Data.Models;

public class Animal
{
    public int Id {get; set;}

    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = null!;

    [MaxLength(200)]
    public string? NickName { get; set; }

    [Required]
    public AnimalClass Classification { get; set; } = null!;

    public string Sound { get; set; } = null!;

    public string Photo { get; set; } = null!;
}