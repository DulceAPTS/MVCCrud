using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCCrud.Models.ViewModels
{
    public class TablaViewModel
    {
        public int ID { get; set; }
        [Required]
        [Display(Name = "Nombre")]
        [StringLength(50)]
        public string Nombre { get; set; }
        [Required]
        [Display(Name = "Correo electronico")]
        [StringLength(50)]
        [EmailAddress]
        public string Correo { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de nacimiento")]
        [DisplayFormat(DataFormatString = "(0:dd.mm.yyyy)", ApplyFormatInEditMode = true)]
        public DateTime Fecha_nacimiento { get; set; }
    }
}