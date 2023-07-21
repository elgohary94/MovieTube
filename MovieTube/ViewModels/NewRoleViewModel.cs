using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MovieTube.ViewModels;

public class NewRoleViewModel
{   
    [Required]
    [DisplayName("Role Name")]
    public String RoleName { get; set; }
}