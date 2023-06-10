using System.ComponentModel.DataAnnotations;

namespace MovieTube.ViewModels;

public class NewRoleViewModel
{   
    [Required]
    public String RoleName { get; set; }
}