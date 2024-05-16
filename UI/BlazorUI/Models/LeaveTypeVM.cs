using System.ComponentModel.DataAnnotations;

namespace BlazorUI.Models
{
  public class LeaveTypeVM
  {
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    [Display(Name = "Default Number of Days")]
    public int DefaultDays { get; set; }
  }
}
