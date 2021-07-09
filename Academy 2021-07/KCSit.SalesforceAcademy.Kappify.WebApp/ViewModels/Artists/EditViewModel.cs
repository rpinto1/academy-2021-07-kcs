using System;
using System.ComponentModel.DataAnnotations;

namespace KCSit.SalesforceAcademy.Kappify.WebApp.ViewModels.Artists
{
    public class EditViewModel
    {
        [Required]
        public Guid Uuid { get; set; }

        [Required]
        [MaxLength(40)]
        public string Name { get; set; }
        [MaxLength(200)]
        public string Bio { get; set; }

        [Required]
        [MaxLength(20)]
        public string Genre { get; set; }
    }
}
