using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace FahrzeugeMVC.Models
{
    public class FahrzeugEinfuegenModel
    {
        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Type { get; set; }

        public List<SelectListItem> FahrzeugTypen { get; private set; }
            = new()
            {
                new SelectListItem("Auto", "Auto", true),
                new SelectListItem("Motorrad", "Motorrad"),
                new SelectListItem("Fahrrad", "Fahrrad")
            };
    }
}