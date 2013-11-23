using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace dziennik_asp_mvc.Models
{
    public class LogOnModel
    {
        [Required]
        [Display(Name = "Login")]
        public string UserName { get; set; }

        public bool isActive { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "{0} musi być co najmniej {2} znaków długości.", MinimumLength = 4)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }
    }
}
