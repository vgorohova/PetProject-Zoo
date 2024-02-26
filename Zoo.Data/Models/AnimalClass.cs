using System.ComponentModel.DataAnnotations;

namespace Zoo.Data.Models;

public class AnimalClass
{
    public int Id {get; set;}

    [Required]
    [MaxLength(200)]
    public string ClassTitle { get; set; } = null!;
}