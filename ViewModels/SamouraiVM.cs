using Module6.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace Module6.ViewModels
{
    public class SamouraiVM
    {
        public Samourai Samourai { get; set; }

        [Display(Name = "Arme choisie")]
        public int SelectedWeapon { get; set; }

        public List<SelectListItem> WeaponsListItems { get; set; } = new List<SelectListItem>();
    }
}