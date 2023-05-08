using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ChallengeB3.Application.ViewModel;

public class RegisterViewModel
{
    [Key]
    public int RegisterId { get; set; }

    [Required(ErrorMessage = "A Descrição é necessária")]
    [MinLength(2)]
    [MaxLength(100)]
    [DisplayName("Description")]
    public string Description { get; set; } = string.Empty;

    [Required(ErrorMessage = "O Status é necessário")]
    [MinLength(1)]
    [MaxLength(100)]
    [DisplayName("Status")]
    public string Status { get; set; } = string.Empty;

    [Required(ErrorMessage = "A Data é necessária")]
    [MinLength(1)]
    [MaxLength(100)]
    [DisplayName("Date")]
    public DateTime Date { get; set; }
}
