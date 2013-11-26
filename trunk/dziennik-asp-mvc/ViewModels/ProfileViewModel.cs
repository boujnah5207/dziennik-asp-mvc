using dziennik_asp_mvc.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace dziennik_asp_mvc.ViewModels
{
    public class ProfileViewModel
    {
        public Users Users { get; set; }

        [StringLength(255)]
        [Display(Name = "Aktualne hasło")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Pole nie może być puste!")]
        public string ActualPassword { get; set; }

        [StringLength(255)]
        [Display(Name = "Nowe hasło")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Pole nie może być puste!")]
        public string NewPassword { get; set; }

        [StringLength(255)]
        [Display(Name = "Potwierdź nowe hasło")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Pole nie może być puste!")]
        public string RepeatedPassword { get; set; }
    }
}